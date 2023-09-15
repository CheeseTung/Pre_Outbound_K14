using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class CommonMaster
    {
        public int Id { get; set; }
        public string MasterCode { get; set; } = null!;
        public string MasterType { get; set; } = null!;
        public string MasterName { get; set; } = null!;
        public string? Description { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
