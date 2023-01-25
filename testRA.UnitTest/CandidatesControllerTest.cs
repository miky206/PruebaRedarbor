using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using testRA.Domain.Entities;
using testRA.Service.Interfaces;
using testRA.WebApp.AutoMapper;
using testRA.WebApp.Controllers;
using testRA.WebApp.Models.ViewModels;

namespace testRA.UnitTest
{
    public class CandidatesControllerTest
    {
       
        private readonly Mock<ICandidateService> candidateService;
        private readonly Mock<ICandidateExperienceService> candidateExperienceService;
        private readonly IMapper _mapper;
        public CandidatesControllerTest()
        {
            candidateService = new Mock<ICandidateService>();
            candidateExperienceService = new Mock<ICandidateExperienceService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AppProfile>());
            var mapper = config.CreateMapper();
            _mapper = mapper;
        }
       
        [Fact]
        public async Task Index_Test_Ok()
        {
            //Arrange
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetAll())
                           .Returns(candidateExperienceList);
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object,_mapper);

            //Act

            var candidateResult = await candidatesController.Index();
            //Asserts
            var viewResult = Assert.IsType<ViewResult>(candidateResult);
            var model = Assert.IsAssignableFrom<IEnumerable<CandidatesViewModel>>(
                viewResult.ViewData.Model);
            Assert.NotNull(candidateResult);
            Assert.Equal(GetListCandidates().Result.Count(), model.Count());
        }
        [Fact]
        public async Task Detail_Test_Ok()
        {
            //arrange
            int id = 1;
            var candidatesList = GetListCandidates();
            var candidate = candidatesList.Result.Where(x=> x.IdCandidate == id);
            var candidateExperienceList = GetListCandidateExperience();
            IList<CandidateExperience> candidateExperience = candidateExperienceList.Result.Where(x => x.IdCandidate == id).ToList();
            candidateService.Setup(x => x.GetById(id))
                            .Returns(Task.FromResult(candidate.FirstOrDefault()));
            candidateExperienceService.Setup(x => x.GetAllByCandidate(id))
                           .Returns(Task.FromResult(candidateExperience));
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object, _mapper);

            //act

            var candidateResult = await candidatesController.Details(id);
            //asserts
            var viewResult = Assert.IsType<ViewResult>(candidateResult);
            var model = Assert.IsAssignableFrom<CandidatesViewModel>(
                viewResult.ViewData.Model);
            Assert.NotNull(candidateResult);
            Assert.IsType<CandidatesViewModel>(model);
            Assert.Equal("Marcos", model.Name);
            Assert.Equal(DateTime.Now.Day, model.Birthdate.Day);
        }
        [Fact]
        public async Task DetailReturnsARedirectToIndexCandidateWhenIdIsNull()
        {
            // Arrange
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetAll())
                           .Returns(candidateExperienceList);
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object, _mapper);

            // Act
            var result = await candidatesController.Details(id: null);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Candidates", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        [Fact]
        public async Task Create_With_InvalidModel()
        {
            // Arrange
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetAll())
                           .Returns(candidateExperienceList);
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object, _mapper);
            candidatesController.ModelState.AddModelError("error", "some error");
            
            // Act
            var result = await candidatesController.Create(candidates: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Create_ModelStateValid_CreateCandidateCalledOnce()
        {
            // Arrange
            Candidates? candidate = null;
            candidateService.Setup(r => r.Insert(It.IsAny<Candidates>()))
                .Callback<Candidates>(x => candidate = x);
            var newCandidate = new CandidatesViewModel()
            {
                IdCandidate = 4,
                Name = "Victor",
                Surname = "Lopez",
                Birthdate = Convert.ToDateTime("18/06/2001"),
                Email = "victor@prueba.com",
                InsertDate = DateTime.Now

            };

            //Act
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object, _mapper);
            candidatesController.Create(newCandidate);

            //Assert
            candidateService.Verify(x => x.Insert(It.IsAny<Candidates>()), Times.Once);
            Assert.Equal(candidate.Name, newCandidate.Name);
            Assert.Equal(candidate.Email, newCandidate.Email);
            Assert.Equal(candidate.Birthdate, newCandidate.Birthdate);

        }
        [Fact]
        public void DeleteCandidate_ExistingIdPassed_RemoveOneItem()
        {
            //Arrange
            var id = 1;
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetAll())
                           .Returns(candidateExperienceList);
            //Act
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object, _mapper);
            var result = candidatesController.DeleteConfirmed(id).Result;
            //Assert
            candidateService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task Create_Ko_With_ValidModel_RedirectToIndex()
        {
            // Arrange 
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetAll())
                           .Returns(candidateExperienceList);
            var candidatesController = new CandidatesController(candidateService.Object, candidateExperienceService.Object, _mapper);
            candidatesController.ModelState.AddModelError("error", "some error");

            // Act
            var result = await candidatesController.Create(candidates: null);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
        #region Data_Fake
        private Task<IList<Candidates>> GetListCandidates()
        {
            IList<Candidates> listCandidatesData = new List<Candidates>
            {
                new Candidates(1,"Marcos", "Gonzalez", DateTime.Now.AddYears(-20),"Prueba@prueba.com",DateTime.Now,null),
                new Candidates(2, "Raul", "Martín", DateTime.Now.AddYears(-25), "Prueba@prueba.com", DateTime.Now, null),
                new Candidates(3, "Adela", "Fernandez", DateTime.Now.AddYears(-2), "Prueba@prueba.com", DateTime.Now, null)
            };

            return Task.FromResult(listCandidatesData);
        }
        
        private Task<IList<CandidateExperience>> GetListCandidateExperience()
        {
            IList<CandidateExperience> listCandidateExperienceData = new List<CandidateExperience>
            {
                new CandidateExperience(1,1,"ENDESA", "Mantenimiento","reparaciones basicas",1500.7m,Convert.ToDateTime("01/01/2021"),Convert.ToDateTime("31/12/2021"), DateTime.Now,null),
                new CandidateExperience(2,3, "COCACOLA", "Operario","control de calidad",1200.7m,Convert.ToDateTime("01/01/2020"),Convert.ToDateTime("31/12/2021"), DateTime.Now,null),
                new CandidateExperience(3,2, "INTEL", "Gestor de residuos","reciclaje de productos quimicos",2500.7m,Convert.ToDateTime("01/01/2022"),Convert.ToDateTime("31/12/2022"), DateTime.Now,null)
            };

            return Task.FromResult(listCandidateExperienceData);
        }
        #endregion
    }
}
