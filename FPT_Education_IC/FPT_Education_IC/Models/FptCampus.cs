using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class FptCampus
    {
        public FptCampus()
        {
            UsrAccounts = new HashSet<UsrAccount>();
        }

        public int CampusCode { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }

        public virtual ICollection<UsrAccount> UsrAccounts { get; set; }
    }
}
