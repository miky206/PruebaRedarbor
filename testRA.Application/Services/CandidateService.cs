using testRA.Domain.Entities;
using testRA.Infrastructure.Repositories;
using testRA.Service.Interfaces;

namespace testRA.Service.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly IGenericRespository<Candidates> _candidatesRepo;

        public CandidateService(IGenericRespository<Candidates> candidatesRepo)
        {
            _candidatesRepo = candidatesRepo;
        }
        public async Task<bool> Delete(int id)
        {
           return await _candidatesRepo.Delete(id);
        }

        public async Task<IList<Candidates>> GetAll()
        {
           return await _candidatesRepo.GetAll();
        }

        public async Task<Candidates> GetById(int id)
        {
            return await _candidatesRepo.GetById(id);
        }

        public async Task<bool> Insert(Candidates model)
        {
            return await _candidatesRepo.Insert(model);
        }

        public async Task<bool> Update(Candidates model)
        {
            return await _candidatesRepo.Update(model);
        }
    }
}
