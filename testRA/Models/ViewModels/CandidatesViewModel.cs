using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace testRA.WebApp.Models.ViewModels
{
    public class CandidatesViewModel
    {
        public int IdCandidate { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DisplayName("Insert Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InsertDate { get; set; }
        public List<CandidateExperienceViewModel>? ListCandidateExperiences { get; set; }
    }
}
