using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Statics;
using System.Collections;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FPT_Education_IC.Service
{
    public class ManagementService
    {
        private readonly DataContext _context;

        public ManagementService(DataContext context)
        {
            _context = context;
        }

        public ArrayList getAllUsrRoleManage(int userId)
        {
            ArrayList usrRole_List = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(c => c.UserId == userId);
            if (usrAccount != null)
            {
                if (usrAccount.Role.Equals(StaticsData.ADMIN_ROLE))
                {
                    var result = _context.UsrAccountRoles.FirstOrDefault(p => p.RoleCode.Equals(StaticsData.STAFF_ROLE));
                    usrRole_List.Add(result);
                }
                else if(usrAccount.Role.Equals(StaticsData.SUPER_ADMIN_ROLE))
                {
                    var result = _context.UsrAccountRoles.Where(p => !p.RoleCode.Equals(StaticsData.DEFAULT_ROLE)).ToList();
                    usrRole_List.AddRange(result);
                }
            }

            return usrRole_List;
        }


        public ArrayList getAllUsrAccountManage(int userId)
        {
            ArrayList usrAccountManage_List = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(c => c.UserId == userId);

            if (usrAccount != null)
            {
                if (usrAccount.Role.Equals(StaticsData.ADMIN_ROLE) || usrAccount.Role.Equals(StaticsData.STAFF_ROLE))
                {
                    var result = _context.UsrAccounts.Where(p => !p.Role.Equals(StaticsData.DEFAULT_ROLE) && p.Campus == usrAccount.Campus).ToList();
                    if (result != null && result.Count > 0)
                    {
                        usrAccountManage_List.AddRange(result);
                    }
                }
                else if (usrAccount.Role.Equals(StaticsData.SUPER_ADMIN_ROLE))
                {
                    var result = _context.UsrAccounts.Where(p => !p.Role.Equals(StaticsData.DEFAULT_ROLE)).ToList();
                    if (result != null && result.Count > 0)
                    {
                        usrAccountManage_List.AddRange(result);
                    }
                }
            }

            return usrAccountManage_List;
        }



        public void UpdateUsrRole(string email, string role, int updateUsr, DateTime updateDate)
        {
            if (!string.IsNullOrEmpty(email))
            {
                UsrAccount account = new UsrAccount();
                account = _context.UsrAccounts.FirstOrDefault(x => x.Email == email);
                if (account != null)
                {
                    account.Email = email;
                    account.Role = role;
                    account.UpdateUser = updateUsr;
                    account.UpdateDate = updateDate;

                    _context.UsrAccounts.Update(account);
                    _context.SaveChanges();
                }
            }
        }


        public void RemoveUsrRole(int id, int updateUsr, DateTime updateDate)
        {
            UsrAccount account = new UsrAccount();
            account = _context.UsrAccounts.FirstOrDefault(x => x.UserId == id);
            if (account != null)
            {
                account.Role = StaticsData.DEFAULT_ROLE;
                account.UpdateUser = updateUsr;
                account.UpdateDate = updateDate;

                _context.UsrAccounts.Update(account);
                _context.SaveChanges();
            }
        }

    }
}
