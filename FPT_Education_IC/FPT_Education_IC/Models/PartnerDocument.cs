using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class PartnerDocument
    {
        public int DocumentId { get; set; }
        public int PartnerId { get; set; }
        public int? ProgramId { get; set; }
        public string Type { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Partner Partner { get; set; } = null!;
        public virtual Program? Program { get; set; }
    }
}
