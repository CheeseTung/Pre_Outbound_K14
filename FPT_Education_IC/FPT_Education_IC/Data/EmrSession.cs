using FPT_Education_IC.Controllers;
using Microsoft.AspNetCore.Http;

namespace FPT_Education_IC.Data
{
    public class EmrSession
    {
        public string userIdIdentity { get; set; } = "";
        public int userId { get; set; }
        public string userRole { get; set; } = "";
        public string userName { get; set; } = "";
        public string userEmail { get; set; } = "";
        public string userImage { get; set; } = "";
        public int userCampus { get; set; }

        private DataContext _context;

        public EmrSession(HttpContext httpContext) {
            if (httpContext != null && httpContext.Session != null)
            {
                userId = httpContext.Session.GetInt32("user_Id")??0;
                userIdIdentity = string.IsNullOrEmpty(httpContext.Session.GetString("user_identity")) ? "" : httpContext.Session.GetString("user_identity");
                userRole = string.IsNullOrEmpty(httpContext.Session.GetString("user_role")) ? "" : httpContext.Session.GetString("user_role");
                userEmail = string.IsNullOrEmpty(httpContext.Session.GetString("user_email")) ? "" : httpContext.Session.GetString("user_email");
                userName = string.IsNullOrEmpty(httpContext.Session.GetString("user_name")) ? "" : httpContext.Session.GetString("user_name");
                userImage = string.IsNullOrEmpty(httpContext.Session.GetString("user_img")) ? "" : httpContext.Session.GetString("user_img");
                userCampus = httpContext.Session.GetInt32("user_campus") ?? 0;
            }
        }

        public void putSession(HttpContext httpContext)
        {
            httpContext.Session.SetInt32("user_Id", userId);
            httpContext.Session.SetString("user_identity", userIdIdentity);
            httpContext.Session.SetString("user_role", userRole);
            httpContext.Session.SetString("user_email", userEmail);
            httpContext.Session.SetString("user_name", userName);
            httpContext.Session.SetString("user_img", userImage);
            httpContext.Session.SetInt32("user_campus", userCampus);
        }

        public void clearSession(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }
    }
}
