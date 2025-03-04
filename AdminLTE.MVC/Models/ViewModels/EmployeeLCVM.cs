﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models.ViewModels
{
    public class EmployeeLCVM
    {
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> LocalCommunities { get; set; }
    }
}
