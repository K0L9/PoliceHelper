using AdminLTE.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models.ViewModels
{
    public class LocalCommunityVM
    {
        public IEnumerable<LocalCommunity> LocalCommunities { get; set; }
        public LocalCommunity LocalCommunity { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
