using FPT_Education_IC.Data;
using FPT_Education_IC.Statics;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FPT_Education_IC.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly DataContext _context;

        public AccountController(ILogger<AccountController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login(string message, string ReturnUrl)
        {

            var campusList = _context.FptCampuses.ToList();
            ViewBag.CampusList = campusList;
            ViewBag.Message = message;
            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                ViewBag.ReturnUrl = ReturnUrl;
            }
            return View();
        }

        [Route("google-login")]
        [AllowAnonymous]
        public IActionResult GoogleLogin(int campusCode, string ReturnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("LoginCallback", new { campusCode = campusCode, returnUrl = ReturnUrl })
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);

        }

        [Route("LoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCallback(int campusCode, string ReturnUrl)
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var errorMessage = "";
            if (!result.Succeeded)
            {
                errorMessage = "Đăng nhập không thành công! Vui lòng kiểm tra thông tin đăng nhập";
                return RedirectToAction("Login", "Account", new { message = errorMessage });
            }

            var emailClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            var nameClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            var imgUrlClaim = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Uri);
            string usrEmail = "";
            string usrName = "";
            string usrImage = "";
            var userAccount = new UsrAccount();

            if (emailClaim != null)
            {
                usrEmail = emailClaim.Value;
                usrName = nameClaim?.Value;
                usrImage = imgUrlClaim?.Value;
                // check user in DB
                userAccount = _context.UsrAccounts.FirstOrDefault(x => x.Email.Equals(usrEmail));
                // if not have user in DB => add new user
                if (userAccount == null)
                {
                    AccountService accountService = new AccountService(_context);
                    userAccount = accountService.addNewAccount(usrEmail, usrName, usrImage, campusCode);
                    SystemsService systemsService = new SystemsService(_context);
                    systemsService.CreateNotification(userAccount.UserId, 1, "Chào mừng bạn đến với hệ thống FPT Education International Cooperation của Đại học FPT.", userAccount.UserId);
                } else
                {
                    if (!StaticsData.SUPER_ADMIN_ROLE.Equals(userAccount.Role) && campusCode != userAccount.Campus)
                    {
                        CampusService campusService = new CampusService(_context);
                        var campusName = campusService.getNameByCode(campusCode);
                        errorMessage = "Tài khoản của bạn không thể đăng nhập campus " + campusName;
                        return RedirectToAction("Login", "Account", new { message = errorMessage });
                    }
                }
            }
            else
            {
                errorMessage = "Không thể đăng nhập bằng tài khoản của bạn. Vui lòng kiểm tra lại email";
                return RedirectToAction("Login", "Account", new { message = errorMessage });
            }

            // put Session
            var identifier = result.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            EmrSession emrSession = new EmrSession(HttpContext);
            emrSession.userId = userAccount.UserId;
            emrSession.userIdIdentity = identifier?.Value;
            emrSession.userRole = userAccount.Role;
            emrSession.userEmail = userAccount.Email;
            emrSession.userName = userAccount.UserName;
            emrSession.userImage = userAccount.Image;
            emrSession.putSession(HttpContext);

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl); // Chuyển hướng đến returnUrl
            }

            // Redirect to Homepage
            return RedirectToAction("HomePage", "Home");
        }

        [Route("AccountProfile")]
        [Authorize]
        public IActionResult AccountProfile()
        {
            if (User.Identity.IsAuthenticated && HttpContext != null)
            {
                EmrSession emrSession = new EmrSession(HttpContext);
                ViewBag.EmrSession = emrSession;
                if (emrSession != null && !string.IsNullOrEmpty(emrSession.userEmail))
                {
                    var userAccount = new UsrAccount();
                    userAccount = _context.UsrAccounts.FirstOrDefault(x => x.Email.Equals(emrSession.userEmail));
                    if (userAccount != null)
                    {
                        ViewBag.UserAccount = userAccount;
                        CampusService campusService = new CampusService(_context);
                        ViewBag.CampusName = campusService.getNameByCode(userAccount.Campus);
                    }
                }
                RouterMapping mapping = new RouterMapping();
                mapping.AddService(HttpContext, this, _context);
            }
            return View();
        }

        [Route("UpdateUserProfile")]
        [HttpPost]
        [Authorize]
        public IActionResult UpdateUserProfile(IFormFile usrImg)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            AccountService accountService = new AccountService(_context);
            UsrAccount usrAccount = accountService.GetUsrAccountById(emrSession.userId);

            string usrName = Request.Form["usrName"];
            string usrRollNumber = Request.Form["usrRollNumber"];
            string usrDOB = Request.Form["usrDOB"];
            int usrGender = int.Parse(Request.Form["usrGender"]);
            string usrPhone = Request.Form["usrPhone"];
            string usrMajor = Request.Form["usrMajor"];
            string IdNumber = Request.Form["IdNumber"];
            string IdNumberStDate = Request.Form["IdNumberStDate"];
            string passportNum = Request.Form["passportNum"];
            string passportStDate = Request.Form["passportStDate"];
            string passportEndDate = Request.Form["passportEndDate"];
            string linkFacebook = Request.Form["linkFacebook"];

            string usrImagePath = "";
            var imagePath = "";
            string oldImg = usrAccount.Image;
            if (usrImg != null && usrImg.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                imagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile");
                Directory.CreateDirectory(imagePath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"profile_{usrAccount.UserId}_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(usrImg.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                imagePath = Path.Combine(imagePath, fileName);

                usrImagePath = $"/assets/img/account/profile/{fileName}";
            }

            accountService.UpdateUsrAccount(emrSession.userId, usrName, usrImagePath, usrGender, usrDOB, usrMajor,
                    usrRollNumber, usrPhone, IdNumber, IdNumberStDate, passportNum, passportStDate, passportEndDate, linkFacebook);

            usrAccount = accountService.GetUsrAccountById(emrSession.userId);
            if (usrAccount != null)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        usrImg.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "account", "profile", Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }
                return Json(new { success = true });
            }


            return Json(new { success = false });
        }

        [Route("AccountBilling")]
        [Authorize]
        public IActionResult AccountBilling()
        {
            if (User.Identity.IsAuthenticated && HttpContext != null)
            {
                EmrSession emrSession = new EmrSession(HttpContext);
                ViewBag.EmrSession = emrSession;
                if (emrSession != null && !string.IsNullOrEmpty(emrSession.userEmail))
                {
                    var userAccount = new UsrAccount();
                    userAccount = _context.UsrAccounts.FirstOrDefault(x => x.Email.Equals(emrSession.userEmail));
                    if (userAccount != null)
                    {
                        ViewBag.UserAccount = userAccount;
                        CampusService campusService = new CampusService(_context);
                        ViewBag.CampusName = campusService.getNameByCode(userAccount.Campus);
                    }
                }
                RouterMapping mapping = new RouterMapping();
                mapping.AddService(HttpContext, this, _context);
            }
            return View();
        }

        [Route("AccountResultStudy")]
        [Authorize]
        public IActionResult AccountResultStudy()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("UserNotification")]
        [Authorize]
        public IActionResult UserNotification()
        {
            if (User.Identity.IsAuthenticated && HttpContext != null)
            {
                EmrSession emrSession = new EmrSession(HttpContext);
                ViewBag.EmrSession = emrSession;
                if (emrSession != null && !string.IsNullOrEmpty(emrSession.userEmail))
                {
                    var userAccount = new UsrAccount();
                    userAccount = _context.UsrAccounts.FirstOrDefault(x => x.Email.Equals(emrSession.userEmail));
                    if (userAccount != null)
                    {
                        ViewBag.UserAccount = userAccount;
                        CampusService campusService = new CampusService(_context);
                        ViewBag.CampusName = campusService.getNameByCode(userAccount.Campus);
                    }
                }
                RouterMapping mapping = new RouterMapping();
                mapping.AddService(HttpContext, this, _context);
            }
            return View();
        }

        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            EmrSession emrSession = new EmrSession(HttpContext);
            emrSession.clearSession(HttpContext);
            return RedirectToAction("HomePage", "Home");
        }
    }
}
