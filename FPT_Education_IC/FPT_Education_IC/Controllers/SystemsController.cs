using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using FPT_Education_IC.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Education_IC.Controllers
{
    [Route("Systems")]
    public class SystemsController : Controller
    {
        private readonly ILogger<SystemsController> _logger;
        private readonly DataContext _context;

        public SystemsController(ILogger<SystemsController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ViewDashboard")]
        [Authorize]
        public IActionResult ViewDashboard()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }


        [Route("ViewListFAQ")]
        [Authorize]
        [HttpGet]
        public ActionResult ViewListFAQ(string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue) )
            {
                ViewBag.SearchValue = searchValue;
            }
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            return View();
        }

        [Route("ViewDetailsFAQ")]
        [Authorize]
        public ActionResult ViewDetailsFAQ(int questionID)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (questionID != null)
            {
                ViewBag.QuestionID = questionID;
            }

            return View();
        }

        [Route("CreateFAQSystem")]
        [Authorize]
        public ActionResult CreateFAQSystem()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("UpdateFAQSystem")]
        [Authorize]
        public ActionResult UpdateFAQSystem(int questionID)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (questionID != null)
            {
                ViewBag.QuestionID = questionID;
            }

            return View();
        }

        [Route("AddNewFAQ")]
        [Authorize]
        [HttpPost]
        public ActionResult AddNewFAQ()
        {
            string questionType = Request.Form["questionType"];
            string questionContent = Request.Form["questionContent"];
            string answerContent = Request.Form["answerContent"];
            answerContent = answerContent.Replace("\r\n", "").Replace("\n", "");
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.AddNewFAQ(questionType, questionContent, answerContent, updateUser, updateDate);

            return RedirectToAction("CreateFAQSystem");
        }

        [Route("UpdateFAQ")]
        [Authorize]
        public ActionResult UpdateFAQ()
        {
            int questionID = int.Parse(Request.Form["questionID"]);
            string questionType = Request.Form["questionType"];
            string questionContent = Request.Form["questionContent"];
            string answerContent = Request.Form["answerContent"];
            answerContent = answerContent.Replace("\r\n", "").Replace("\n", "");
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.UpdateFAQ(questionID, questionType, questionContent, answerContent, updateUser, updateDate);

            return RedirectToAction("ViewDetailsFAQ", "Systems", new { questionID });
        }

        [Route("DeleteFAQ")]
        [Authorize]
        public ActionResult DeleteFAQ(int questionID)
        {

            if (questionID != null)
            {
                SystemsService systemsService = new SystemsService(_context);
                systemsService.DeleteFAQ(questionID);
            }

            return RedirectToAction("ViewListFAQ");
        }

        [Route("ManageProgramType")]
        [Authorize]
        public ActionResult ManageProgramType()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("AddProgramCategorize")]
        [Authorize]
        [HttpPost]
        public ActionResult AddProgramCategorize()
        {

            string selectedValue = Request.Form["selectedValue"];
            string categorizeValue = Request.Form["categorizeValue"];
            string descriptionValue = Request.Form["descriptionValue"];
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.AddProgramCategorize(selectedValue, categorizeValue, descriptionValue, updateUser, updateDate);

            return RedirectToAction("ManageProgramType");
        }


        [Route("UpdateProgramCategorize")]
        [Authorize]
        [HttpPost]
        public ActionResult UpdateProgramCategorize()
        {
            int categorizeID = int.Parse(Request.Form["categorizeID"]);
            string selectedValue = Request.Form["selectedValue"];
            string categorizeValue = Request.Form["categorizeValue"];
            string descriptionValue = Request.Form["descriptionValue"];
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.UpdateProgramCategorize(categorizeID, selectedValue, categorizeValue, descriptionValue, updateUser, updateDate);

            return RedirectToAction("ManageProgramType");
        }


        [Route("DeleteProgramCategorize")]
        [Authorize]
        public ActionResult DeleteProgramCategorize(string categorizeID)
        {
            
            if (!string.IsNullOrEmpty(categorizeID))
            {
                int id = int.Parse(categorizeID);
                SystemsService systemsService = new SystemsService(_context);
                systemsService.DeleteProgramCategorize(id);
            }

            return RedirectToAction("ManageProgramType");
        }

        [Route("ManageDocumentType")]
        [Authorize]
        public ActionResult ManageDocumentType()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }


        [Route("AddDocumentType")]
        [Authorize]
        [HttpPost]
        public ActionResult AddDocumentType()
        {
            string categorizeValue = Request.Form["categorizeValue"];
            string descriptionValue = Request.Form["descriptionValue"];
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.AddDocumentType(categorizeValue, descriptionValue, updateUser, updateDate);

            return RedirectToAction("ManageDocumentType");
        }


        [Route("UpdateProgramType")]
        [Authorize]
        [HttpPost]
        public ActionResult UpdateProgramType()
        {
            int categorizeID = int.Parse(Request.Form["categorizeID"]);
            string categorizeValue = Request.Form["categorizeValue"];
            string descriptionValue = Request.Form["descriptionValue"];
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.UpdateDocumentType(categorizeID, categorizeValue, descriptionValue, updateUser, updateDate);

            return RedirectToAction("ManageDocumentType");
        }


        [Route("DeleteDocumentType")]
        [Authorize]
        public ActionResult DeleteDocumentType(string categorizeID)
        {

            if (!string.IsNullOrEmpty(categorizeID))
            {
                int id = int.Parse(categorizeID);
                SystemsService systemsService = new SystemsService(_context);
                systemsService.DeleteDocumentType(id);
            }

            return RedirectToAction("ManageDocumentType");
        }

        [Route("InterfaceConfiguration")]
        [Authorize]
        public ActionResult InterfaceConfiguration(string actionStr)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(actionStr) && actionStr.Equals("Banner"))
            {
                return View("InterfaceConfiguration_Banner");
            }

            return View();
        }

        [Route("InterfaceConfiguration_Banner")]
        [Authorize]
        public ActionResult InterfaceConfiguration_Banner()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }

        [Route("SystemMessage")]
        [Authorize]
        public ActionResult SystemMessage()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            return View();
        }


        [Route("AddContactUs")]
        [Authorize]
        [HttpPost]
        public ActionResult AddContactUs()
        {
            string contactUsTypeValue = Request.Form["contactUsTypeValue"];
            string contactUsLinkValue = Request.Form["contactUsLinkValue"];
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.AddContactUs(contactUsTypeValue, contactUsLinkValue, updateUser, updateDate);

            return RedirectToAction("InterfaceConfiguration");
        }

        [Route("UpdateContactUs")]
        [Authorize]
        [HttpPost]
        public ActionResult UpdateContactUs()
        {
            int contactUsID = int.Parse(Request.Form["contactUsID"]);
            string contactUsTypeValue = Request.Form["contactUsTypeValue"];
            string contactUsLinkValue = Request.Form["contactUsLinkValue"];
            int updateUser = int.Parse(Request.Form["updateUser"]);
            DateTime updateDate = DateTime.Parse(Request.Form["updateDate"]);

            SystemsService systemsService = new SystemsService(_context);
            systemsService.UpdateContactUs(contactUsID, contactUsTypeValue, contactUsLinkValue, updateUser, updateDate);

            return RedirectToAction("InterfaceConfiguration");
        }


        [Route("DeleteContactUs")]
        [Authorize]
        public ActionResult DeleteContactUs(string contactUsID)
        {

            if (!string.IsNullOrEmpty(contactUsID))
            {
                int id = int.Parse(contactUsID);
                SystemsService systemsService = new SystemsService(_context);
                systemsService.DeleteContactUs(id);
            }

            return RedirectToAction("InterfaceConfiguration");
        }

        [Route("AddBanner")]
        [Authorize]
        [HttpPost]
        public ActionResult AddBanner(string BannerProgramValue, IFormFile BannerImageValue)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            if (BannerImageValue != null && BannerImageValue.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                var imagePath = Path.Combine("wwwroot", "assets", "img", "banner");
                Directory.CreateDirectory(imagePath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"banner_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(BannerImageValue.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                imagePath = Path.Combine(imagePath, fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    BannerImageValue.CopyTo(stream);
                }

                string bannerImagePath = $"/assets/img/banner/{fileName}";

                SystemsService systemsService = new SystemsService(_context);
                systemsService.AddBanner(bannerImagePath, BannerProgramValue, emrSession.userId, SaveDate);
            }

            return RedirectToAction("InterfaceConfiguration", "Systems", new { actionStr = "Banner"});
        }

        [Route("UpdateBanner")]
        [Authorize]
        [HttpPost]
        public ActionResult UpdateBanner(string BannerProgramValue, IFormFile BannerImageValue, string BannerIDUpdate, string OldBannerProgramValue)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            int updateID = int.Parse(BannerIDUpdate);
            if (BannerImageValue != null && BannerImageValue.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                var imagePath = Path.Combine("wwwroot", "assets", "img", "banner");
                Directory.CreateDirectory(imagePath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"banner_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(BannerImageValue.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                imagePath = Path.Combine(imagePath, fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    BannerImageValue.CopyTo(stream);
                }

                string bannerImagePath = $"/assets/img/banner/{fileName}";

                SystemsService systemsService = new SystemsService(_context);
                systemsService.UpdateBanner(updateID, bannerImagePath, BannerProgramValue, emrSession.userId, DateTime.Now);

                // Delete the old image associated with OldBannerProgramValue
                if (!string.IsNullOrEmpty(OldBannerProgramValue))
                {
                    var oldImagePath = Path.Combine("wwwroot", "assets", "img", "banner", Path.GetFileName(OldBannerProgramValue));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            } 
            else
            {
                SystemsService systemsService = new SystemsService(_context);
                systemsService.UpdateBanner(updateID, null, BannerProgramValue, emrSession.userId, DateTime.Now);
            }

            return RedirectToAction("InterfaceConfiguration", "Systems", new { actionStr = "Banner" });
        }

        [Route("DeleteBanner")]
        [Authorize]
        public ActionResult DeleteBanner(string bannerID, string OldBannerProgramValue)
        {

            if (!string.IsNullOrEmpty(bannerID))
            {
                int id = int.Parse(bannerID);
                SystemsService systemsService = new SystemsService(_context);
                systemsService.DeleteBanner(id);

                if (!string.IsNullOrEmpty(OldBannerProgramValue))
                {
                    var oldImagePath = Path.Combine("wwwroot", "assets", "img", "banner", Path.GetFileName(OldBannerProgramValue));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            return RedirectToAction("InterfaceConfiguration", "Systems", new { actionStr = "Banner" });
        }


    }
}
