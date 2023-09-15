using Microsoft.AspNetCore.Mvc;
using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using FPT_Education_IC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Collections;
using FPT_Education_IC.Statics;
using Microsoft.Extensions.Logging;

namespace FPT_Education_IC.Controllers
{
    [Route("Document")]
    public class DocumentController : Controller
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly DataContext _context;
        public DocumentController(ILogger<DocumentController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ManageDocument")]
        [Authorize]
        public IActionResult ManageDocument()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            DocumentService documentService = new DocumentService(_context);
            List<DocumentModel> listDoc = documentService.getAllDocument();
            ViewBag.listDoc = listDoc;
            return View();

        }

        [Route("GetProgramsByPartner")]
        [Authorize]
        public ActionResult GetProgramsByPartner(string partnerId)
        {
            PartnerService partnerService = new PartnerService(_context);
            List<Models.Program> listProgramByPartner = partnerService.GetProgramsByPartner(Convert.ToInt32(partnerId));

            return Json(new { success = true, list = listProgramByPartner });

        }


        [Route("RequestDocument")]
        [Authorize]
        public IActionResult RequestDocument()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            DocumentService documentService = new DocumentService(_context);
            List<DocumentModel> listDoc = documentService.getAllDocument();
            ViewBag.listDoc = listDoc;
            return View();

        }

        [Route("DocumentDetail")]
        [Authorize]
        public IActionResult DocumentDetail(string documentId)
        {
            int docId = Convert.ToInt32(documentId);
            DocumentService documentService = new DocumentService(_context);
            ProgramsService programService = new ProgramsService(_context);
            PartnerService partnerService = new PartnerService(_context);
            ArrayList listProgram = programService.getAllPrograms();
            List<Partner> listPartner = partnerService.getAvailablePartner();
            PartnerDocument doc = documentService.getDocumentDetail(docId);
            ViewBag.listProgram = listProgram;
            ViewBag.listPartner = listPartner;
            ViewBag.doc = doc;
            return View();
        }

        [Route("DownloadDocument")]
        [Authorize]
        [HttpGet]
        public IActionResult DownloadDocument(int documentId)
        {
            // Tìm thông tin tệp dựa trên documentId
            DocumentService documentService = new DocumentService(_context);
            var document = documentService.GetDocumentById(documentId);

            // Kiểm tra xem document có tồn tại và có đường dẫn không
            if (document != null && !string.IsNullOrEmpty(document.Path))
            {
                var filePath = Path.Combine("wwwroot/document", document.Path.TrimStart('/'));
                string correctedFilePath = filePath.Replace("\\", "/");
                // Kiểm tra xem tệp có tồn tại không
                if (System.IO.File.Exists(correctedFilePath))
                {
                    // Trả về tệp như một tệp tin để tải về
                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    return File(fileStream, "application/octet-stream", Path.GetFileName(correctedFilePath));
                }
            }

            // Trả về thông báo lỗi nếu tệp không tồn tại
            return NotFound();
        }

        [Route("AddDocument")]
        [Authorize]
        public IActionResult AddDocument()
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            ProgramsService programService = new ProgramsService(_context);
            PartnerService partnerService = new PartnerService(_context);
            ArrayList listProgram = programService.getAllPrograms();
            List<Partner> listPartner = partnerService.getAvailablePartner();
            ViewBag.listProgram = listProgram;
            ViewBag.listPartner = listPartner;
            return View();
        }

        [Route("DoAdd")]
        [Authorize]
        public IActionResult DoAdd(IFormFile docAdd)
        {
            int? programId = null;
            string docname = Request.Form["docname"];
            int partnerid = Convert.ToInt32(Request.Form["partnerid"]);
            string programid = Request.Form["programid"];
            if (!string.IsNullOrEmpty(programid))
            {
                programId = Convert.ToInt32(programid);
            }

            string type = Request.Form["type"];

            string partnerDocPath = "";
            var docPath = "";
            if (docAdd != null && docAdd.Length > 0)
            {
                // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                docPath = Path.Combine("wwwroot", "assets", "document", "partner");
                Directory.CreateDirectory(docPath);

                DateTime SaveDate = DateTime.Now;

                string fileNameWithoutExtension = $"partner_doc_{SaveDate:yyyyMMdd_HHmmss}";
                string fileExtension = Path.GetExtension(docAdd.FileName);
                string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                // Save the image to the directory
                docPath = Path.Combine(docPath, fileName);

                partnerDocPath = $"/assets/document/partner/{fileName}";
            }


            string description = Request.Form["desc"];
            DateTime startdate = DateTime.Parse(Request.Form["startdate"]);
            DateTime? enddate = null;
            if (!string.IsNullOrEmpty(Request.Form["enddate"]))
            {
                enddate = DateTime.Parse(Request.Form["enddate"]);
            }
            EmrSession emrSession = new EmrSession(HttpContext);
            AccountService accountService = new AccountService(_context);
            UsrAccount updateUsr = accountService.GetUsrAccountById(emrSession.userId);
            if (updateUsr != null)
            {
                int status = 1;
                if (updateUsr.Role.Equals(StaticsData.STAFF_ROLE))
                {
                    status = 0; // request thêm mới
                }

                DocumentService documentService = new DocumentService(_context);
                PartnerDocument newDoc = documentService.addNewDocument(partnerid, programId, type, partnerDocPath, docname, description, startdate, enddate, status, updateUsr.UserId, DateTime.Now);

                if (newDoc != null)
                {
                    if (!string.IsNullOrEmpty(docPath))
                    {
                        using (var stream = new FileStream(docPath, FileMode.Create))
                        {
                            docAdd.CopyTo(stream);
                        }
                    }
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

        [Route("DeleteDocument")]
        [Authorize]
        public IActionResult DeleteDocument(string documentId)
        {
            if (!string.IsNullOrEmpty(documentId))
            {
                int docID = int.Parse(documentId);
                DocumentService documentService = new DocumentService(_context);
                EmrSession emrSession = new EmrSession(HttpContext);
                AccountService accountService = new AccountService(_context);
                UsrAccount usrAccount = accountService.GetUsrAccountById(emrSession.userId);
                if (usrAccount.Role == StaticsData.ADMIN_ROLE || usrAccount.Role == StaticsData.SUPER_ADMIN_ROLE)
                {
                    documentService.DeleteDocument(docID);
                }
                else if (usrAccount.Role == StaticsData.STAFF_ROLE)
                {
                    documentService.ApproveDocument(docID, 3);
                }
                
            }
            return RedirectToAction("ManageDocument");
        }

        [Route("UpdateDocument")]
        [Authorize]
        public IActionResult UpdateDocument(string documentId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(documentId))
            {
                ViewBag.DocumentId = int.Parse(documentId);
            }
            PartnerService partnerService = new PartnerService(_context);
            List<Partner> listPartner = partnerService.getAvailablePartner();
            ViewBag.listPartner = listPartner;

            return View();
        }

        [Route("DoUpdate")]
        [Authorize]
        public IActionResult DoUpdate(IFormFile docAdd, string documentId)
        {
            if (!string.IsNullOrEmpty(documentId)) {

                int docID = int.Parse(documentId);
                int? programId = null;
                string docname = Request.Form["docname"];
                int partnerid = Convert.ToInt32(Request.Form["partnerid"]);
                string programid = Request.Form["programid"];
                if (!string.IsNullOrEmpty(programid))
                {
                    programId = Convert.ToInt32(programid);
                }

                string type = Request.Form["type"];

                string partnerDocPath = "";
                var docPath = "";
                if (docAdd != null && docAdd.Length > 0)
                {
                    // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                    docPath = Path.Combine("wwwroot", "assets", "document", "partner");
                    Directory.CreateDirectory(docPath);

                    DateTime SaveDate = DateTime.Now;

                    string fileNameWithoutExtension = $"partner_doc_{SaveDate:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(docAdd.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    docPath = Path.Combine(docPath, fileName);

                    partnerDocPath = $"/assets/document/partner/{fileName}";
                }


                string description = Request.Form["desc"];
                DateTime startdate = DateTime.Parse(Request.Form["startdate"]);
                DateTime? enddate = null;
                if (!string.IsNullOrEmpty(Request.Form["enddate"]))
                {
                    enddate = DateTime.Parse(Request.Form["enddate"]);
                }
                EmrSession emrSession = new EmrSession(HttpContext);
                AccountService accountService = new AccountService(_context);
                UsrAccount updateUsr = accountService.GetUsrAccountById(emrSession.userId);
                if (updateUsr != null)
                {
                    int status = 1;
                    if (updateUsr.Role.Equals(StaticsData.STAFF_ROLE))
                    {
                        status = 2; // request thay đổi
                    }

                    DocumentService documentService = new DocumentService(_context);
                    documentService.updateDocument(docID, partnerid, programId, type, partnerDocPath, docname, description, startdate, enddate, status, updateUsr.UserId, DateTime.Now);

                    if (!string.IsNullOrEmpty(docPath))
                    {
                        using (var stream = new FileStream(docPath, FileMode.Create))
                        {
                            docAdd.CopyTo(stream);
                        }

                        string oldDoc = Request.Form["oldDoc"];
                        if (!string.IsNullOrEmpty(oldDoc))
                        {
                            var oldImagePath = Path.Combine("wwwroot", "assets", "document", "partner", Path.GetFileName(oldDoc));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                    }

                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });

        }

        [Route("ApproveDocument")]
        [Authorize]
        public IActionResult ApproveDocument(string documentId, int status)
        {
            if (!string.IsNullOrEmpty(documentId))
            {
                int docID = int.Parse(documentId);
                DocumentService documentService = new DocumentService(_context);
                if (status == 0 || status == 2)
                {
                    status = 1;
                    documentService.ApproveDocument(docID, status);
                }
                else if (status == 3)
                {
                    documentService.DeleteDocument(docID);
                }
            }

            return RedirectToAction("ManageDocument", "Document");
        }
    }
}
