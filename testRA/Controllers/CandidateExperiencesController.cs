using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testRA.Data;
using testRA.Models;

namespace testRA.Controllers
{
    public class CandidateExperiencesController : Controller
    {
        private readonly TestRedarborContext _context;

        public CandidateExperiencesController(TestRedarborContext context)
        {
            _context = context;
        }

        // GET: CandidateExperiences
        public async Task<IActionResult> Index()
        {
            var testRAContext = _context.CandidateExperience.Include(c => c.Candidates);
            return View(await testRAContext.ToListAsync());
        }

        // GET: CandidateExperiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CandidateExperience == null)
                return NotFound();

            var candidateExperience = await _context.CandidateExperience
                .Include(c => c.Candidates)
                .FirstOrDefaultAsync(m => m.IdCandidateExperience == id);
            if (candidateExperience == null)
                return NotFound();

            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Create
        public IActionResult Create()
        {
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Name");
            return View();
        }
        public IActionResult CreateWithCandidate(int id)
        {
           ViewData["IdCandidate"] = new SelectList(_context.Candidates.Where(x => x.IdCandidate == id), "IdCandidate", "Name");
           return View();
        }

        // POST: CandidateExperiences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCandidateExperience,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperience candidateExperience)
        {
            candidateExperience.InsertDate = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                _context.Add(candidateExperience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Name", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CandidateExperience == null)
                return NotFound();

            var candidateExperience = await _context.CandidateExperience.FindAsync(id);
            if (candidateExperience == null)
                return NotFound();

            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "Name", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // POST: CandidateExperiences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCandidateExperience,IdCandidate,Company,Job,Description,Salary,BeginDate,EndDate,InsertDate,ModifyDate")] CandidateExperience candidateExperience)
        {
            if (id != candidateExperience.IdCandidateExperience)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    candidateExperience.ModifyDate = DateTime.Now;
                    _context.Update(candidateExperience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExperienceExists(candidateExperience.IdCandidateExperience))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCandidate"] = new SelectList(_context.Candidates, "IdCandidate", "IdCandidate", candidateExperience.IdCandidate);
            return View(candidateExperience);
        }

        // GET: CandidateExperiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CandidateExperience == null)
                return NotFound();

            var candidateExperience = await _context.CandidateExperience
                .Include(c => c.Candidates)
                .FirstOrDefaultAsync(m => m.IdCandidateExperience == id);
            if (candidateExperience == null)
                return NotFound();

            return View(candidateExperience);
        }

        // POST: CandidateExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CandidateExperience == null)
                return Problem("Entity set 'testRAContext.CandidateExperience' is null.");

            var candidateExperience = await _context.CandidateExperience.FindAsync(id);
            if (candidateExperience != null)
                _context.CandidateExperience.Remove(candidateExperience);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExperienceExists(int id)
        {
            return (_context.CandidateExperience?.Any(e => e.IdCandidateExperience == id)).GetValueOrDefault();
        }
    }
}
