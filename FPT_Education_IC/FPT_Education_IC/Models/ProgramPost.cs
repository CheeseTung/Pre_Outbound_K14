using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class ProgramPost
    {
        public ProgramPost()
        {
            PostComments = new HashSet<PostComment>();
        }

        public int PotsId { get; set; }
        public int ProgramId { get; set; }
        public string Title { get; set; } = null!;
        public string PostContent { get; set; } = null!;
        public string? Image { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Program Program { get; set; } = null!;
        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}
