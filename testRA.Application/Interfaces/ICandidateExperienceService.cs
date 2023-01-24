using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testRA.Domain.Entities;

namespace testRA.Service.Interfaces
{
    public interface ICandidateExperienceService
    {
        Task<bool> Insert(CandidateExperience model);
        Task<bool> Update(CandidateExperience model);
        Task<bool> Delete(int id);
        Task<CandidateExperience> GetById(int id);
        Task<IList<CandidateExperience>> GetAll();
        Task<IList<CandidateExperience>> GetAllByCandidate(int id);
    }
}
