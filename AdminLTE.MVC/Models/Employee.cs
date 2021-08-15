using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [DisplayName("Ім'я")]
        [Required(ErrorMessage ="Введіть ім'я")]
        public string FirstName { get; set; }

        [DisplayName("Прізвище")]
        [Required(ErrorMessage ="Введіть прізвище")]
        public string LastName { get; set; }

        [DisplayName("По-батькові")]
        public string MiddleName{ get; set; }

        [Required(ErrorMessage ="Введіть номер телефону")]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }
        public string PhotoPath { get; set; }

        //foreign keys
        [Required(ErrorMessage ="Оберіть ОТГ")]
        public int LocalCommunityId{ get; set; }

        //navigation prop
        [Required]
        public virtual LocalCommunity LocalCommunity { get; set; }
    }
}
