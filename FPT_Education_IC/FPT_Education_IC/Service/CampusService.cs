using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FPT_Education_IC.Service
{
    public class CampusService
    {
        private readonly DataContext _context;

        public CampusService(DataContext context)
        {
            _context = context;
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

        public ArrayList getAllCampus()
        {
            ArrayList list = new ArrayList();
            var result = _context.FptCampuses.ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }
    }
}
