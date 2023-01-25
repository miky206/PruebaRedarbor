using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using testRA.Domain.Entities;
using testRA.Service.Interfaces;
using testRA.Service.Service;
using testRA.WebApp.Models.ViewModels;

namespace testRA.WebApp.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly ICandidateExperienceService _candidateExperienceService;
        private readonly IMapper _mapper;
        public CandidatesController(ICandidateService candidateService, ICandidateExperienceService candidateExperienceService, IMapper mapper)
        {
            _candidateService = candidateService ?? throw new System.ArgumentNullException(nameof(candidateService));
            _candidateExperienceService = candidateExperienceService ?? throw new System.ArgumentNullException(nameof(candidateExperienceService));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper)); 
        }
        #region Public Methods
        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            IList<Candidates> queryCandidates = await _candidateService.GetAll();
            if (queryCandidates != null)
            {
                var listCandidates = _mapper.Map<List<CandidatesViewModel>>(queryCandidates);
                return View(listCandidates);
            }
            return Problem("Entity set 'testRAContext.Candidates' is null.");
        }

        //GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Candidates getCandidates = await _candidateService.GetById(id??0);
            if (getCandidates != null)
            {
                var responseCandidates = _mapper.Map<CandidatesViewModel>(getCandidates);
                responseCandidates.ListCandidateExperiences =await GetCandidateExperiences(responseCandidates.IdCandidate);
                return View(responseCandidates);
            }
            else
                return RedirectToAction(actionName: nameof(Index),
                controllerName: "Candidates");
        }

        //GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Createa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidate,Name,Surname,Birthdate,Email,InsertDate,ModifyDate")] CandidatesViewModel candidates)
        {
            if (ModelState.IsValid)
            {
                Candidates newCandidate = new Candidates(
                    candidates.IdCandidate,
                    candidates.Name,
                    candidates.Surname,
                    candidates.Birthdate,
                    candidates.Email,
                    DateTime.Now,
                    null
                    );
                if(await _candidateService.Insert(newCandidate))
                    return RedirectToAction(nameof(Index));
                else
                    return Problem("Entity set 'testRAContext.candidate' not insert.");
            }
            return View(candidates);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Candidates getCandidates = await _candidateService.GetById(id ?? 0);
            if (getCandidates != null)
            {
                var responseCandidates = _mapper.Map<CandidatesViewModel>(getCandidates);
                responseCandidates.ListCandidateExperiences = await GetCandidateExperiences(responseCandidates.IdCandidate);
                return View(responseCandidates);
            }
            else
                return NotFound();
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidate,Name,Surname,Birthdate,Email,InsertDate, ListCandidateExperiences")] CandidatesViewModel candidates)
        {
            if (id != candidates.IdCandidate)
                return NotFound();
            if (ModelState.IsValid)
            {
               Candidates newCandidate = new Candidates(
                        candidates.IdCandidate, candidates.Name, candidates.Surname, candidates.Birthdate,
                        candidates.Email, candidates.InsertDate, DateTime.Now);
                if (await _candidateService.Update(newCandidate))
                {
                    if (candidates.ListCandidateExperiences !=null)
                    {
                        if (!UpdateExperience(candidates.ListCandidateExperiences))
                             return Problem("Entity set 'testRAContext.CandidateExperience' not updated.");
                    }
                }
                else
                {
                    if (!CandidatesExists(candidates.IdCandidate))
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(candidates);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var candidates = await _candidateService.GetById(id ?? 0);
            if (candidates == null)
                return NotFound();

            return View(_mapper.Map<CandidatesViewModel>(candidates));
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool response = await _candidateService.Delete(id);
            return (response) ? RedirectToAction(nameof(Index)) : Problem("Entity set 'testRAContext.Candidates' has not been delete.");
        }

        #endregion
        #region Private Methods

        private async Task<List<CandidateExperienceViewModel>> GetCandidateExperiences(int id)
        {
            IList<CandidateExperience> listCandidateExperience = await _candidateExperienceService.GetAllByCandidate(id);
            if (listCandidateExperience == null)
                return new List<CandidateExperienceViewModel>();

             return _mapper.Map<List<CandidateExperienceViewModel>>(listCandidateExperience);
        }
        private bool CandidatesExists(int id)
        {
            return (_candidateService.GetById(id) != null)?true:false;
        }


        private bool UpdateExperience(List<CandidateExperienceViewModel> candidateExperienceList)
        {
            try
            { 
                foreach (var itemExperience in candidateExperienceList)
                {
                    CandidateExperience candidateExperience = new CandidateExperience(
                        itemExperience.IdCandidateExperience,
                        itemExperience.IdCandidate,
                        itemExperience.Company,
                        itemExperience.Job,
                        itemExperience.Description,
                        itemExperience.Salary,
                        itemExperience.BeginDate,
                        itemExperience.EndDate,
                        itemExperience.InsertDate,
                        DateTime.Now
                        );
                   var response = _candidateExperienceService.Update(candidateExperience);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
