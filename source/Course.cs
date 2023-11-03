using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTutorial
{
    public class Course
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // M:N
        public virtual ICollection<Student>? Students { get; set; }

        public Course() { }
        public Course(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
