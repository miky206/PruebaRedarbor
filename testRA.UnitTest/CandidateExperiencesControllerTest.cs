using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testRA.Domain.Entities;
using testRA.Service.Interfaces;
using testRA.WebApp.AutoMapper;
using testRA.WebApp.Controllers;
using testRA.WebApp.Models.ViewModels;

namespace testRA.UnitTest
{
    public  class CandidateExperiencesControllerTest
    {
        private readonly Mock<ICandidateService> candidateService;
        private readonly Mock<ICandidateExperienceService> candidateExperienceService;
        private readonly IMapper _mapper;
        public CandidateExperiencesControllerTest()
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
            var candidateExperiencesController = new CandidateExperiencesController(candidateExperienceService.Object, _mapper, candidateService.Object);

            //Act

            var candidateExperiencesResult = await candidateExperiencesController.Index();
            //Asserts
            var viewResult = Assert.IsType<ViewResult>(candidateExperiencesResult);
            var model = Assert.IsAssignableFrom<IEnumerable<CandidateExperienceViewModel>>(
                viewResult.ViewData.Model);
            Assert.NotNull(candidateExperiencesResult);
            Assert.Equal(GetListCandidates().Result.Count(), model.Count());
        }
        [Fact]
        public async Task Detail_Test_Ok()
        {
            //arrange
            int id = 1;
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            CandidateExperience candidateExperience = candidateExperienceList.Result.Where(x => x.IdCandidateExperience == id).FirstOrDefault();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetById(id))
                           .Returns(Task.FromResult(candidateExperience));
            var candidateExperiencesController = new CandidateExperiencesController(candidateExperienceService.Object, _mapper, candidateService.Object);

            //act

            var candidateExperienceResult = await candidateExperiencesController.Details(id);
            //asserts
            var viewResult = Assert.IsType<ViewResult>(candidateExperienceResult);
            var model = Assert.IsAssignableFrom<CandidateExperienceViewModel>(
                viewResult.ViewData.Model);
            Assert.NotNull(candidateExperienceResult);
            Assert.IsType<CandidateExperienceViewModel>(model);
            Assert.Equal("ENDESA", model.Company);
            Assert.Equal(1500.7m, model.Salary);
        }
        [Fact]
        public async Task DetailReturnsARedirectToIndexCandidateExperienceWhenIdIsNull()
        {
            // Arrange
            var candidatesList = GetListCandidates();
            var candidateExperienceList = GetListCandidateExperience();
            candidateService.Setup(x => x.GetAll())
                            .Returns(candidatesList);
            candidateExperienceService.Setup(x => x.GetAll())
                           .Returns(candidateExperienceList);
            var candidateExperiencesController = new CandidateExperiencesController(candidateExperienceService.Object, _mapper, candidateService.Object);

            // Act
            var result = await candidateExperiencesController.Details(id: null);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("CandidateExperiences", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
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
