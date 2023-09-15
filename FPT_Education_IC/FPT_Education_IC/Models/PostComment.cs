using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class PostComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string CommentText { get; set; } = null!;
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ProgramPost Post { get; set; } = null!;
    }
}
