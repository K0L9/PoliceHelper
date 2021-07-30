using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models
{
    public class LocalCommunity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        //navigation prop
        public virtual List<Employee> Employees { get; set; }

        public LocalCommunity()
        {
            Employees = new List<Employee>();
        }
    }
}
