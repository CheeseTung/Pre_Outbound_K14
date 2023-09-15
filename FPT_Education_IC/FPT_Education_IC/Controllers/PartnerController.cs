using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using FPT_Education_IC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Newtonsoft.Json;
using FPT_Education_IC.Statics;
using Microsoft.AspNetCore.Hosting.Server;

namespace FPT_Education_IC.Controllers
{
    [Route("Partner")]
    public class PartnerController : Controller
    {
        private readonly ILogger<PartnerController> _logger;
        private readonly DataContext _context;
        public PartnerController(ILogger<PartnerController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ManagePartner")]
        [Authorize]
        public IActionResult ManagePartner()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            PartnerService partnerService = new PartnerService(_context);
            List<Country> listCountry = partnerService.getAllCountry();
            List<Partner> ListPartner = partnerService.getListPartner();
            ViewBag.ListPartner = ListPartner;
            ViewBag.ListCountry = listCountry;
            return View();

        }

        [Route("PartnerProgram")]
        [Authorize]
        public IActionResult PartnerProgram(string partnerId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            if (!string.IsNullOrEmpty(partnerId))
            {
                ViewBag.PartnerId = int.Parse(partnerId);

            }

            return View();

        }

        [Route("PartnerHistory")]
        [Authorize]
        public IActionResult PartnerHistory(string partnerId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            if (!string.IsNullOrEmpty(partnerId))
            {
                ViewBag.PartnerId = int.Parse(partnerId);

            }

            return View();

        }


        [Route("CreatePartnerHistory")]
        [Authorize]
        public IActionResult CreatePartnerHistory(string partnerId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            if (!string.IsNullOrEmpty(partnerId))
            {
                ViewBag.PartnerId = int.Parse(partnerId);

            }

            return View();

        }

        [Route("PartnerDocument")]
        [Authorize]
        public IActionResult PartnerDocument(string partnerId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            if (!string.IsNullOrEmpty(partnerId))
            {
                ViewBag.PartnerId = int.Parse(partnerId);
                DocumentService documentService = new DocumentService(_context);
                List<DocumentModel> listDoc = documentService.getAllDocumentByPartnerId(partnerId);
                ViewBag.ListDoc = listDoc;
            }

            return View();

        }

        [Route("RequestPartner")]
        [Authorize]
        public IActionResult RequestPartner()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            PartnerService partnerService = new PartnerService(_context);
            List<Country> listCountry = partnerService.getAllCountry();
            List<Partner> ListPartner = partnerService.getListPartner();
            ViewBag.ListPartner = ListPartner;
            ViewBag.ListCountry = listCountry;
            return View();
        }


        [Route("ApprovePartner")]
        [Authorize]
        public IActionResult ApprovePartner(string partnerId)
        {
            if (!string.IsNullOrEmpty(partnerId))
            {
                int partnerID = int.Parse(partnerId);
                PartnerService partnerService = new PartnerService(_context);
                EmrSession emrSession = new EmrSession(HttpContext);

                partnerService.ApprovePartner(partnerID, emrSession.userId);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("AddPartner")]
        [Authorize]
        public IActionResult AddPartner()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);

            PartnerService partnerService = new PartnerService(_context);
            UserService userService = new UserService(_context);
            List<Country> listCountry = partnerService.getAllCountry();
            List<UsrAccount> listStaff = userService.getAllStaff();
            ProgramsService programService = new ProgramsService(_context);
            ArrayList listProgram = programService.getAllPrograms();
            List<Partner> listPartner = partnerService.getAvailablePartner();
            ViewBag.listProgram = listProgram;
            ViewBag.listPartner = listPartner;
            ViewBag.ListCountry = listCountry;
            ViewBag.ListStaff = listStaff;
            return View();
        }

        [Route("DoAdd")]
        [Authorize]
        public IActionResult DoAdd(IFormFile uploadedImage)
        {
            EmrSession emrSession = new EmrSession(HttpContext);
            //thêm mới đối tác
            string name = Request.Form["partnername"];
            string email = Request.Form["partneremail"];
            string country = Request.Form["partnercountry"];
            string address = Request.Form["partneraddress"];
            string website = Request.Form["partnerwebsite"];
            DateTime startdate = DateTime.Parse(Request.Form["startdate"]);
            string description = Request.Form["desc"];
            int resuser = Convert.ToInt32(Request.Form["resuser"]);
            bool isactive = false;
            string phone = Request.Form["partnerphone"];
            string imagePath = "";
            string imgPath = "";
            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                imgPath = Path.Combine("wwwroot", "assets", "img", "partner");
                Directory.CreateDirectory(imgPath);
                string imgNameWithoutExtension = $"partner_{DateTime.Now:yyyyMMdd_HHmmss}";
                string imgExtension = Path.GetExtension(uploadedImage.FileName);
                string imgName = $"{imgNameWithoutExtension}{imgExtension}";
                imgPath = Path.Combine(imgPath, imgName);


                imagePath = $"/assets/img/partner/{imgName}";
            }

            DateTime updateDate = DateTime.Now;
            if (emrSession.userRole.Equals(StaticsData.ADMIN_ROLE) || emrSession.userRole.Equals(StaticsData.SUPER_ADMIN_ROLE))
            {
                isactive = true;
            }

            PartnerService partnerService = new PartnerService(_context);
            int partnerId = partnerService.addNewPartner(name, resuser, country, address, email, phone, imagePath, website, startdate, isactive, emrSession.userId, updateDate, description);

            if (!string.IsNullOrEmpty(imgPath))
            {
                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    uploadedImage.CopyTo(stream);
                }
            }

            //thêm mới liên hệ
            ContactService contactService = new ContactService(_context);
            string listContact = Request.Form["listContact"];
            if (!string.IsNullOrEmpty(listContact) && partnerId > -1)
            {
                List<ContactModel> contactInfoList = JsonConvert.DeserializeObject<List<ContactModel>>(listContact);


                // Xử lý danh sách contactInfoList ở đây
                foreach (var contact in contactInfoList)
                {
                    // Thực hiện các thao tác xử lý với thông tin liên hệ (contact)
                    string cname = contact.cname;
                    string ctitle = contact.ctitle;
                    string cemail = contact.cemail;
                    int clevel = int.Parse(contact.clevel);
                    string cfacebook = contact.cfb;
                    string ctwitter = contact.ctwitter;
                    string clinkdln = contact.clinkdln;
                    contactService.addNewContact(partnerId, clevel, cname, cemail, ctitle, clinkdln, ctwitter, cfacebook);

                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("PartnerProfile")]
        [Authorize]
        public IActionResult PartnerProfile(string partnerId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            PartnerService partnerService = new PartnerService(_context);
            UserService userService = new UserService(_context);
            ContactService contactService = new ContactService(_context);
            DocumentService documentService = new DocumentService(_context);
            List<Country> listCountry = partnerService.getAllCountry();
            List<PartnerContact> listContact = contactService.getAllContactByPartnerId(partnerId);
            List<DocumentModel> listDoc = documentService.getAllDocumentByPartnerId(partnerId);
            Partner partner = new Partner();
            partner = partnerService.getPartnerProfile(Convert.ToInt32(partnerId));
            ViewBag.listContact = listContact;
            ViewBag.Partner = partner;
            ViewBag.ListCountry = listCountry;
            ViewBag.ListDoc = listDoc;
            return View();
        }

        [Route("DoUpdateProfile")]
        [Authorize]
        public IActionResult DoUpdateProfile(IFormFile uploadedImage, string partnerId)
        {
            if (!string.IsNullOrEmpty(partnerId))
            {
                int id = int.Parse(partnerId);
                EmrSession emrSession = new EmrSession(HttpContext);
                //thêm mới đối tác
                string name = Request.Form["partnername"];
                string email = Request.Form["partneremail"];
                string country = Request.Form["partnercountry"];
                string address = Request.Form["partneraddress"];
                string website = Request.Form["partnerwebsite"];
                DateTime startdate = DateTime.Parse(Request.Form["startdate"]);
                string description = Request.Form["desc"];
                int resuser = Convert.ToInt32(Request.Form["resuser"]);
                string phone = Request.Form["partnerphone"];
                string imagePath = "";
                string imgPath = "";
                if (uploadedImage != null && uploadedImage.Length > 0)
                {
                    imgPath = Path.Combine("wwwroot", "assets", "img", "partner");
                    Directory.CreateDirectory(imgPath);
                    string imgNameWithoutExtension = $"partner_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string imgExtension = Path.GetExtension(uploadedImage.FileName);
                    string imgName = $"{imgNameWithoutExtension}{imgExtension}";
                    imgPath = Path.Combine(imgPath, imgName);


                    imagePath = $"/assets/img/partner/{imgName}";
                }

                DateTime updateDate = DateTime.Now;

                PartnerService partnerService = new PartnerService(_context);
                partnerService.updatePartnerProfile(id, name, resuser, country, address, email, phone, imagePath, startdate, description, website, emrSession.userId, updateDate);

                if (!string.IsNullOrEmpty(imgPath))
                {
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        uploadedImage.CopyTo(stream);
                    }

                    string oldImage = Request.Form["oldImage"];
                    if (!string.IsNullOrEmpty(oldImage))
                    {
                        var oldImagePath = Path.Combine("wwwroot", "assets", "img", "partner", Path.GetFileName(oldImage));
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

        [Route("DoUpdateContact")]
        [Authorize]
        public IActionResult DoUpdateContact()
        {
            int contactId = Convert.ToInt32(Request.Form["contactid"]);
            int partnerId = Convert.ToInt32(Request.Form["partnerId"]);
            string name = Request.Form["name"];
            string title = Request.Form["title"];
            int level = Convert.ToInt32(Request.Form["level"]);
            string email = Request.Form["email"];
            string facebook = Request.Form["facebook"];
            string twitter = Request.Form["twitter"];
            string linkdln = Request.Form["linkdln"];
            ContactService contactService = new ContactService(_context);
            contactService.updateContact(contactId, partnerId, level, name, email, title, linkdln, twitter, facebook);
            return RedirectToAction("PartnerProfile", "Partner", new { partnerId = partnerId });
        }

        [Route("DoDeleteContact")]
        [Authorize]
        public IActionResult DoDeleteContact(string contactId, string partnerId)
        {
            int partnerid = Convert.ToInt32(partnerId);
            DataContext dataContext = new DataContext();
            ContactService contactService = new ContactService(_context);
            contactService.deleteContact(contactId);

            return RedirectToAction("PartnerProfile", "Partner", new { partnerId = partnerid });
        }


        [Route("DoAddPartnerHistory")]
        [Authorize]
        public IActionResult DoAddPartnerHistory(string partnerId, IFormFile docAdd, List<IFormFile> images)
        {
            if (!string.IsNullOrEmpty(partnerId))
            {
                int PartnerID = int.Parse(partnerId);
                string historyTitle = Request.Form["historyTitle"];
                string postContent = Request.Form["postContent"];
                int userId = int.Parse(Request.Form["userId"]);
                DateTime stDate = DateTime.Parse(Request.Form["startdate"]);
                DateTime endDate = DateTime.Parse(Request.Form["enddate"]);

                // Thêm document
                string documentPath = "";
                string docAddPath = "";
                if (docAdd != null && docAdd.Length > 0)
                {
                    docAddPath = Path.Combine("wwwroot", "assets", "document", "partnerhistory", partnerId);
                    Directory.CreateDirectory(docAddPath);
                    string docNameWithoutExtension = $"partnerhistory_{partnerId}_{DateTime.Now:yyyyMMdd_HHmmss}";
                    string docExtension = Path.GetExtension(docAdd.FileName);
                    string docName = $"{docNameWithoutExtension}{docExtension}";
                    docAddPath = Path.Combine(docAddPath, docName);
                    documentPath = $"/assets/document/partnerhistory/{partnerId}/{docName}";
                }

                //Thêm ảnh
                string historyImgPath = "";
                string imgAddPath = "";
                if (images != null && images.Count > 0) {
                    historyImgPath = Path.Combine("wwwroot", "assets", "img", "partnerhistory", partnerId);
                    Directory.CreateDirectory(historyImgPath);
                    imgAddPath = $"/assets/img/partnerhistory/{partnerId}/";
                }

                EmrSession emrSession = new EmrSession(HttpContext);
                PartnerService partnerService = new PartnerService(_context);
                int id = partnerService.AddNewPartnerHistory(PartnerID, historyTitle, postContent, userId, stDate, endDate, documentPath, imgAddPath, emrSession.userId, DateTime.Now);

                if (id > -1)
                {
                    // thêm file vào folder
                    if (!string.IsNullOrEmpty(docAddPath))
                    {
                        using (var stream = new FileStream(docAddPath, FileMode.Create))
                        {
                            docAdd.CopyTo(stream);
                        }
                    }

                    // Thêm ảnh vào folder
                    if (!string.IsNullOrEmpty(historyImgPath))
                    {
                        int count = 0;
                        foreach (var image in images)
                        {
                            if (image != null && image.Length > 0)
                            {
                                string imgNameWithoutExtension = $"partnerhistory_{count}_{DateTime.Now:yyyyMMdd_HHmmss}";
                                string imgExtension = Path.GetExtension(image.FileName);
                                string imgName = $"{imgNameWithoutExtension}{imgExtension}";
                                string imgFilePath = Path.Combine(historyImgPath, imgName);

                                using (var stream = new FileStream(imgFilePath, FileMode.Create))
                                {
                                    image.CopyTo(stream);
                                }
                            }
                        }
                    }
                }

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [Route("GetHistoryImages")]
        [Authorize]
        public ActionResult GetHistoryImages(string historyId)
        {
            if (string.IsNullOrWhiteSpace(historyId))
            {
                return BadRequest("Invalid historyId.");
            }

            try
            {
                var imageFolderPath = Path.Combine("wwwroot", "assets", "img", "partnerhistory", historyId);

                if (!Directory.Exists(imageFolderPath))
                {
                    return NotFound("Image folder not found.");
                }

                var imagePaths = Directory.GetFiles(imageFolderPath)
                          .Where(filePath => filePath.ToLower().EndsWith(".jpg") ||
                                             filePath.ToLower().EndsWith(".png") ||
                                             filePath.ToLower().EndsWith(".jpeg"))
                          .Select(filePath => Url.Content(filePath.Replace("wwwroot", "").Replace("\\", "/"))) // Loại bỏ tiền tố "~"
                          .ToArray();

                return Json(imagePaths);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
