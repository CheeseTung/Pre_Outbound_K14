using System;
using System.Collections.Generic;

namespace FPT_Education_IC.ViewModels
{
    public class UsrStudyLog
    {
    public string ProgramName { get; set; }
    public string CourseName { get; set; }
    public string FPTCourse { get; set; }
    public decimal MaxMark { get; set; }
    public decimal ResultMark { get; set; }
    public decimal PassMark { get; set; }
    public string Note { get; set; }
    public bool Status { get; set; }

    public DateTime ProgramEndDate { get; set; }
    }
}
