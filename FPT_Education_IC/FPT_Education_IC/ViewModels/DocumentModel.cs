using System;
using System.Collections.Generic;

namespace FPT_Education_IC.ViewModels
{
    public class DocumentModel
    {
        public int documentId { get; set; }
        public string Docname { get; set; }
        public int PartnerId { get; set; }
        public string Partname { get; set; }
        public int? ProgramId { get; set; }
        public string Programname { get; set; }
        public string Type { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int Status { get; set; }

        public string filePath { get; set; }
    }

 
}
