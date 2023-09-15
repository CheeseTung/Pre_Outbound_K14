using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using FPT_Education_IC.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace FPT_Education_IC.Controllers
{
    [Route("Management")]
    public class ManagementController : Controller
    {
        private readonly ILogger<ManagementController> _logger;
        private readonly DataContext _context;

        public ManagementController(ILogger<ManagementController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("AccountManageList")]
        [Authorize]
        public IActionResult AccountManageList()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            return View();
        }

        [Route("AddManageAccount")]
        [Authorize]
        public IActionResult AddManageAccount(string accountIdUpdate)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(accountIdUpdate))
            {
                int usrAccountId = int.Parse(accountIdUpdate);
                ViewBag.UsrAccountId = usrAccountId;
            }
            return View();
        }

        [Route("GetAccountMangeCampus")]
        [Authorize]
        public IActionResult GetAccountMangeCampus(string code)
        {
            AccountService accountService = new AccountService(_context);
            ArrayList listAccountByCampus = accountService.GetListAccountManageByCampus(int.Parse(code));

            return Json(new { success = true, list = listAccountByCampus });
        }

        [Route("CheckEmailExist")]
        [Authorize]
        [HttpGet]
        public IActionResult CheckEmailExist(string email)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            AccountService accountService = new AccountService(_context);
            string emailExists = accountService.checkEmailExist(email, emrSession.userId);

            return Json(emailExists);
        }

        [Route("UpdateUsrRole")]
        [Authorize]
        [HttpPost]
        public ActionResult UpdateUsrRole(string userEmail, string userRole)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            if (emrSession.userEmail.Equals(userEmail))
            {
                return Json(new { success = false, message = "Bạn không thể phân quyền vai trò của bạn. Hãy liên hệ với quản trị viên hệ thống." });
            }
            if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userRole))
            {
                ManagementService managementService = new ManagementService(_context);
                managementService.UpdateUsrRole(userEmail, userRole, emrSession.userId, DateTime.Now);
                return Json(new { success = true, message = "Đã phân quyền thành công cho " + userEmail });
            }

            return Json(new { success = false });
        }

        [Route("RemoveManageAccount")]
        [Authorize]
        [HttpPost]
        public ActionResult RemoveManageAccount(string accountIdUpdate)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            if (!string.IsNullOrEmpty(accountIdUpdate))
            {
                int userIdRemove = int.Parse(accountIdUpdate);
                if (emrSession.userId == userIdRemove)
                {
                    return Json(new { success = false, message = "Bạn không thể hủy phân quyền vai trò của bạn. Hãy liên hệ với quản trị viên hệ thống." });
                }
                AccountService accountService = new AccountService(_context);
                ManagementService managementService = new ManagementService(_context);
                managementService.RemoveUsrRole(userIdRemove, emrSession.userId, DateTime.Now);
                return Json(new { success = true, message = "Đã hủy phân quyền thành công cho " + accountService.getUsrEmailById(userIdRemove)});

            }

            return Json(new { success = false });
        }
    }
}
