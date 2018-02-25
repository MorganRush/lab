using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src.firstPart
{
    public class Article
    {
        public String Identifier { get; private set; }
        public String Title { get; private set; }
        public String Description { get; private set; }

        public Article IsPartOf { get; set; }
        public List<Article> References { get; set; }

        public Article(String identifier, String title, String description, Article isPartOf)
        {
            Identifier = identifier;
            Title = title;
            Description = description;
            IsPartOf = isPartOf;
        }
    }
}
