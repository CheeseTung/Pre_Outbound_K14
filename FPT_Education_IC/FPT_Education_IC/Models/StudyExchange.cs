using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class StudyExchange
    {
        public StudyExchange()
        {
            StudyResults = new HashSet<StudyResult>();
        }

        public int ExchangeId { get; set; }
        public int ProgramId { get; set; }
        public string CourseName { get; set; } = null!;
        public string FptCourse { get; set; } = null!;
        public decimal MaxMark { get; set; }
        public decimal PassMark { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual ICollection<StudyResult> StudyResults { get; set; }
    }
}
