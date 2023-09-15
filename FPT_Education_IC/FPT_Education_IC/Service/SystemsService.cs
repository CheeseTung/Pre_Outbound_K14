using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Statics;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Principal;

namespace FPT_Education_IC.Service
{
    public class SystemsService
    {
        private readonly DataContext _context;

        public SystemsService(DataContext context) {
            _context = context;
        }

        public ArrayList GetContactUsList()
        {
            ArrayList contactUsList = new ArrayList();

            var query = _context.CommonMasters.Where(p => p.MasterCode.Equals("ContactUs"));

            var result = query.Select(c => new CommonMaster
            {
                Id = c.Id,
                MasterCode = c.MasterCode.Trim(),
                MasterType = c.MasterType.Trim(),
                MasterName = c.MasterName.Trim(),
                Description = c.Description,
                UpdateUser = c.UpdateUser,
                UpdateDate = c.UpdateDate
            })
                .OrderBy(c => c.MasterType)
                .ToList();

            if (result.Count > 0)
            {
                contactUsList.AddRange(result);
            }
            return contactUsList;

        }
        
        public void AddContactUs(string masterName, string description, int updateUser, DateTime updateDate)
        {
            int count = GetLastCommon();
            string masterType = "CTU" + count;

            CommonMaster common = new CommonMaster();
            common.MasterCode = "ContactUs";
            common.MasterType = masterType;
            common.MasterName = masterName;
            common.Description = description;
            common.UpdateUser = updateUser;
            common.UpdateDate = updateDate;
            _context.CommonMasters.Add(common);
            _context.SaveChanges();
        }

        public void UpdateContactUs(int categorizeID, string masterName, string description, int updateUser, DateTime updateDate)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);

            if (common != null)
            {
                common.MasterName = masterName;
                common.Description = description;
                common.UpdateUser = updateUser;
                common.UpdateDate = updateDate;
                _context.CommonMasters.Update(common);
                _context.SaveChanges();
            }
        }

        public void DeleteContactUs(int categorizeID)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);
            if (common != null)
            {
                _context.CommonMasters.Remove(common);
                _context.SaveChanges();
            }
        }

        public ArrayList GetListProgramCategorize(string code)
        {
            ArrayList programCategorize_List = new ArrayList();

            var query = _context.CommonMasters.Where(p => p.MasterCode != null);

            if (!string.IsNullOrEmpty(code))
            {
                if ("Study".Equals(code))
                {
                    query = query.Where(p => p.MasterCode.Equals("Study"));
                } else if ("Culture".Equals(code))
                {
                    query = query.Where(p => p.MasterCode.Equals("Culture"));
                }
            } else
            {
                query = query.Where(p => p.MasterCode.Equals("Study") || p.MasterCode.Equals("Culture"));
            }

            var result = query.Select(c => new CommonMaster
                {
                    Id = c.Id,
                    MasterCode = c.MasterCode.Trim(),
                    MasterType = c.MasterType.Trim(),
                    MasterName = c.MasterName.Trim(),
                    Description = c.Description,
                    UpdateUser = c.UpdateUser,
                    UpdateDate = c.UpdateDate
                })
                .OrderBy(c => c.MasterType)
                .ToList();

            if (result.Count > 0)
            {
                programCategorize_List.AddRange(result);
            }
            return programCategorize_List;
        }

        public void AddProgramCategorize(string masterCode, string masterName, string description, int updateUser, DateTime updateDate)
        {
            int index = GetLastCommon();
            string masterType = "";
            if (masterCode.Equals("Study"))
            {
                masterType = "ST" + index;
            } else if (masterCode.Equals("Culture"))
            {
                masterType = "CT" + index;
            }

            CommonMaster common = new CommonMaster();
            common.MasterCode = masterCode;
            common.MasterType = masterType;
            common.MasterName = masterName;
            common.Description = description;
            common.UpdateUser = updateUser;
            common.UpdateDate = updateDate;
            _context.CommonMasters.Add(common);
            _context.SaveChanges();
        }

        public int GetLastCommon()
        {
            int index = 0;
            CommonMaster result =_context.CommonMasters.OrderByDescending(x => x.Id).FirstOrDefault();
            if (result != null)
            {
                index = result.Id;
            }
            return index;
        }

        public void UpdateProgramCategorize(int categorizeID, string masterCode, string masterName, string description, int updateUser, DateTime updateDate)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);

            if (common != null)
            {
                common.MasterCode = masterCode;
                common.MasterName = masterName;
                common.Description = description;
                common.UpdateUser = updateUser;
                common.UpdateDate = updateDate;
                _context.CommonMasters.Update(common);
                _context.SaveChanges();
            }
        }

        public void DeleteProgramCategorize(int categorizeID)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);
            if (common != null)
            {
                _context.CommonMasters.Remove(common);
                _context.SaveChanges();
            }
        }

        public ArrayList GetListDocumentType()
        {
            ArrayList documentCategorize_List = new ArrayList();

            var query = _context.CommonMasters.Where(p => p.MasterCode.Equals("Document"));

            var result = query.Select(c => new CommonMaster
            {
                Id = c.Id,
                MasterCode = c.MasterCode.Trim(),
                MasterType = c.MasterType.Trim(),
                MasterName = c.MasterName.Trim(),
                Description = c.Description,
                UpdateUser = c.UpdateUser,
                UpdateDate = c.UpdateDate
            })
                .OrderBy(c => c.MasterType)
                .ToList();

            if (result.Count > 0)
            {
                documentCategorize_List.AddRange(result);
            }
            return documentCategorize_List;
        }

        public void AddDocumentType(string masterName, string description, int updateUser, DateTime updateDate)
        {
            int count = GetLastCommon();
            string masterType = "DC" + count;

            CommonMaster common = new CommonMaster();
            common.MasterCode = "Document";
            common.MasterType = masterType;
            common.MasterName = masterName;
            common.Description = description;
            common.UpdateUser = updateUser;
            common.UpdateDate = updateDate;
            _context.CommonMasters.Add(common);
            _context.SaveChanges();
        }

        public void UpdateDocumentType(int categorizeID, string masterName, string description, int updateUser, DateTime updateDate)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);

            if (common != null)
            {
                common.MasterName = masterName;
                common.Description = description;
                common.UpdateUser = updateUser;
                common.UpdateDate = updateDate;
                _context.CommonMasters.Update(common);
                _context.SaveChanges();
            }
        }

        public void DeleteDocumentType(int categorizeID)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);
            if (common != null)
            {
                _context.CommonMasters.Remove(common);
                _context.SaveChanges();
            }
        }

        public void AddNewFAQ(string type, string question, string answer, int updateUser, DateTime updateDate)
        {
            Faq faq = new Faq();
            faq.Type = type.Trim();
            faq.Question = question.Trim();
            faq.Answer = answer.Trim();
            faq.UpdateUser = updateUser;
            faq.UpdateDate = updateDate;
            _context.Faqs.Add(faq);
            _context.SaveChanges();
        }

        public void UpdateFAQ(int questionID, string type, string question, string answer, int updateUser, DateTime updateDate)
        {

            var faq = _context.Faqs.FirstOrDefault(c => c.Id == questionID);

            if (faq != null)
            {
                faq.Type = type.Trim();
                faq.Question = question.Trim();
                faq.Answer = answer.Trim();
                faq.UpdateUser = updateUser;
                faq.UpdateDate = updateDate;
                _context.Faqs.Update(faq);
                _context.SaveChanges();
            }
        }

        public void DeleteFAQ(int questionID)
        {
            var faq = _context.Faqs.FirstOrDefault(c => c.Id == questionID);
            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                _context.SaveChanges();
            }
        }

        public string GetMasterNameByType(string type)
        {
            string name = "";
            if (!string.IsNullOrEmpty(type))
            {
                CommonMaster master = _context.CommonMasters.FirstOrDefault(c => c.MasterType.Equals(type));
                if (master != null)
                {
                    name = master.MasterName;
                }
            }
            return name;
        }

        public ArrayList GetListFAQ(string searchValue)
        {
            ArrayList FAQ_List = new ArrayList();

            var query = _context.Faqs.Where(p => p.Question != null);

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Type.Contains(searchValue) || p.Question.Contains(searchValue));
            }

            var result = query.Select(c => new Faq
            {
                Id = c.Id,
                Type = c.Type.Trim(),
                Question = c.Question.Trim(),
                Answer = c.Answer.Trim(),
                UpdateUser = c.UpdateUser,
                UpdateDate = c.UpdateDate
            })
                .OrderBy(c => c.Type)
                .ThenBy(c => c.UpdateDate)
                .ToList();

            if (result.Count > 0)
            {
                FAQ_List.AddRange(result);
            }
            return FAQ_List;
        }

        public Faq GetQuestionByID(int questionID)
        {
            Faq faq = new Faq();
            var ojb = _context.Faqs.FirstOrDefault(c => c.Id == questionID);
            if (ojb != null)
            {
               faq = ojb;
            }
            return faq;
        }

        public ArrayList GetListBanner()
        {
            ArrayList bannerList = new ArrayList();

            var query = _context.CommonMasters.Where(p => p.MasterCode.Equals("Banner"));

            var result = query.Select(c => new CommonMaster
            {
                Id = c.Id,
                MasterCode = c.MasterCode.Trim(),
                MasterType = c.MasterType.Trim(),
                MasterName = c.MasterName.Trim(),
                Description = c.Description,
                UpdateUser = c.UpdateUser,
                UpdateDate = c.UpdateDate
            })
                .OrderByDescending(c => c.UpdateDate)
                .ToList();

            if (result.Count > 0)
            {
                bannerList.AddRange(result);
            }
            return bannerList;
        }

        public void AddBanner(string masterName, string description, int updateUser, DateTime updateDate)
        {
            int count = GetLastCommon();
            string masterType = "BN" + count;

            CommonMaster common = new CommonMaster();
            common.MasterCode = "Banner";
            common.MasterType = masterType;
            common.MasterName = masterName;
            common.Description = description;
            common.UpdateUser = updateUser;
            common.UpdateDate = updateDate;
            _context.CommonMasters.Add(common);
            _context.SaveChanges();
        }

        public void UpdateBanner(int updateID, string masterName, string description, int updateUser, DateTime updateDate)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == updateID);

            if (common != null)
            {
                if (!string.IsNullOrEmpty(masterName))
                {
                    common.MasterName = masterName;
                }
                common.Description = description;
                common.UpdateUser = updateUser;
                common.UpdateDate = updateDate;
                _context.CommonMasters.Update(common);
                _context.SaveChanges();
            }
        }

        public void DeleteBanner(int categorizeID)
        {
            var common = _context.CommonMasters.FirstOrDefault(c => c.Id == categorizeID);
            if (common != null)
            {
                _context.CommonMasters.Remove(common);
                _context.SaveChanges();
            }
        }

        public void CreateNotification(int receiverUsr, int notiType, string notiContent, int? updateUsr)
        {
            Notification notification = new Notification();
            notification.ReceiverUsr = receiverUsr;
            notification.NotiType = notiType;
            notification.NotiContent = notiContent;
            notification.UpdateDate = DateTime.Now;
            if (updateUsr != null)
            {
                notification.UpdateUsr = updateUsr;
            }
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public ArrayList GetListUsrNotification(int receiverUsr)
        {
            ArrayList list = new ArrayList();
            var result = _context.Notifications.Where(x => x.ReceiverUsr == receiverUsr).OrderByDescending(x => x.UpdateDate).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

    }
}
