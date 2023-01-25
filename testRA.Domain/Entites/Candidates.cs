using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testRA.Domain.Entities
{
    [PrimaryKey(nameof(IdCandidate))]
    public class Candidates
    {
        public int IdCandidate { get; private set; }
        [Column(TypeName = "varchar(50)")]
        public string Name{ get; private set; }
        [Column(TypeName = "varchar(250)")]
        public string Surname { get; private set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Birthdate { get; private set; }
        [Column(TypeName = "varchar(250)")]
        public string Email { get; private set; }
        [DisplayName("Insert Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InsertDate { get; private set; }
        [DisplayName("Modify Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ModifyDate { get; private set; }
        public Candidates(int idCandidate, string name, string surname, DateTime birthdate,
            string email, DateTime insertDate, DateTime? modifyDate)
        {
            IdCandidate = idCandidate;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Email = email;
            InsertDate = insertDate;
            ModifyDate = modifyDate;
        }
      
    }
}
