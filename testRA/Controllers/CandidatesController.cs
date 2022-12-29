using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testRA.Data;
using testRA.Models;

namespace testRA.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly TestRedarborContext _context;

        public CandidatesController(TestRedarborContext context)
        {
            _context = context;
        }
        #region Public Methods
        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            return _context.Candidates != null ?
                        View(await _context.Candidates.ToListAsync()) :
                        Problem("Entity set 'testRAContext.Candidates' is null.");
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null || _context.Candidates == null)
                return NotFound();

            var candidates = await _context.Candidates
                .FirstOrDefaultAsync(m => m.IdCandidate == id);

            
            if (candidates == null)
            {
                return NotFound();
            }
            else
            {
                var experience = _context.CandidateExperience.Where(x=> x.IdCandidate == id);
                if (experience.Any())
                    candidates.CandidateExperiences = experience.ToList<CandidateExperience>();
            }

            return View(candidates);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Createa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidate,Name,Surname,Birthdate,Email,InsertDate,ModifyDate")] Candidates candidates)
        {
            if (ModelState.IsValid)
            {
                candidates.InsertDate = DateTime.Now.Date;
                _context.Add(candidates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidates);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Candidates == null)
                return NotFound();

            var candidates = await _context.Candidates.FindAsync(id);

            if (candidates == null)
                return NotFound();

            candidates.CandidateExperiences = GetExperience(id);
            
            return View(candidates);
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidate,Name,Surname,Birthdate,Email,InsertDate,ModifyDate, CandidateExperiences")] Candidates candidates)
        {
            if (id != candidates.IdCandidate)
                return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    candidates.ModifyDate = DateTime.Now.Date;
                    _context.Update(candidates);
                    await _context.SaveChangesAsync();
                    if (candidates.CandidateExperiences != null)
                    {
                        List<CandidateExperience> candidateExperienceList = candidates.CandidateExperiences;
                        if (!UpdateExperience(candidateExperienceList))
                            return Problem("Entity set 'testRAContext.CandidateExperience' not updated.");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatesExists(candidates.IdCandidate))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(candidates);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Candidates == null)
                return NotFound();

            var candidates = await _context.Candidates
                .FirstOrDefaultAsync(m => m.IdCandidate == id);

            if (candidates == null)
                return NotFound();

            return View(candidates);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Candidates == null)
                return Problem("Entity set 'testRAContext.Candidates' is null.");

            var candidates = await _context.Candidates.FindAsync(id);
            if (candidates != null)
            {
                _context.Candidates.Remove(candidates);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        #endregion
        #region Private Methods
        private bool CandidatesExists(int id)
        {
            return (_context.Candidates?.Any(e => e.IdCandidate == id)).GetValueOrDefault();
        }

        private List<CandidateExperience> GetExperience(int? id)
        {
            var candidateExperienceList = _context.CandidateExperience.Where(x => x.IdCandidate == id);
            if (candidateExperienceList.Any())            
                return candidateExperienceList.ToList();

            return new List<CandidateExperience>();
        }
        private bool  UpdateExperience(List<CandidateExperience> candidateExperienceList)
        {
            try
            {
                foreach (var itemExperience in candidateExperienceList)
                {
                    _context.CandidateExperience.Update(itemExperience);
                    _context.SaveChanges();
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
