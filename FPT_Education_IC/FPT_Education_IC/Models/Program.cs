using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class Program
    {
        public Program()
        {
            Feedbacks = new HashSet<Feedback>();
            PartnerDocuments = new HashSet<PartnerDocument>();
            PaymentLogs = new HashSet<PaymentLog>();
            ProgramCooperations = new HashSet<ProgramCooperation>();
            ProgramLogs = new HashSet<ProgramLog>();
            ProgramPosts = new HashSet<ProgramPost>();
            RegisterQuestions = new HashSet<RegisterQuestion>();
            Registers = new HashSet<Register>();
            StudyExchanges = new HashSet<StudyExchange>();
        }

        public int ProgramId { get; set; }
        public string Type { get; set; } = null!;
        public string Categorize { get; set; } = null!;
        public int? IsStudyExchange { get; set; }
        public int? UserId { get; set; }
        public string? Country { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Participants { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? FaceBookLink { get; set; }
        public DateTime RegisterStartDate { get; set; }
        public DateTime RegisterEndDate { get; set; }
        public decimal? PaymentValue { get; set; }
        public DateTime? PaymentStartDate { get; set; }
        public DateTime? PaymentEndDate { get; set; }
        public string? PaymentDescription { get; set; }
        public string? Image { get; set; }
        public int Status { get; set; }
        public int UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual UsrAccount? User { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<PartnerDocument> PartnerDocuments { get; set; }
        public virtual ICollection<PaymentLog> PaymentLogs { get; set; }
        public virtual ICollection<ProgramCooperation> ProgramCooperations { get; set; }
        public virtual ICollection<ProgramLog> ProgramLogs { get; set; }
        public virtual ICollection<ProgramPost> ProgramPosts { get; set; }
        public virtual ICollection<RegisterQuestion> RegisterQuestions { get; set; }
        public virtual ICollection<Register> Registers { get; set; }
        public virtual ICollection<StudyExchange> StudyExchanges { get; set; }
    }
}
