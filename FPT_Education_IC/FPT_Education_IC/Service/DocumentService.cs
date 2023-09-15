using Microsoft.AspNetCore.Mvc;
using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Collections;

namespace FPT_Education_IC.Service
{
    public class DocumentService 
    {
        private readonly DataContext _context;

        public DocumentService(DataContext context)
        {
            _context = context;
        }

        public List<DocumentModel> getAllDocument()
        {
            List<DocumentModel> listdoc = new List<DocumentModel>();
            var result = (from pd in _context.PartnerDocuments
                          join p in _context.Partners on pd.PartnerId equals p.PartnerId
                          join cm in _context.CommonMasters on pd.Type equals cm.MasterType
                          join pro in _context.Programs on pd.ProgramId equals pro.ProgramId into Programs
                          from proval in Programs.DefaultIfEmpty()
                          select new DocumentModel
                          {
                              documentId = pd.DocumentId,
                              Docname = pd.Name,
                              Partname = p.Name,
                              PartnerId = p.PartnerId,
                              ProgramId = pd.ProgramId,
                              Programname = proval.Title,
                              Type = cm.MasterName,
                              Startdate = pd.StartDate,
                              Enddate = pd.EndDate,
                              Status = pd.Status,
                              filePath = pd.Path
                          }
                          ).ToList();

            if (result != null && result.Count > 0)
            {
                listdoc.AddRange(result);
            }

            return listdoc;
        }

        public PartnerDocument addNewDocument(int partnerid, int? programid, string type, string path, string name,
           string description, DateTime startdate, DateTime? enddate, int status, int updateUser, DateTime updateDate)
        {
            
            PartnerDocument document = new PartnerDocument();
            document.PartnerId = partnerid;
            document.ProgramId = programid;
            document.Type= type;
            document.Path = path;
            document.Name = name;
            document.Description = description;
            document.StartDate = startdate;
            document.EndDate = enddate;
            document.Status = status;
            document.UpdateUser = updateUser;
            document.UpdateDate = updateDate;

            _context.PartnerDocuments.Add(document);
            _context.SaveChanges();
            return document;
        }

        public void deleteDocument(String id)
        {
            var document = _context.PartnerDocuments.FirstOrDefault(x => x.DocumentId == Convert.ToInt32(id));
            if (document != null)
            {
                _context.PartnerDocuments.Remove(document);
                _context.SaveChanges();
            }
        }

        public PartnerDocument getDocumentDetail(int id)
        {
            PartnerDocument doc = new PartnerDocument();
            var result = _context.PartnerDocuments.FirstOrDefault(x => x.DocumentId == id);

            if (result != null)
            {
                doc = result;
            }
            return doc;
        }

        public PartnerDocument GetDocumentById(int documentId)
        {
            var document = _context.PartnerDocuments.FirstOrDefault(doc => doc.DocumentId == documentId);
            return document;
        }

        public int getPartnerIdByDocId(int id)
        {
            var partnerId = _context.PartnerDocuments.Where(doc => doc.DocumentId == id).Select(doc => doc.PartnerId).FirstOrDefault();
            return partnerId;
        }
        public string getPartnerNameByDocId(int id)
        {
            var partnerName = _context.PartnerDocuments.Where(d => d.DocumentId == id).Join(_context.Partners,d => d.PartnerId,p => p.PartnerId,(document, partner) => partner.Name).FirstOrDefault();

            return partnerName;
        }

        public Nullable<int> getProgramIdByDocId(int id)
        {
            Nullable<int> programId = _context.PartnerDocuments.Where(doc => doc.ProgramId == id).Select(doc => doc.ProgramId).FirstOrDefault();
            return programId;
        }

        public string GetTypeByDocId(int id)
        {
            var type = _context.PartnerDocuments.Where(doc => doc.DocumentId == id).Select(doc => doc.Type).FirstOrDefault();
            return type;
        }



        public void updateDocument(int docid, int partnerid, int? programid, string type, string path, string name,
           string description, DateTime startdate, DateTime? enddate, int status, int updateUser, DateTime updateDate)
        {
            var docUpdate = _context.PartnerDocuments.Where(d => d.DocumentId == docid).FirstOrDefault();
            if (docUpdate != null)
            {
                docUpdate.PartnerId = partnerid;
                docUpdate.ProgramId = programid;
                docUpdate.Type = type;
                if (!string.IsNullOrEmpty(path))
                {
                    docUpdate.Path = path;
                }
                docUpdate.Name = name;
                docUpdate.Description = description;
                docUpdate.StartDate = startdate;
                docUpdate.EndDate = enddate;
                docUpdate.Status = status;
                docUpdate.UpdateUser = updateUser;
                docUpdate.UpdateDate = updateDate;

                _context.PartnerDocuments.Update(docUpdate);
                _context.SaveChanges();
            }
        }

        public List<DocumentModel> getAllDocumentByPartnerId(string partnerId)
        {
            List<DocumentModel> listdoc = new List<DocumentModel>();
            int partnerID = int.Parse(partnerId);
            if (partnerID > -1)
            {
                var result = (from pd in _context.PartnerDocuments
                              join p in _context.Partners on pd.PartnerId equals p.PartnerId
                              join cm in _context.CommonMasters on pd.Type equals cm.MasterType
                              join pro in _context.Programs on pd.ProgramId equals pro.ProgramId into Programs
                              from proval in Programs.DefaultIfEmpty()
                              where p.PartnerId == partnerID
                              select new DocumentModel
                              {
                                  documentId = pd.DocumentId,
                                  Docname = pd.Name,
                                  PartnerId = partnerID,
                                  Partname = p.Name,
                                  ProgramId = pd.ProgramId,
                                  Programname = proval.Title,
                                  Type = cm.MasterName,
                                  Startdate = pd.StartDate,
                                  Enddate = pd.EndDate,
                                  Status = pd.Status,
                                  filePath = pd.Path
                              }
                              ).ToList();

                if (result != null && result.Count > 0)
                {
                    listdoc.AddRange(result);
                }
            }
            return listdoc;
        }

        public ArrayList GetAllProgramDocument(int programID)
        {
            ArrayList list = new ArrayList();
            var result = _context.PartnerDocuments.Where(p => p.ProgramId == programID).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public void ApproveDocument(int documentID, int status)
        {
            PartnerDocument partnerDocument = _context.PartnerDocuments.FirstOrDefault(p => p.DocumentId == documentID);
            if (partnerDocument != null)
            {
                partnerDocument.Status = status;
                _context.PartnerDocuments.Update(partnerDocument);
                _context.SaveChanges();
            }
        }

        public void DeleteDocument(int documentID)
        {
            PartnerDocument partnerDocument = _context.PartnerDocuments.FirstOrDefault(p => p.DocumentId == documentID);
            if (partnerDocument != null)
            {
                _context.PartnerDocuments.Remove(partnerDocument);
                _context.SaveChanges();
            }
        }
    }
}
