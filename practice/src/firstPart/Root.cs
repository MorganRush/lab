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

        public Article Article { get; private set; }

        private Root()
        {
            Article = new Article("root", "root", "root", null);
        }

        public static Root getRoot()
        {
            if (root == null)
                root = new Root();
            return root;
        }
    }
}
