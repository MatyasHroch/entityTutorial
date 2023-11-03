using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTutorial
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CountryCode { get; set; } = "";
        
        // 1:1
        public virtual City CapitalCity { get; set; } = new City();

        // 1:N
        public virtual ICollection<City> AllCities { get; set; } = new List<City>();

        public Country() { }

    }
}
