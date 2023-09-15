namespace FPT_Education_IC.ViewModels
{
    public class ViewDashBoard
    {
        public int count { get; set; }
        public int programId { get; set; }
        public string programCategory { get; set; }
        public string programName { get; set; }
        public int numberRegister { get; set; }
        public int numberAccept { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string[] country { get; set; }
        public string[] partner { get; set; }
        public int status { get; set; }
    }
}
