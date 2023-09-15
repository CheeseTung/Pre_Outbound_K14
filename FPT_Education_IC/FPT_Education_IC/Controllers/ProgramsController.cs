using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using FPT_Education_IC.Statics;
using FPT_Education_IC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using System.Collections;
using System.Text;

namespace FPT_Education_IC.Controllers
{
    [Route("Programs")]
    public class ProgramsController : Controller
    {

        private readonly ILogger<ProgramsController> _logger;
        private readonly DataContext _context;

        public ProgramsController(ILogger<ProgramsController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ViewProgramDetails")]
        [AllowAnonymous]
        public IActionResult ViewProgramDetails(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }

            return View("ProgramDetails");
        }

        [Route("ViewProgramManage")]
        [Authorize]
        public IActionResult ViewProgramManage(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }

            return View();
        }

        [Route("CreateProgram")]
        [Authorize]
        public IActionResult CreateProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("UpdateProgram")]
        [Authorize]
        public IActionResult UpdateProgram(string programId)
        {
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("ListManageProgram")]
        [Authorize]
        public IActionResult ListManageProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("ListRequestPrograms")]
        [Authorize]
        public IActionResult ListRequestPrograms()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("ManageParticipatingProgram")]
        [Authorize]
        public IActionResult ManageParticipatingProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("ManageParticipationProgram")]
        [Authorize]
        public IActionResult ManageParticipationProgram()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("AddManageProgram")]
        [Authorize]
        public IActionResult AddManageProgram(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            int programID = -1;
            if (!string.IsNullOrEmpty(programId))
            {
                programID = int.Parse(programId);
            }
            ViewBag.ProgramID = programID;

            return View();
        }

        [Route("UpdateManageProgram")]
        [Authorize]
        public IActionResult UpdateManageProgram(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            int programID = -1;
            if (!string.IsNullOrEmpty(programId))
            {
                programID = int.Parse(programId);
            }
            ViewBag.ProgramID = programID;

            return View();
        }

        [Route("ListApproveProgram")]
        [Authorize]
        public IActionResult ListApproveProgram(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }

            return View();
        }

        [Route("ManageRegisterStudy")]
        [Authorize]
        public IActionResult ManageRegisterStudy(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }

            return View();

        }

        
        [Route("ManageRegisterFeedback")]
        [Authorize]
        public IActionResult ManageRegisterFeedback(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }

            return View();

        }

        [Route("ProgramInformation")]
        [Authorize]
        public IActionResult ProgramInformation(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }

            return View();
        }

        [Route("RegisterProgram")]
        [Authorize]
        public IActionResult RegisterProgram(string programId)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            if (string.IsNullOrEmpty(emrSession.userIdIdentity) || string.IsNullOrEmpty(emrSession.userRole))
            {
                return Challenge();
            }
            else if (!emrSession.userRole.Equals(StaticsData.DEFAULT_ROLE))
            {
                return RedirectToAction("HomePage", "Home");
            }
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("DoRegisterProgram")]
        [Authorize]
        public IActionResult DoRegisterProgram(string[] answerQuestion, string[] questionID)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            AccountService accountService = new AccountService(_context);
            UsrAccount usrAccount = accountService.GetUsrAccountById(emrSession.userId);
            string programId = Request.Form["programId"];
            string userName = Request.Form["usrName"];
            string usrMajor = Request.Form["usrMajor"];
            string usrRollNumber = Request.Form["usrRollNumber"];
            if (string.IsNullOrEmpty(usrRollNumber))
            {
                usrRollNumber = emrSession.userEmail.Split("@")[0];
            }
            string usrDOB = Request.Form["usrDOB"];
            int usrGender = int.Parse(Request.Form["usrGender"]);
            string usrPhone = Request.Form["usrPhone"];
            string IdNumber = Request.Form["IdNumber"];
            string IdNumberStDate = Request.Form["IdNumberStDate"];
            string passportNum = Request.Form["passportNum"];
            string passportStDate = Request.Form["passportStDate"];
            string passportEndDate = Request.Form["passportEndDate"];
            string linkFacebook = Request.Form["linkFacebook"];
            string partnerId = Request.Form["partnerId"];

            if (!string.IsNullOrEmpty(programId) || !programId.Equals("null"))
            {
                int programID = int.Parse(programId);

                accountService.UpdateUsrAccount(emrSession.userId, userName,
                    usrAccount.Image, usrGender, usrDOB, usrMajor,
                    usrRollNumber, usrPhone, IdNumber, IdNumberStDate, passportNum, passportStDate, passportEndDate, linkFacebook);

                RegisterService registerService = new RegisterService(_context);
                int registerID = registerService.addRegisterService(programID, emrSession.userId, partnerId);
                if (answerQuestion != null && answerQuestion.Length > 0 && questionID != null && questionID.Length > 0)
                {
                    registerService.AddAnswerRegister(emrSession.userId, questionID, answerQuestion);
                }

                ProgramsService programsService = new ProgramsService(_context);
                string logFileName = programsService.GetProgramLogFileName(programID);
                string messageLog = "Đã đăng ký tham gia chương trình";
                StaticsData.LogToXml(messageLog, logFileName, emrSession.userName);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("UpdateUserRegister")]
        [Authorize]
        public IActionResult UpdateUserRegister(string[] answerQuestion, string[] questionID, bool re_register)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            AccountService accountService = new AccountService(_context);
            UsrAccount usrAccount = accountService.GetUsrAccountById(emrSession.userId);
            string registerId = Request.Form["registerId"];
            string userName = Request.Form["usrName"];
            string usrMajor = Request.Form["usrMajor"];
            string usrRollNumber = Request.Form["usrRollNumber"];
            if (string.IsNullOrEmpty(usrRollNumber))
            {
                usrRollNumber = emrSession.userEmail.Split("@")[0];
            }
            string usrDOB = Request.Form["usrDOB"];
            int usrGender = int.Parse(Request.Form["usrGender"]);
            string usrPhone = Request.Form["usrPhone"];
            string IdNumber = Request.Form["IdNumber"];
            string IdNumberStDate = Request.Form["IdNumberStDate"];
            string passportNum = Request.Form["passportNum"];
            string passportStDate = Request.Form["passportStDate"];
            string passportEndDate = Request.Form["passportEndDate"];
            string linkFacebook = Request.Form["linkFacebook"];
            string partnerId = Request.Form["partnerId"];

            if (!string.IsNullOrEmpty(registerId) || !registerId.Equals("null"))
            {
                int registerID = int.Parse(registerId);

                accountService.UpdateUsrAccount(emrSession.userId, userName,
                    usrAccount.Image, usrGender, usrDOB, usrMajor,
                    usrRollNumber, usrPhone, IdNumber, IdNumberStDate, passportNum, passportStDate, passportEndDate, linkFacebook);

                RegisterService registerService = new RegisterService(_context);
                registerService.UpdateUsrRegister(registerID, emrSession.userId, partnerId);
                if (re_register)
                {
                    registerService.UpdateRegisterStatus(registerID, emrSession.userId, StaticsData.REGISTER_REQUEST, null, null);
                }

                if (answerQuestion != null && answerQuestion.Length > 0 && questionID != null && questionID.Length > 0)
                {
                    registerService.UpdateAnswerRegister(emrSession.userId, questionID, answerQuestion);
                }
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("ViewRegisterProgram")]
        [Authorize]
        public IActionResult ViewRegisterProgram(string programId, string registerId, bool re_register)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(registerId))
            {
                int id = int.Parse(registerId);
                ViewBag.RegisterId = id;
                if (re_register)
                {
                    ViewBag.Re_register = true;
                }
                return View();
            }

            return RedirectToAction("ViewProgramDetails", "Programs", new { programId = programId });
        }

        [Route("CancelRegisterProgram")]
        [Authorize]
        public IActionResult CancelRegisterProgram(string programId, string registerId, string reasonCalcel)
        {
            if (!string.IsNullOrEmpty(registerId))
            {
                int id = int.Parse(registerId);
                EmrSession emrSession = new EmrSession(HttpContext);
                RegisterService registerService = new RegisterService(_context);
                registerService.UpdateRegisterStatus(id, emrSession.userId, StaticsData.REGISTER_CANCEL, null, reasonCalcel);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("UpdateListProgramCategorize")]
        [Authorize]
        public IActionResult UpdateListProgramCategorize(string code)
        {
            SystemsService systemsService = new SystemsService(_context);
            ArrayList listProgramCategorize = systemsService.GetListProgramCategorize(code);

            return Json(new { success = true, list = listProgramCategorize });

        }

        [Route("GetPartnersByCountry")]
        [HttpPost]
        [Authorize]
        public IActionResult GetPartnersByCountry(string country)
        {
            PartnerService partnerService = new PartnerService(_context);
            ArrayList listPartnerByCountry = partnerService.GeListPartnerByCountry(country);

            return Json(new { success = true, list = listPartnerByCountry });
        }

        [Route("GetContactsByPartner")]
        [HttpPost]
        [Authorize]
        public IActionResult GetContactsByPartner(string partnerId)
        {
            ArrayList listContactsByPartner = new ArrayList();
            if (!string.IsNullOrEmpty(partnerId) && partnerId != "null")
            {
                int pid = int.Parse(partnerId.Trim());
                PartnerService partnerService = new PartnerService(_context);
                listContactsByPartner = partnerService.GetListContactPartner(pid);
            }

            return Json(new { success = true, list = listContactsByPartner });
        }

        [Route("CreateNewProgram")]
        [Authorize]
        public IActionResult CreateNewProgram(IFormFile programImg, string programPartnerList, string registerQuestionList, string programSubjectList)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            string programImagePath = "";
            var imagePath = "";
            if (programImg != null && programImg.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                imagePath = Path.Combine("wwwroot", "assets", "img", "program");
                Directory.CreateDirectory(imagePath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"program_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(programImg.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                imagePath = Path.Combine(imagePath, fileName);

                programImagePath = $"/assets/img/program/{fileName}";
            }

            int exchangeData = int.Parse(Request.Form["exchangeData"]);
            string programCategorize = Request.Form["programCategorize"];
            if (exchangeData == 0)
            {
                programCategorize = "OTHER";
            }
            string programCountry = Request.Form["programCountry"];
            if (string.IsNullOrEmpty(programCountry) || "null".Equals(programCountry))
            {
                programCountry = null;
            }
            string programTitle = Request.Form["programTitle"];
            string participants = Request.Form["participants"];
            string facebookLink = Request.Form["facebookLink"];
            DateTime startDate = DateTime.Parse(Request.Form["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Form["endDate"]);
            DateTime registStDate = DateTime.Parse(Request.Form["registStDate"]);
            DateTime registEndDate = DateTime.Parse(Request.Form["registEndDate"]);
            string paymentValue = Request.Form["paymentValue"];
            string paymentDesc = Request.Form["paymentDesc"];
            DateTime paymentStDate = DateTime.Parse(Request.Form["paymentStDate"]);
            DateTime paymentEndDate = DateTime.Parse(Request.Form["paymentEndDate"]);
            string programDescValue = Request.Form["programDescValue"];
            List<string> programPartnerList_Value = new List<string>();
            if (!string.IsNullOrEmpty(programPartnerList))
            {
                programPartnerList_Value = programPartnerList.Split(',').ToList();

            }
            List<string> registerQuestionList_Value = new List<string>();
            if (!string.IsNullOrEmpty(registerQuestionList))
            {
                StringBuilder sb = new StringBuilder(registerQuestionList);
                sb.Remove(sb.Length - 1, 1); // Xóa ký tự cuối cùng
                registerQuestionList = sb.ToString();
                registerQuestionList_Value = registerQuestionList.Split(new string[] { "|," }, StringSplitOptions.None).ToList();
            }
            List<string> programSubjectList_Value = new List<string>();
            if (!string.IsNullOrEmpty(programSubjectList))
            {
                programSubjectList_Value = programSubjectList.Split(',').ToList();

            }
            int usrIdUpdate = emrSession.userId;
            DateTime updateDate = DateTime.Now;

            ProgramsService programsService = new ProgramsService(_context);
            int reccentProgramID = programsService.AddNewProgram(exchangeData, programCategorize, programCountry, programTitle, participants, facebookLink, startDate, endDate, registStDate, registEndDate,
                                          paymentValue, paymentDesc, paymentStDate, paymentEndDate, programDescValue, programImagePath, usrIdUpdate, updateDate);
            if (reccentProgramID > -1)
            {
                if (programImg != null && programImg.Length > 0 && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        programImg.CopyTo(stream);
                    }
                }
                programsService.AddPartnerProgram(reccentProgramID, programPartnerList_Value);
                programsService.AddResgisterQuestionProgram(reccentProgramID, 0, registerQuestionList_Value);
                if (exchangeData == 1)
                {
                    programsService.AddSubjectToStudyProgram(reccentProgramID, programSubjectList_Value);
                }
                string logFileName = $"program_{reccentProgramID}_{DateTime.Now:yyyyMMdd_HHmmss}";
                string messageLog = "Đã tạo chương trình";
                StaticsData.LogToXml(messageLog, logFileName, emrSession.userName);

                programsService.AddNewProgramLog(reccentProgramID, logFileName, usrIdUpdate, updateDate);
                
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("DoUpdateProgram")]
        [Authorize]
        public IActionResult DoUpdateProgram(IFormFile programImg, string programId, string programPartnerList, string registerQuestionList, string programSubjectList)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            string programImagePath = "";
            var imagePath = "";
            if (string.IsNullOrEmpty(programId))
            {
                return Json(new { success = false });
            }
            if (programImg != null && programImg.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                imagePath = Path.Combine("wwwroot", "assets", "img", "program");
                Directory.CreateDirectory(imagePath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"program_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(programImg.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                imagePath = Path.Combine(imagePath, fileName);

                programImagePath = $"/assets/img/program/{fileName}";
            }
            int programID = int.Parse(Request.Form["programId"]);
            ProgramsService programsService = new ProgramsService(_context);
            Models.Program oldProgram = programsService.getProgramById(programID);
            string oldImg = oldProgram.Image;
            int exchangeData = int.Parse(Request.Form["exchangeData"]);
            string programCategorize = Request.Form["programCategorize"];
            string programCountry = Request.Form["programCountry"];
            if (string.IsNullOrEmpty(programCountry) || "null".Equals(programCountry))
            {
                programCountry = null;
            }
            string programTitle = Request.Form["programTitle"];
            string participants = Request.Form["participants"];
            string facebookLink = Request.Form["facebookLink"];
            DateTime startDate = DateTime.Parse(Request.Form["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Form["endDate"]);
            DateTime registStDate = DateTime.Parse(Request.Form["registStDate"]);
            DateTime registEndDate = DateTime.Parse(Request.Form["registEndDate"]);
            string paymentValue = Request.Form["paymentValue"];
            string paymentDesc = Request.Form["paymentDesc"];
            DateTime paymentStDate = DateTime.Parse(Request.Form["paymentStDate"]);
            DateTime paymentEndDate = DateTime.Parse(Request.Form["paymentEndDate"]);
            string programDescValue = Request.Form["programDescValue"];
            List<string> programPartnerList_Value = new List<string>();
            if (!string.IsNullOrEmpty(programPartnerList))
            {
                programPartnerList_Value = programPartnerList.Split(',').ToList();

            }
            List<string> registerQuestionList_Value = new List<string>();
            if (!string.IsNullOrEmpty(registerQuestionList))
            {
                StringBuilder sb = new StringBuilder(registerQuestionList);
                sb.Remove(sb.Length - 1, 1); // Xóa ký tự cuối cùng
                registerQuestionList = sb.ToString();
                registerQuestionList_Value = registerQuestionList.Split(new string[] { "|," }, StringSplitOptions.None).ToList();
            }
            List<string> programSubjectList_Value = new List<string>();
            if (!string.IsNullOrEmpty(programSubjectList))
            {
                programSubjectList_Value = programSubjectList.Split(',').ToList();

            }
            int usrIdUpdate = emrSession.userId;
            DateTime updateDate = DateTime.Now;

            int reccentProgramID = programsService.UpdateProgram(programID, exchangeData, programCategorize, programCountry, programTitle, participants, facebookLink,
                startDate, endDate, registStDate, registEndDate, paymentValue, paymentDesc, paymentStDate, paymentEndDate, programDescValue, programImagePath, usrIdUpdate, updateDate);
            if (reccentProgramID > -1)
            {
                if (programImg != null && programImg.Length > 0 && !string.IsNullOrEmpty(imagePath))
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        programImg.CopyTo(stream);
                    }

                    if (!string.IsNullOrEmpty(oldImg))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "program", Path.GetFileName(oldImg));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }
                programsService.UpdatePartnerProgram(reccentProgramID, programPartnerList_Value);
                //programsService.AddResgisterQuestionProgram(reccentProgramID, 0, registerQuestionList_Value);
                //if (exchangeData == 1)
                //{
                //    programsService.AddSubjectToStudyProgram(reccentProgramID, programSubjectList_Value);
                //}
                string logFileName = programsService.GetProgramLogFileName(programID);
                string messageLog = "Đã cập nhật chương trình";
                StaticsData.LogToXml(messageLog, logFileName, emrSession.userName);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        
        [Route("UpdateRegisterStudy")]
        [Authorize]
        public IActionResult UpdateRegisterStudy(string exchangeId, string registerStudyList)
        {
            if (!string.IsNullOrEmpty(exchangeId) && !string.IsNullOrEmpty(registerStudyList))
            {
                int exChang_Id = int.Parse(exchangeId);
                List<string> registerStudyList_Value = new List<string>();
                if (!string.IsNullOrEmpty(registerStudyList))
                {
                    registerStudyList_Value = registerStudyList.Split(',').ToList();

                }
                ProgramsService programsService = new ProgramsService(_context);
                EmrSession emrSession = new EmrSession(HttpContext);
                programsService.UpdateRegisterStudy(exChang_Id, registerStudyList_Value, emrSession.userId);

                return Json(new { success = true });
            }

            return Json(new { success = false });

        }


        [Route("ApproveProgram")]
        [Authorize]
        public IActionResult ApproveProgram(string programId, string programManageList)
        {
            if (!string.IsNullOrEmpty(programId))
            {
                EmrSession emrSession = new EmrSession(HttpContext);
                if (emrSession.userRole.Equals(StaticsData.ADMIN_ROLE) || emrSession.userRole.Equals(StaticsData.SUPER_ADMIN_ROLE))
                {
                    int prID = int.Parse(programId);
                    ProgramsService programsService = new ProgramsService(_context);
                    
                    List<string> programManageList_Value = new List<string>();
                    if (!string.IsNullOrEmpty(programManageList))
                    {
                        programManageList_Value = programManageList.Split(',').ToList();

                    }
                    programsService.ApproveProgram(prID, programManageList_Value, emrSession.userId, DateTime.Now);
                    string logFileName = programsService.GetProgramLogFileName(prID);
                    string messageLog = "Đã phê duyệt chương trình";
                    StaticsData.LogToXml(messageLog, logFileName, emrSession.userName);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true }, new { massage = "Không có quyền" });
                }
            }

            return Json(new { success = false });
        }

        [Route("UpdateApproveProgram")]
        [Authorize]
        public IActionResult UpdateApproveProgram(string programId, string programManageList)
        {
            if (!string.IsNullOrEmpty(programId))
            {
                EmrSession emrSession = new EmrSession(HttpContext);
                int prID = int.Parse(programId);
                ProgramsService programsService = new ProgramsService(_context);

                List<string> programManageList_Value = new List<string>();
                if (!string.IsNullOrEmpty(programManageList))
                {
                    programManageList_Value = programManageList.Split(',').ToList();

                }
                programsService.UpdateApproveProgram(prID, programManageList_Value, emrSession.userId, DateTime.Now);
                string logFileName = programsService.GetProgramLogFileName(prID);
                string messageLog = "Đã cập nhật quản lý chương trình";
                StaticsData.LogToXml(messageLog, logFileName, emrSession.userName);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("UpdateRegisterStatus")]
        [Authorize]
        public IActionResult UpdateRegisterStatus(string registerId, string registerStatus, string requestChange)
        {
            if (!string.IsNullOrEmpty(registerId) && !string.IsNullOrEmpty(registerStatus))
            {
                int id = int.Parse(registerId);
                int status = int.Parse(registerStatus);
                string rqChange = "";
                string rqCancel = "";
                if (status == StaticsData.REGISTER_PENDING)
                {
                    rqChange = requestChange;
                } else if (status == StaticsData.REGISTER_CANCEL)
                {
                    rqCancel = requestChange;
                }
                EmrSession emrSession = new EmrSession(HttpContext);
                RegisterService registerService = new RegisterService(_context);
                registerService.UpdateRegisterStatus(id, emrSession.userId, status, rqChange, rqCancel);

                return Json(new { success = true });

            }

            return Json(new { success = false });
        }

        
        [Route("UpdateRegisterPayment")]
        [Authorize]
        public IActionResult UpdateRegisterPayment(string programId, string registerId, string registerStatus, string Value, string Date)
        {
            if (!string.IsNullOrEmpty(registerId) && !string.IsNullOrEmpty(programId) && !string.IsNullOrEmpty(Value))
            {
                int id = int.Parse(registerId);
                int prmId = int.Parse(programId);
                int status = int.Parse(registerStatus);
                Decimal paymentValue = Decimal.Parse(Value);
                DateTime paymentDate = DateTime.Parse(Date);
                EmrSession emrSession = new EmrSession(HttpContext);

                RegisterService registerService = new RegisterService(_context);
                registerService.UpdateRegisterPayment(prmId, id, emrSession.userId, status, paymentValue, paymentDate);

                return Json(new { success = true });

            }
            return Json(new { success = false });
        }


        [Route("CloseProgram")]
        [Authorize]
        public IActionResult CloseProgram(string programId)
        {
            if (!string.IsNullOrEmpty(programId))
            {
                EmrSession emrSession = new EmrSession(HttpContext);
                if (emrSession.userRole.Equals(StaticsData.ADMIN_ROLE) || emrSession.userRole.Equals(StaticsData.SUPER_ADMIN_ROLE))
                {
                    int prID = int.Parse(programId);
                    ProgramsService programsService = new ProgramsService(_context);
                    programsService.CloseProgram(prID, emrSession.userId, DateTime.Now);

                    string logFileName = programsService.GetProgramLogFileName(prID);
                    string messageLog = "Đã đóng chương trình";
                    StaticsData.LogToXml(messageLog, logFileName, emrSession.userName);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = true }, new { massage = "Không có quyền" });
                }
            }

            return Json(new { success = false });
        }
    }
}
