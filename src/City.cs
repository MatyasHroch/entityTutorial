using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTutorial
{
    public class City
    {
        public int Id { get; set; } 
        public string Name { get; set; } = "";
        public int Population { get; set; } = 0;
        public string Motto { get; set; } = "";

        public City() { }

    }
}
