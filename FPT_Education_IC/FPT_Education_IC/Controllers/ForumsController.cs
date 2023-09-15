using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Service;
using FPT_Education_IC.Statics;
using FPT_Education_IC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Education_IC.Controllers
{
    [Route("Forums")]
    public class ForumsController : Controller
    {
        private readonly ILogger<ForumsController> _logger;
        private readonly DataContext _context;

        public ForumsController(ILogger<ForumsController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ParticipatingForum")]
        [Authorize]
        public ActionResult ParticipatingForum()
        {

            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            return View();
        }

        [Route("ParticipationForum")]
        [Authorize]
        public ActionResult ParticipationForum()
        {

            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            return View();
        }

        [Route("ViewForumDetails")]
        [Authorize]
        public ActionResult ViewForumDetails(string programId)
        {

            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }
            return View("ForumDetails");
        }

        [Route("CreateForum")]
        [Authorize]
        public ActionResult CreateForum(string programId)
        {
            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }
            return View();
        }

        [Route("ViewForumMembers")]
        [Authorize]
        public ActionResult ViewForumMembers(string programId)
        {

            RouterMapping mapping = new RouterMapping();
            mapping.AddService(HttpContext, this, _context);
            if (!string.IsNullOrEmpty(programId))
            {
                ViewBag.ProgramId = int.Parse(programId);
            }
            return View("ForumMembers");
        }

        [Route("AddNewForum")]
        [Authorize]
        public ActionResult AddNewForum(string programId, IFormFile imageAdd, IFormFile docAdd, string postTitle, string postContent)
        {

            if (!string.IsNullOrEmpty(programId) && !string.IsNullOrEmpty(postTitle) && !string.IsNullOrEmpty(postContent))
            {

                int programID = int.Parse(programId);


                string forumImagePath = "";
                var imagePath = "";
                if (imageAdd != null && imageAdd.Length > 0)
                {
                    // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                    imagePath = Path.Combine("wwwroot", "assets", "img", "forum");
                    Directory.CreateDirectory(imagePath);

                    DateTime SaveDate = DateTime.Now;

                    string fileNameWithoutExtension = $"forum_{SaveDate:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(imageAdd.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    imagePath = Path.Combine(imagePath, fileName);

                    forumImagePath = $"/assets/img/forum/{fileName}";
                }

                string forumDocPath = "";
                var docPath = "";
                if (docAdd != null && docAdd.Length > 0)
                {
                    // Đảm bảo thư mục tồn tại hoặc tạo mới nếu chưa có
                    docPath = Path.Combine("wwwroot", "assets", "document", "forum");
                    Directory.CreateDirectory(docPath);

                    DateTime SaveDate = DateTime.Now;

                    string fileNameWithoutExtension = $"forum_doc_{SaveDate:yyyyMMdd_HHmmss}";
                    string fileExtension = Path.GetExtension(docAdd.FileName);
                    string fileName = $"{fileNameWithoutExtension}{fileExtension}";

                    // Save the image to the directory
                    docPath = Path.Combine(docPath, fileName);

                    forumDocPath = $"/assets/document/forum/{fileName}";
                }
                if(!string.IsNullOrEmpty(forumDocPath))
                {

                    string addDiv = "<p class='mb-3 mt-3'><a href=\"" + forumDocPath + "\" id=\"downloadLink\">Tải hoặc xem tệp</a></p>";
                    postContent += addDiv;
                }

                ForumService forumService = new ForumService(_context);
                EmrSession emrSession = new EmrSession(HttpContext);
                int recentId = forumService.AddNewForum(programID, postTitle, postContent, forumImagePath, emrSession.userId);
                if (recentId > -1)
                {
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            imageAdd.CopyTo(stream);
                        }
                    }
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

        [Route("AddNewComment")]
        [Authorize]
        public ActionResult AddNewComment(string userId, string postId, string comment)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(postId) && !string.IsNullOrEmpty(comment))
            {
                int usrID = int.Parse(userId);
                int postID = int.Parse(postId);

                ForumService forumService = new ForumService(_context);
                forumService.AddNewPostComment(usrID, postID, comment);
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
        
    }
}
