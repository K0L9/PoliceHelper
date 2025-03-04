﻿using AdminLTE.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models.ViewModels
{
    public enum IndexMode
    {
        Show,
        SelectForRemove,
        SelectForEdit
    }
    public class EmployeesLocalCommunitiesVM
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<SelectListItem> LocalCommunities { get; set; }
        public EmployeesFilterVM Filter { get; set; }
        public IndexMode Mode { get; set; }
        public PageInfo PageInfo { get; set; }
        public bool IsFilter { get; set; }
        public EmployeesLocalCommunitiesVM()
        {
            Mode = IndexMode.Show;
        }
    }
}
