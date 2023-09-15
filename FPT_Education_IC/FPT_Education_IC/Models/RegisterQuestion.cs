using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class RegisterQuestion
    {
        public RegisterQuestion()
        {
            RegisterAnswers = new HashSet<RegisterAnswer>();
        }

        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int QuestionType { get; set; }
        public string QuestionContent { get; set; } = null!;
        public bool? IsRequire { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual ICollection<RegisterAnswer> RegisterAnswers { get; set; }
    }
}
