using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class StudyResult
    {
        public int Id { get; set; }
        public int ExchangeId { get; set; }
        public int UserId { get; set; }
        public decimal ResultMark { get; set; }
        public bool Condition { get; set; }
        public string? ConditionReason { get; set; }
        public bool SubjectStatus { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual StudyExchange Exchange { get; set; } = null!;
        public virtual UsrAccount User { get; set; } = null!;
    }
}
