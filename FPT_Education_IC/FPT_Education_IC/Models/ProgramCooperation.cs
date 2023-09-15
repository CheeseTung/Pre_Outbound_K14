using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class ProgramCooperation
    {
        public int CooperationId { get; set; }
        public int ProgramId { get; set; }
        public int PartnerId { get; set; }
        public int MaxNumberRegister { get; set; }
        public int NumberOfRegister { get; set; }
        public int PartnerContact { get; set; }

        public virtual Partner Partner { get; set; } = null!;
        public virtual Program Program { get; set; } = null!;
    }
}
