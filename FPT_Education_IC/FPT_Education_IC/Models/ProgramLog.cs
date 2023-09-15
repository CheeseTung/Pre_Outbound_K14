using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class ProgramLog
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string LogPath { get; set; } = null!;
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Program Program { get; set; } = null!;
    }
}
