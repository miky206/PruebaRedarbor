using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace testRA.WebApp.Models.ViewModels
{
    public class CandidateExperienceViewModel
    {
        public int IdCandidateExperience { get; set; }
        public int IdCandidate { get; set; }
        [Required]
        public string? Company { get; set; }
        [Required]
        public string? Job { get; set; }
        [Required]
        public string? Description { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Salary { get; set; }
        [Required]
        [DisplayName("Begin Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginDate { get; set; }
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InsertDate { get; set; }
        public CandidatesViewModel? Candidates { get; set; }
    }
}
