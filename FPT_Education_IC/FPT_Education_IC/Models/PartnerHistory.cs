using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class PartnerHistory
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Documents { get; set; }
        public string? Image { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Partner Partner { get; set; } = null!;
        public virtual UsrAccount User { get; set; } = null!;
    }
}
