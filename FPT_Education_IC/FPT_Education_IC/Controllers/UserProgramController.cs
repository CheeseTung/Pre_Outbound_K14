using FPT_Education_IC.Data;
using FPT_Education_IC.Service;
using FPT_Education_IC.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Education_IC.Controllers
{
    [Route("UserProgram")]
    public class UserProgramController : Controller
    {
        private readonly ILogger<UserProgramController> _logger;
        private readonly DataContext _context;

        public UserProgramController(ILogger<UserProgramController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ViewRegisterProgram")]
        [Authorize]
        public ActionResult ViewRegisterProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("ViewParticipationProgram")]
        [Authorize]
        public ActionResult ViewParticipationProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("ViewParticipatingProgram")]
        [Authorize]
        public ActionResult ViewParticipatingProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("AddProgramFeedback")]
        [Authorize]
        public ActionResult AddProgramFeedback(string programId, string userId)
        {
            if (!string.IsNullOrEmpty(programId) && !string.IsNullOrEmpty(userId))
            {
                int programID = int.Parse(programId);
                int userID = int.Parse(userId);

                int programContent = int.Parse(Request.Form["programContent"]);
                int partnerFacilities = int.Parse(Request.Form["partnerFacilities"]);
                int partnerSupport = int.Parse(Request.Form["partnerSupport"]);
                int extracurricular = int.Parse(Request.Form["extracurricular"]);
                int staffSupport = int.Parse(Request.Form["staffSupport"]);
                string feedbackMore = Request.Form["feedbackMore"];

                RegisterService registerService = new RegisterService(_context);
                registerService.AddProgramFeedBack(programID, userID, programContent, partnerFacilities, partnerSupport, extracurricular, staffSupport, feedbackMore, DateTime.Now);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}
