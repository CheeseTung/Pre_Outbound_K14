using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class PaymentLog
    {
        public int LogId { get; set; }
        public int RegisterId { get; set; }
        public int ProgramId { get; set; }
        public decimal PaymentValue { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Status { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual Register Register { get; set; } = null!;
    }
}
