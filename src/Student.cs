using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTutorial
{
    public class Student
    {
        public int Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // M:N
        public virtual ICollection<Course>? Courses { get; set; }

        public Student() { }
        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, id: {Id}";
        }

        public bool ProperDelete()
        {
            // here we can execute some additional code
            return true;
        }
    }
}
