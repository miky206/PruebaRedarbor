using testRA.Domain.Entities;

namespace testRA.Service.Interfaces
{
    public interface ICandidateService
    {
        Task<bool> Insert(Candidates model);
        Task<bool> Update(Candidates model);
        Task<bool> Delete(int id);
        Task<Candidates> GetById(int id);
        Task<IList<Candidates>> GetAll();
    }
}
