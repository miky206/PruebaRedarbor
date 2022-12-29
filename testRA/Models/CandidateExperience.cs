using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testRA.Models
{
    [PrimaryKey(nameof(IdCandidateExperience))]
    public class CandidateExperience
    {
        public int IdCandidateExperience { get; set; }
        public int IdCandidate { get; set; }
        [Column(TypeName ="varchar(100)")]
        public string Company { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Job { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Description{ get; set; }
        [Column(TypeName = "decimal(8,2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Salary { get; set; }
        [DisplayName("Begin Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginDate { get; set; }
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InsertDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ModifyDate { get; set; }
        [ForeignKey("IdCandidate")]
        public Candidates? Candidates { get; set; }
    }
}
