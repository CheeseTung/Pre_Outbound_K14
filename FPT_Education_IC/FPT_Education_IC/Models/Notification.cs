using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int NotiType { get; set; }
        public string NotiContent { get; set; } = null!;
        public int ReceiverUsr { get; set; }
        public int? UpdateUsr { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
