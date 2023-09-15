using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Register
    {
        public Register()
        {
            PaymentLogs = new HashSet<PaymentLog>();
            RegisterAnswers = new HashSet<RegisterAnswer>();
        }

        public int RegisterId { get; set; }
        public int ProgramId { get; set; }
        public int UserId { get; set; }
        public DateTime RegisterDate { get; set; }
        public int RegisterStatus { get; set; }
        public string? RequestChange { get; set; }
        public string? RequestCancel { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? PartnerId { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual UsrAccount User { get; set; } = null!;
        public virtual ICollection<PaymentLog> PaymentLogs { get; set; }
        public virtual ICollection<RegisterAnswer> RegisterAnswers { get; set; }
    }
}
