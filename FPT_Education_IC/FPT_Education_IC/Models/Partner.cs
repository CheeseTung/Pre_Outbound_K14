using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Partner
    {
        public Partner()
        {
            PartnerContacts = new HashSet<PartnerContact>();
            PartnerDocuments = new HashSet<PartnerDocument>();
            PartnerHistories = new HashSet<PartnerHistory>();
            ProgramCooperations = new HashSet<ProgramCooperation>();
        }

        public int PartnerId { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public string Country { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Image { get; set; } = null!;
        public string? Website { get; set; }
        public bool IsActive { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime StartDate { get; set; }
        public string? Description { get; set; }

        public virtual Country CountryNavigation { get; set; } = null!;
        public virtual UsrAccount User { get; set; } = null!;
        public virtual ICollection<PartnerContact> PartnerContacts { get; set; }
        public virtual ICollection<PartnerDocument> PartnerDocuments { get; set; }
        public virtual ICollection<PartnerHistory> PartnerHistories { get; set; }
        public virtual ICollection<ProgramCooperation> ProgramCooperations { get; set; }
    }
}
