using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testRA.Domain.Entities;
using testRA.Infrastructure.Contexts;

namespace testRA.Infrastructure.Repositories
{
    public class CandidatesRespository : IGenericRespository<Candidates>
    {
        private readonly TestRedarborContext _redarborContext;
        public CandidatesRespository(TestRedarborContext redarborContext)
        {
            _redarborContext = redarborContext;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Candidates model = _redarborContext.Candidates.First(x => x.IdCandidate == id);
                _redarborContext.Remove(model);
                await _redarborContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<IList<Candidates>> GetAll()
        {
            IList<Candidates> listCandidates = _redarborContext.Candidates.ToList();
            return listCandidates;
        }

        public async Task<Candidates> GetById(int id)
        {
            return await _redarborContext.Candidates.FindAsync(id);
        }

        public async Task<bool> Insert(Candidates model)
        {
            try
            {
                _redarborContext.Candidates.Add(model);
                await _redarborContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> Update(Candidates model)
        {
            try
            {
                _redarborContext.Candidates.Update(model);
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
