using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int UserId { get; set; }
        public int ProgramContent { get; set; }
        public int Facilities { get; set; }
        public int PartnerSupport { get; set; }
        public int Extracurricular { get; set; }
        public int StaffSupport { get; set; }
        public string? Feedback1 { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual UsrAccount User { get; set; } = null!;
    }
}
