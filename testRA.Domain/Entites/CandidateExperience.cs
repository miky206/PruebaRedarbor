using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testRA.Domain.Entities
{
    [PrimaryKey(nameof(IdCandidateExperience))]
    public class CandidateExperience
    {
       
        public int IdCandidateExperience { get; private set; }
        public int IdCandidate { get; private set; }
        [Column(TypeName ="varchar(100)")]
        public string Company { get; private set; }
        [Column(TypeName = "varchar(100)")]
        public string Job { get; private set; }
        [Column(TypeName = "varchar(4000)")]
        public string Description{ get; private set; }
        [Column(TypeName = "decimal(8,2)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Salary { get; private set; }
        [DisplayName("Begin Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginDate { get; private set; }
        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EndDate { get; private set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InsertDate { get; private set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ModifyDate { get; private set; }
        [ForeignKey("IdCandidate")]
        public Candidates? Candidates { get; set; }

        public CandidateExperience(int idCandidateExperience, int idCandidate, string company, string job, string description, 
            decimal salary, DateTime beginDate, DateTime? endDate, DateTime insertDate, DateTime? modifyDate)
        {
            IdCandidateExperience = idCandidateExperience;
            IdCandidate = idCandidate;
            Company = company;
            Job = job;
            Description = description;
            Salary = salary;
            BeginDate = beginDate;
            EndDate = endDate;
            InsertDate = insertDate;
            ModifyDate = modifyDate;
   
        }

    }
}
