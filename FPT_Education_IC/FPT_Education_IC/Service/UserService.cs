using Microsoft.AspNetCore.Mvc;
using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using Microsoft.EntityFrameworkCore;
using FPT_Education_IC.Statics;

namespace FPT_Education_IC.Service
{
    public class UserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public List<UsrAccount> getAllStaff()
        {
            List<UsrAccount> listusr = new List<UsrAccount>();

            var result = _context.UsrAccounts.Where(u => !u.Role.Equals(StaticsData.DEFAULT_ROLE)).ToList();
            if (result != null && result.Count > 0)
            {
                listusr.AddRange(result);
            }

            return listusr;
        }

        public int getUserIdByPartner(int partnerid)
        {
            int userId = 0;
            Partner partner = new Partner();
            partner = _context.Partners.FirstOrDefault(p => p.PartnerId == partnerid);
            if(partner != null)
            {
                userId = partner.UserId;
            }
            return userId;
        }

        public UsrAccount GetUsrAccountByPartnerId(int id)
        {
            int userId = getUserIdByPartner(id);
            UsrAccount usr = new UsrAccount();
            usr = _context.UsrAccounts.FirstOrDefault(u => u.UserId == userId);
            return usr;
        }

        

    }
}
