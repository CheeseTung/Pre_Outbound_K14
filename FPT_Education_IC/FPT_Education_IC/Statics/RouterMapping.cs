using FPT_Education_IC.Data;
using FPT_Education_IC.Service;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Education_IC.Statics
{
    public class RouterMapping
    {
        public RouterMapping()
        {
        }

        /// <summary>
        /// Thêm các service để xử lý function cho các trang Views
        /// </summary>
        public void AddService(HttpContext httpContext,Controller controller, DataContext context)
        {
            // Add Session to Controller
            EmrSession emrSession = new EmrSession(httpContext);
            controller.ViewBag.EmrSession = emrSession;
            
            // Add Service to Controller
            controller.ViewBag.AccountService = new AccountService(context);
            controller.ViewBag.CampusService = new CampusService(context);
            controller.ViewBag.ContactService = new ContactService(context);
            controller.ViewBag.CountryService = new CountryService(context);
            controller.ViewBag.DocumentService = new DocumentService(context);
            controller.ViewBag.ForumService = new ForumService(context);
            controller.ViewBag.ManagementService = new ManagementService(context);
            controller.ViewBag.PartnerService = new PartnerService(context);
            controller.ViewBag.PermissionService = new PermissionService(context);
            controller.ViewBag.ProgramsService = new ProgramsService(context);
            controller.ViewBag.RegisterService = new RegisterService(context);
            controller.ViewBag.SystemsService = new SystemsService(context);
            controller.ViewBag.UserService = new UserService(context);
        }
    }
}
