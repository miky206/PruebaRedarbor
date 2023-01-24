using AutoMapper;
using testRA.Domain.Entities;
using testRA.WebApp.Models.ViewModels;

namespace testRA.WebApp.AutoMapper
{
    public class AppProfile:Profile
    {
        public AppProfile()
        {
            CreateMap<Candidates, CandidatesViewModel>().ReverseMap();
            CreateMap<CandidateExperience, CandidateExperienceViewModel>().ReverseMap();
        }
    }
}
