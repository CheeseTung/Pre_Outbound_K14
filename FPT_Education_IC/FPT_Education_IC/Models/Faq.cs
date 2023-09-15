using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Faq
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public DateTime? UpdateDate { get; set; }
        public int UpdateUser { get; set; }
    }
}
