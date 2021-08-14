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

        [DisplayName("First name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Middle name")]
        [Required]
        public string MiddleName{ get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }
        public string PhotoPath { get; set; }

        //foreign keys
        public int LocalCommunityId{ get; set; }

        //navigation prop
        [Required]
        public virtual LocalCommunity LocalCommunity { get; set; }
    }
}
