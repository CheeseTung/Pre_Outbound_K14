using System;
using System.Collections.Generic;

namespace FPT_Education_IC.ViewModels
{
    public class ProgramViewModel
    {
        public List<Models.Program> Programs { get; set; } // Danh sách chương trình cho trang hiện tại
        public int CurrentPage { get; set; } // Trang hiện tại
        public int TotalPages { get; set; } // Tổng số trang
    }
}
