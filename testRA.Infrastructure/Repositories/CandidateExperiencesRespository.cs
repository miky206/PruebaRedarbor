using Microsoft.EntityFrameworkCore;
using testRA.Domain.Entities;
using testRA.Infrastructure.Contexts;

namespace testRA.Infrastructure.Repositories
{
    public class CandidateExperiencesRespository : IGenericRespository<CandidateExperience>
    {
        private readonly TestRedarborContext _redarborContext;
        public CandidateExperiencesRespository(TestRedarborContext redarborContext)
        {
            _redarborContext = redarborContext;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                CandidateExperience model = _redarborContext.CandidateExperience.First(x => x.IdCandidateExperience == id);
                _redarborContext.Remove(model);
                await _redarborContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IList<CandidateExperience>> GetAll()
        {
            IList<CandidateExperience> listCandidateExperience = _redarborContext.CandidateExperience.ToList();
            return listCandidateExperience;
        }
       
        public async Task<CandidateExperience> GetById(int id)
        {
            return await _redarborContext.CandidateExperience.FindAsync(id);
        }

        public async Task<bool> Insert(CandidateExperience model)
        {
            try
            {
                _redarborContext.CandidateExperience.Add(model);
                await _redarborContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(CandidateExperience model)
        {
            try
            {
                _redarborContext.CandidateExperience.Update(model);
                _redarborContext.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}
