using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testRA.Domain.Entities;
using testRA.Infrastructure.Repositories;
using testRA.Service.Interfaces;

namespace testRA.Service.Service
{
    public class CandidateExperienceService : ICandidateExperienceService
    {
        private readonly IGenericRespository<CandidateExperience> _candidatesExperienceRepo;

        public CandidateExperienceService(IGenericRespository<CandidateExperience> candidatesExperienceRepo)
        {
            _candidatesExperienceRepo = candidatesExperienceRepo;
        }
        public async Task<bool> Delete(int id)
        {
           return await _candidatesExperienceRepo.Delete(id);
        }

        public async Task<IList<CandidateExperience>> GetAll()
        {
           return await _candidatesExperienceRepo.GetAll();
        }

        public async Task<IList<CandidateExperience>> GetAllByCandidate(int id)
        {
            var responseCandidateExperienceRepo = await _candidatesExperienceRepo.GetAll();
            return responseCandidateExperienceRepo.Where(x=>x.IdCandidate == id).ToList();
        }

        public async Task<CandidateExperience> GetById(int id)
        {
            return await _candidatesExperienceRepo.GetById(id);
        }

        public async Task<bool> Insert(CandidateExperience model)
        {
            return await _candidatesExperienceRepo.Insert(model);
        }

        public async Task<bool> Update(CandidateExperience model)
        {
            return await _candidatesExperienceRepo.Update(model);
        }
    }
}
