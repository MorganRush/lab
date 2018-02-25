using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src.firstPart
{
    public class Root
    {
        private static Root root;

        public List<Article> References { get; set; }

        private Root(List<Article> References)
        {
            this.References = References;
        }

        public static Root getRoot(List<Article> References)
        {
            if (root == null)
                root = new Root(References);
            return root;
        }
    }
}
