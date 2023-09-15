using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Statics;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace FPT_Education_IC.Service
{
    public class AccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        // Add new account when first time login
        public UsrAccount addNewAccount(string email, string usrName, string imgUrl, int campusCode)
        {
            UsrAccount account = new UsrAccount();
            account.Email = email;
            account.UserName = string.IsNullOrEmpty(usrName) ? email.Split("@")[0] : usrName;
            account.Image = string.IsNullOrEmpty(imgUrl) ? StaticsData.DEFAULT_IMG : imgUrl;
            account.Role = StaticsData.DEFAULT_ROLE;
            account.Campus = campusCode;
            account.Gender = 1;
            account.Dob = DateTime.Now;
            account.Major = null;
            account.RollNumber = email.Split("@")[0];
            account.Phone = null;
            account.IdNumber = null;
            account.IdNumberStDate = null;
            account.Passport = null;
            account.PassportStartDate = null;
            account.PassportEndDate = null;
            account.Facebook = null;
            account.UpdateUser = null;
            account.UpdateDate = DateTime.Now;
            account.IsActive = true;

            _context.UsrAccounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        public void UpdateUsrAccount(int userId, string usrName, string usrImg, int gender, string dob, string usrMajor, string RollNumber, string phone, string idNumber,
            string IdNumberStDate, string passportNum, string passportStDate, string passportEndDate, string facebookLink)
        {
            UsrAccount account = _context.UsrAccounts.FirstOrDefault(x => x.UserId == userId);
            if (account != null)
            {
                if (!string.IsNullOrEmpty(usrName) && usrName != "null")
                {
                    account.UserName = usrName;
                }
                if (!string.IsNullOrEmpty(usrImg) && usrImg != "null")
                {
                    account.Image = usrImg;
                }
                account.Gender = gender;
                if (!string.IsNullOrEmpty(dob) && dob != "null")
                {
                    account.Dob = DateTime.Parse(dob);
                }
                account.Major = usrMajor;
                account.RollNumber = RollNumber;
                account.Phone = phone;
                account.IdNumber = idNumber;
                if (!string.IsNullOrEmpty(IdNumberStDate) && IdNumberStDate != "null")
                {
                    account.IdNumberStDate = DateTime.Parse(IdNumberStDate);
                }
                if (!string.IsNullOrEmpty(passportNum) && !string.IsNullOrEmpty(passportStDate) && !string.IsNullOrEmpty(passportEndDate)
                    && passportNum != "null" && passportStDate != "null" && passportEndDate != "null")
                {
                    account.Passport = passportNum;
                    account.PassportStartDate = DateTime.Parse(passportStDate);
                    account.PassportEndDate = DateTime.Parse(passportEndDate);
                }
                account.Facebook = facebookLink;
                account.UpdateUser = userId;
                account.UpdateDate = DateTime.Now;
                account.IsActive = true;

                _context.UsrAccounts.Update(account);
                _context.SaveChanges();
            }
        }

        public UsrAccount GetUsrAccountById(int id)
        {
            UsrAccount account = new UsrAccount();
            if (id >= 0)
            {
                var result = _context.UsrAccounts.FirstOrDefault(x => x.UserId == id);
                if (result != null)
                {
                    account = result;
                }
            }
            return account;
        }

        public ArrayList GetListAccountManageByCampus(int campusId)
        {
            ArrayList listAccount = new ArrayList();
            var result = _context.UsrAccounts.Where(p => p.Campus == campusId && !p.Role.Equals(StaticsData.DEFAULT_ROLE)).ToList();

            if (result != null && result.Count > 0)
            {
                listAccount.AddRange(result);
            }
            return listAccount;
        }

        public string getUsrEmailById(int id)
        {
            string usrEmail = null;
            if (id >= 0)
            {
                UsrAccount account = new UsrAccount();
                account = _context.UsrAccounts.FirstOrDefault(x => x.UserId == id);
                if (account != null)
                {
                    usrEmail = account.Email;
                }
            }
            return usrEmail;
        }

        public string checkEmailExist(string email, int userId)
        {
            UsrAccount accManage = _context.UsrAccounts.FirstOrDefault(x => x.UserId == userId);

            if (!string.IsNullOrEmpty(email) && accManage != null)
            {
                if (accManage.Role.Equals(StaticsData.ADMIN_ROLE))
                {
                    UsrAccount account = new UsrAccount();
                    account = _context.UsrAccounts.FirstOrDefault(x => x.Email == email);
                    if (account != null)
                    {
                        if (account.Campus != accManage.Campus)
                        {
                            return "message";
                        }

                        return "true";
                    }
                }
                else if (accManage.Role.Equals(StaticsData.SUPER_ADMIN_ROLE))
                {
                    UsrAccount account = new UsrAccount();
                    account = _context.UsrAccounts.FirstOrDefault(x => x.Email == email);
                    if (account != null)
                    {
                        return "true";
                    }
                }
            }
            return "false";
        }


    }
}
