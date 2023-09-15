using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Country
    {
        public Country()
        {
            Partners = new HashSet<Partner>();
        }

        public string IsoCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string IsoCode3 { get; set; } = null!;

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
