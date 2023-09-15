using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using FPT_Education_IC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FPT_Education_IC.Service;
using FPT_Education_IC.Statics;
using System.Collections;

namespace FPT_Education_IC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [AllowAnonymous]
        public IActionResult HomePage(int page = 1)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            int itemsPerPage = 6; // Số lượng chương trình hiển thị trên mỗi trang
            ProgramsService programsService = new ProgramsService(_context);
            ArrayList listAllProgram = programsService.getAllPrograms();

            var programList = new List<Models.Program>();
            foreach (var item in listAllProgram)
            {
                if (item is Models.Program program)
                {
                    programList.Add(program);
                }
            }

            var totalPages = (int)Math.Ceiling((double)programList.Count / itemsPerPage);

            if (page < 1)
            {
                page = 1;
            }
            else if (page > totalPages)
            {
                page = totalPages;
            }

            var programsOnPage = programList.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            var viewModel = new ProgramViewModel
            {
                Programs = programsOnPage,
                CurrentPage = page,
                TotalPages = totalPages
            };
            ViewBag.ListProgramView = viewModel;


            return View();
        }

        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            var data = _context.FptCampuses.ToList();
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}