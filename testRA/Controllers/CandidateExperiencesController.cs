using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using testRA.Domain.Entities;
using testRA.Service.Interfaces;
using testRA.WebApp.Models.ViewModels;

namespace testRA.WebApp.Controllers
{
    public class CandidateExperiencesController : Controller
    {
        private readonly ICandidateExperienceService _candidateExperienceService;
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;
        public CandidateExperiencesController(ICandidateExperienceService candidateExperienceService, IMapper mapper, ICandidateService candidateService)
        {
            _candidateExperienceService = candidateExperienceService ?? throw new System.ArgumentNullException(nameof(candidateExperienceService));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _candidateService = candidateService ?? throw new System.ArgumentNullException(nameof(candidateService));
        }

        // GET: CandidateExperiences
        public async Task<IActionResult> Index()
        {
            IList<CandidateExperience> queryCandidatesExperience = await _candidateExperienceService.GetAll();
            if (queryCandidatesExperience != null)
            {
                var listCandidateExperience = _mapper.Map<List<CandidateExperienceViewModel>>(queryCandidatesExperience);
                return View(listCandidateExperience);
            }
            return Problem("Entity set 'Candidates Experience' is null.");
        }

        // GET: CandidateExperiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var responsecandidateExperience = await _candidateExperienceService.GetById(id??0);
            if (responsecandidateExperience == null)
                return NotFound();

            var candidateExperience = _mapper.Map<CandidateExperienceViewModel>(responsecandidateExperience);
            candidateExperience.Candidates = await GetCandidate(candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        //// GET: CandidateExperiences/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdCandidate"] = new SelectList(await _candidateService.GetAll(), "IdCandidate", "Name");
            return View();
        }
        public async Task<IActionResult> CreateWithCandidate(int id)
        {
            List<Candidates> listSelectList = new List<Candidates>();
            listSelectList.Add(await _candidateService.GetById(id));
            ViewData["IdCandidate"] = new SelectList(listSelectList, "IdCandidate", "Name");
            return View();
        }

        // POST: CandidateExperiences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidateExperience,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperienceViewModel candidateExperience)
        {
            if (ModelState.IsValid)
            {
                CandidateExperience newCandidateExperience = new CandidateExperience(
                    candidateExperience.IdCandidateExperience,
                    candidateExperience.IdCandidate,
                    candidateExperience.Company,
                    candidateExperience.Job,
                    candidateExperience.Description,
                    candidateExperience.Salary,
                    candidateExperience.BeginDate,
                    candidateExperience.EndDate,
                    DateTime.Now,
                    null
                    );
                return (await _candidateExperienceService.Insert(newCandidateExperience))?
                RedirectToAction(nameof(Index)) : Problem("Entity insert 'testRAContext.CandidateExperience' is null.");
            }
            ViewData["IdCandidate"] = new SelectList(await _candidateService.GetAll(), "IdCandidate", "Name", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var candidateExperience = await _candidateExperienceService.GetById(id??0);
            if (candidateExperience == null)
                return NotFound();

            ViewData["IdCandidate"] = new SelectList(await _candidateService.GetAll(), "IdCandidate", "Name", candidateExperience.IdCandidate);
            return View(_mapper.Map<CandidateExperienceViewModel>(candidateExperience));
        }

        // POST: CandidateExperiences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidateExperience,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperienceViewModel candidateExperience)
        {
            if (id != candidateExperience.IdCandidateExperience)
                return NotFound();

            if (ModelState.IsValid)
            {
                CandidateExperience newCandidateExperience = new CandidateExperience(
                    candidateExperience.IdCandidateExperience,
                    candidateExperience.IdCandidate,
                    candidateExperience.Company,
                    candidateExperience.Job,
                    candidateExperience.Description,
                    candidateExperience.Salary,
                    candidateExperience.BeginDate,
                    candidateExperience.EndDate,
                    candidateExperience.InsertDate,
                    DateTime.Now
                    );
                await _candidateExperienceService.Update(newCandidateExperience);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(await _candidateService.GetAll(), "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var candidateExperience = await _candidateExperienceService.GetById(id ?? 0);
            if (candidateExperience == null)
                return NotFound();

            return View(_mapper.Map<CandidateExperienceViewModel>(candidateExperience));
        }

        // POST: CandidateExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _candidateExperienceService.Delete(id);
            if (response)
                return RedirectToAction(nameof(Index));
            else
                return Problem("Entity set 'testRAContext.CandidateExperience' is null.");
        }

        #region Private Method

        private async Task<CandidatesViewModel> GetCandidate(int idCandidate)
        {
            var responseCandidate = await _candidateService.GetById(idCandidate);
            if (responseCandidate == null)
                return new CandidatesViewModel();
            return _mapper.Map<CandidatesViewModel>(responseCandidate);
        }

        #endregion

       
    }
}
