using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class UsrAccount
    {
        public UsrAccount()
        {
            Feedbacks = new HashSet<Feedback>();
            PartnerHistories = new HashSet<PartnerHistory>();
            Partners = new HashSet<Partner>();
            Programs = new HashSet<Program>();
            Registers = new HashSet<Register>();
            StudyResults = new HashSet<StudyResult>();
        }

        public int UserId { get; set; }
        public string Role { get; set; } = null!;
        public int Campus { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Major { get; set; }
        public string? RollNumber { get; set; }
        public string Image { get; set; } = null!;
        public string? Phone { get; set; }
        public string? IdNumber { get; set; }
        public string? Passport { get; set; }
        public DateTime? PassportStartDate { get; set; }
        public DateTime? PassportEndDate { get; set; }
        public string? Facebook { get; set; }
        public bool IsActive { get; set; }
        public int? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? IdNumberStDate { get; set; }

        public virtual FptCampus CampusNavigation { get; set; } = null!;
        public virtual UsrAccountRole RoleNavigation { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<PartnerHistory> PartnerHistories { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<Register> Registers { get; set; }
        public virtual ICollection<StudyResult> StudyResults { get; set; }
    }
}
