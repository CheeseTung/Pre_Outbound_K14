using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class RegisterAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int RegisterId { get; set; }
        public string? AnswerContent { get; set; }

        public virtual RegisterQuestion Question { get; set; } = null!;
        public virtual Register Register { get; set; } = null!;
    }
}
