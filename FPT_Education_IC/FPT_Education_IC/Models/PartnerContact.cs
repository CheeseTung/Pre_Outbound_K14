using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class PartnerContact
    {
        public int ContactId { get; set; }
        public int PartnerId { get; set; }
        public int ContactLevel { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }

        public virtual Partner Partner { get; set; } = null!;
    }
}
