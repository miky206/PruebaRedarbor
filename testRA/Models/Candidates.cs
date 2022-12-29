using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testRA.Models
{
    [PrimaryKey(nameof(IdCandidate))]
    public class Candidates
    {
        public int IdCandidate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Name{ get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Surname { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }
        [DisplayName("Insert Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InsertDate { get; set; }
        [DisplayName("Modify Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ModifyDate { get; set; }
        public List<CandidateExperience>? CandidateExperiences { get; set; }
    }
}
