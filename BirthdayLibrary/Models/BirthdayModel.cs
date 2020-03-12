using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayLibrary.Models
{
    public class BirthdayModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
