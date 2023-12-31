﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTutorial
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Post> Posts { get; set; }

        public Blog() { }
    }
}
