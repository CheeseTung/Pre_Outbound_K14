using System;
using System.Collections.Generic;

namespace FPT_Education_IC.ViewModels
{
    public class UsrPaymentLog
    {
    public int logId { get; set; }
    public string programName { get; set; }
    public decimal paymentValue { get; set; }
    public DateTime paymentDate { get; set; }
    public DateTime? paymentEndDate { get; set; }
    public int status { get; set; }
    }
}
