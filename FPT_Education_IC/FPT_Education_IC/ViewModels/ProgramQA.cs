namespace FPT_Education_IC.ViewModels
{
    public class ProgramQA
    {
        public int QuestionId { get; set; }
        public int ProgramId { get; set; }
        public int QuestionType { get; set; }
        public string QuestionContent { get; set; } = null!;
        public int AnswerId { get; set; }
        public int RegisterId { get; set; }
        public string? AnswerContent { get; set; }
    }
}
