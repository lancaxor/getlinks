using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetLinks
{
    public class SiteItem
    {
        String link;
        String title;
        int depth;
        SiteItem parent;

        public String URL { get { return this.link; } set { this.link = value; } }
        public String Title { get { return this.title; } set { this.title = value; } }
        public int Depth { get { return this.depth; } set { this.depth = value; } }
        public SiteItem Parent { get { return this.parent; } set { this.parent = value; } }
    }
}
