using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class ProgramManagement
    {
        public int ManageId { get; set; }
        public int ProgramId { get; set; }
        public int UserId { get; set; }
        public int ManageRole { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual UsrAccount User { get; set; } = null!;
    }
}
