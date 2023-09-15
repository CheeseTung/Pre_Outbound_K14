using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using System.Collections;

namespace FPT_Education_IC.Service
{
    public class CountryService
    {
        private readonly DataContext _context;

        public CountryService(DataContext context)
        {
            _context = context;
        }

        public ArrayList getAllCountry()
        {
            ArrayList allCountries = new ArrayList();

            var result = _context.Countries
                .Select(c => new Country
                {
                    IsoCode = c.IsoCode,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();

            if (result.Count > 0)
            {
                allCountries.AddRange(result);
            }
            return allCountries;
        }

        public string GetCountryName(string code)
        {
            string name = "";
            if (!string.IsNullOrEmpty(code))
            {
                Country country = _context.Countries.FirstOrDefault(c => c.IsoCode == code);
                if (country != null)
                {
                    name = country.Name;
                }
            }
            return name;
        }
    }
}
