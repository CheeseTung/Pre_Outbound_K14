using Microsoft.AspNetCore.Mvc;
using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FPT_Education_IC.Service
{
    public class ContactService : Controller
    {
        private readonly DataContext _context;

        public ContactService(DataContext context)
        {
            _context = context;
        }

        public PartnerContact addNewContact(int partnerId, int level, string name, string email, string title, string linkedIn, string twitter, string facebook)
        {
            PartnerContact partnerContact = new PartnerContact();
            partnerContact.PartnerId = partnerId;
            partnerContact.ContactLevel = level;
            partnerContact.Name = name;
            partnerContact.Email = email;
            partnerContact.Title = title;
            partnerContact.LinkedIn = linkedIn; 
            partnerContact.Twitter = twitter;
            partnerContact.Facebook = facebook;

            _context.PartnerContacts.Add(partnerContact);
            _context.SaveChanges();
            return partnerContact;
        }

        public void updateContact(int contactId, int partnerId, int level, string name, string email, string title, string linkedIn, string twitter, string facebook)
        {
            PartnerContact contactUpdate = _context.PartnerContacts.Where(p => p.ContactId == contactId).FirstOrDefault();
            contactUpdate.PartnerId = partnerId;
            contactUpdate.Name = name;
            contactUpdate.Title = title;
            contactUpdate.ContactLevel = level;
            contactUpdate.Email = email;
            contactUpdate.Facebook = facebook;
            contactUpdate.Twitter = twitter;
            contactUpdate.LinkedIn = linkedIn;
            _context.PartnerContacts.Update(contactUpdate);
            _context.SaveChanges();
        }

        public void deleteContact(String contactId)
        {
            var contact = _context.PartnerContacts.FirstOrDefault(x => x.ContactId == Convert.ToInt32(contactId));
            if (contact != null)
            {
                _context.PartnerContacts.Remove(contact);
                _context.SaveChanges();
            }
        }

        public List<PartnerContact> getAllContactByPartnerId(String partnerId)
        {
            var result = new List<PartnerContact>();
            result = _context.PartnerContacts.Where(x => x.PartnerId == Convert.ToInt32(partnerId)).ToList();
            return result;
        }
    }
}
