using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models.ViewModels
{
    public class EmployeesLocalCommunitiesVM
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<SelectListItem> LocalCommunities { get; set; }
        public LocalCommunity LocalCommunity { get; set; }
    }
}
