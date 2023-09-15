using FPT_Education_IC.Data;
using FPT_Education_IC.Models;

namespace FPT_Education_IC.Service
{
    public class PermissionService
    {
        private readonly DataContext _context;
        private readonly RequestDelegate _next;

        public PermissionService(DataContext context)
        {
            _context = context;
        }

        public bool CheckPermission(HttpContext httpContext, int userId ,string role)
        {
            if(!string.IsNullOrEmpty(role))
            {
                var result = _context.UsrAccounts.FirstOrDefault(x => x.UserId == userId && x.Role.Equals(role));
                if(result != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.Redirect("/Account/Login");
                return;
            }

            await _next(httpContext);
        }
    }
}
