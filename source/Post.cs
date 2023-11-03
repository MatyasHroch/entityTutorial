using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EntityTutorial
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        public int BlogId { get; set; }
        //public virtual Blog Blog { get; set; } = null!; // Required reference navigation to principal
    }
}
