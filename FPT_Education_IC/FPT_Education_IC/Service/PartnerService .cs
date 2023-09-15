using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using System.Collections;

namespace FPT_Education_IC.Service
{
    public class PartnerService
    {
        private readonly DataContext _context;

        public PartnerService(DataContext context)
        {
            _context = context;
        }

        public ArrayList GetListAllPartner(string countryCode, bool checkActive)
        {
            ArrayList listAllPartner = new ArrayList();

            var query = _context.Partners.Where(p => p.PartnerId != null);
            if (!string.IsNullOrEmpty(countryCode))
            {
                query = query.Where(p => p.Country == countryCode);
            }
            if (checkActive)
            {
                query = query.Where(p => p.IsActive == true);
            }

            var result = query.Select(p => new Partner
            {
                PartnerId = p.PartnerId,
                Country = p.Country,
                Name = p.Name,
                UserId = p.UserId,
                Address = p.Address,
                Email = p.Email,
                Phone = p.Phone,
                Image = p.Image,
                Website = p.Website,
                IsActive = p.IsActive,
                UpdateUser = p.UpdateUser,
                UpdateDate = p.UpdateDate,
            })
                .OrderBy(p => p.UpdateDate)
                .ThenBy(p => p.PartnerId)
                .ToList();

            if (result.Count > 0)
            {
                listAllPartner.AddRange(result);
            }

            return listAllPartner;
        }

        public ArrayList GeListPartnerByCountry(string countryCode)
        {
            ArrayList listPartner = new ArrayList();
            var result = _context.Partners.Where(p => p.Country == countryCode && p.IsActive).OrderBy(p => p.Name).ToList();
            if (result != null && result.Count > 0)
            {
                listPartner.AddRange(result);
            } 
            return listPartner;
        }

        public ArrayList GetListContactPartner(int partnerId)
        {
            ArrayList contactPartnerList = new ArrayList();
            var query = _context.PartnerContacts.Where(p => p.PartnerId == partnerId);

            var result = query.Select(p => new PartnerContact
            {
                PartnerId = p.PartnerId,
                ContactId = p.ContactId,
                Name = p.Name,
                ContactLevel = p.ContactLevel,
                Email = p.Email,
                Facebook = p.Facebook,
                LinkedIn = p.LinkedIn,
                Title = p.Title,
                Twitter = p.Twitter
            })
                .OrderByDescending(p => p.ContactId)
                .ToList();
            if (result != null && result.Count > 0)
            {
                contactPartnerList.AddRange(result);
            }
            return contactPartnerList;
        }

        public List<Partner> getListPartner()
        {
            var result = new List<Partner>();
            result = _context.Partners.ToList();
            return result;
        }

        public Partner getPartnerProfile(int partnerId)
        {
            Partner partner = new Partner();
            partner = _context.Partners.FirstOrDefault(x => x.PartnerId == partnerId);
            return partner;
        }

        public List<Country> getAllCountry()
        {
            var result = new List<Country>();
            result = _context.Countries.ToList();
            return result;
        }

        public List<Partner> getAvailablePartner()
        {
            var result = new List<Partner>();
            result = _context.Partners.Where(p => p.IsActive).ToList();
            return result;
        }


        public int GetNumberPartnerProgram(int partnerId)
        {
            int number = 0;

            var result = _context.ProgramCooperations.Where(p => p.PartnerId == partnerId).Count();
            if(result >= 0)
            {
                number = result;
            }
            return number;
        }

        public ArrayList ListParnertsManage()
        {
            ArrayList list = new ArrayList();
            var result = (from p in _context.Partners
                          join ct in _context.Countries on p.Country equals ct.IsoCode
                          join usr in _context.UsrAccounts on p.UserId equals usr.UserId
                          select new ManagePartnerModel
                          {
                              partnerID = p.PartnerId,
                              partnerName = p.Name,
                              Email = p.Email,
                              Country = ct.Name,
                              StDate = p.StartDate,
                              Status = p.IsActive,
                              ReUser = usr.Email
                          }
                          ).OrderByDescending(p => p.StDate).ToList();


            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }


        public int addNewPartner(string name, int userId, string country, string address, string email,
            string phone, string image, string website, DateTime startDate, bool isactive, int updateUser, DateTime updateDate, string description)
        {

            Partner partner = new Partner();
            partner.Name = name;
            partner.UserId = userId;
            partner.Country = country;
            partner.Address = address;
            partner.Email = email;
            partner.Phone = phone;
            partner.Image = image;
            partner.Website = website;
            partner.StartDate = startDate;
            partner.IsActive = isactive;
            partner.UpdateUser = updateUser;
            partner.UpdateDate = updateDate;
            partner.Description = description;
            _context.Partners.Add(partner);
            _context.SaveChanges();

            return partner.PartnerId;
        }

        public void updatePartnerProfile(int id, string name, int userId, string country, string address, string email,
            string phone, string image, DateTime stDate, string description, string website, int updateUser, DateTime updateDate)
        {
            Partner partnerUpdate = _context.Partners.Where(p => p.PartnerId == id).FirstOrDefault();
            partnerUpdate.Name = name;
            partnerUpdate.UserId = userId;
            partnerUpdate.Country = country;
            partnerUpdate.Address = address;
            partnerUpdate.Email = email;
            partnerUpdate.Phone = phone;
            if (!string.IsNullOrEmpty(image))
            {
                partnerUpdate.Image = image;
            }
            partnerUpdate.StartDate = stDate;
            partnerUpdate.Description = description;
            partnerUpdate.Website = website;
            partnerUpdate.UpdateUser = updateUser;
            partnerUpdate.UpdateDate = updateDate;
            _context.Partners.Update(partnerUpdate);
            _context.SaveChanges();

        }

        public void deletePartner(String partnerId)
        {
            var partner = _context.Partners.FirstOrDefault(x => x.PartnerId == Convert.ToInt32(partnerId));
            if (partner != null)
            {
                _context.Partners.Remove(partner);
                _context.SaveChanges();
            }
        }

        public string getNameByCode(int code)
        {
            String campusName = "";
            FptCampus campus = new FptCampus();
            campus = _context.FptCampuses.FirstOrDefault(x => x.CampusCode == code);
            if (campus != null)
            {
                campusName = campus.Name;
            }
            return campusName;
        }

        public int getLastestPartner()
        {
            int partnerId = 0;
            Partner partner = new Partner();
            partner = _context.Partners.OrderByDescending(x => x.PartnerId).FirstOrDefault();
            if (partner != null)
            {
                partnerId = partner.PartnerId;
            }
            return partnerId;

        }

        public void ApprovePartner(int id, int userId)
        {
            Partner partner = _context.Partners.FirstOrDefault(p => p.PartnerId == id);

            if (partner != null)
            {
                partner.IsActive = true;
                partner.UpdateUser = userId;
                partner.UpdateDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public List<Models.Program> GetProgramsByPartner(int partnerId)
        {
            var programIdsForPartner = _context.ProgramCooperations
                .Where(mapping => mapping.PartnerId == partnerId)
                .Select(mapping => mapping.ProgramId)
                .ToList();

            var programsForPartner = _context.Programs
                .Where(program => programIdsForPartner.Contains(program.ProgramId))
                .ToList();

            return programsForPartner;
        }

        public int AddNewPartnerHistory(int partnerId, string title, string description, int userId, DateTime stDate, DateTime endDate, string documentPath, string imgPath, int updateUsr, DateTime updateDate)
        {
            PartnerHistory partnerHistory = new PartnerHistory();
            partnerHistory.PartnerId = partnerId;
            partnerHistory.Title = title;
            partnerHistory.Description = description;
            partnerHistory.UserId = userId;
            partnerHistory.StartDate = stDate;
            partnerHistory.EndDate = endDate;
            partnerHistory.Documents = documentPath;
            partnerHistory.Image = imgPath;
            partnerHistory.UpdateUser = updateUsr;
            partnerHistory.UpdateDate = updateDate;

            _context.PartnerHistories.Add(partnerHistory);
            _context.SaveChanges();

            return partnerHistory.Id;

        }

        public ArrayList GetListPartnerHistory(int partnerId)
        {
            ArrayList list = new ArrayList();
            var result = _context.PartnerHistories.Where(p => p.PartnerId == partnerId).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public string GetHistoryImageFolder(int historyId)
        {
            string folder = "";
            PartnerHistory result = _context.PartnerHistories.FirstOrDefault(p => p.Id == historyId);
            if (result != null)
            {
                folder = result.Image;
            }
            return folder;
        }
    }
}
