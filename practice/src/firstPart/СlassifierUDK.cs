using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice.src.firstPart
{
    public class СlassifierUDK
    {
        public Article root;
        
        public void CreateTree()
        {
            root = new Article("root", "root", "root", null);
        }

        public Article FindElement(String identifier, Article startSearch)
        {
            int length = identifier.Length;
            for(int i = 0; i < length; i++)
            {
                Article article = startSearch;
                foreach (Article a in article.References)
                {
                    if(a.Identifier[i] == identifier[i])
                    {
                        if (a.Identifier.CompareTo(identifier) == 0)
                            return a;
                        else if (a.Identifier[i] == '.' || a.Identifier[i + 1] != identifier[i + 1])
                            continue;
                        article = a;
                        break;
                    }
                }
            }
            return null;
        }

        public void AddElement(Article article)
        {
            String identifier = article.Identifier;
            int length = identifier.Length;
            int indexPoint = identifier.IndexOf(".");
            Article element = root;
            if (indexPoint != -1)
            {
                String[] array = identifier.Split('.');
                for (int i = 0; i < array.Length-1; i++)
                {
                    element = FindElement(array[i], element);
                }
                identifier = array[array.Length - 1];
            }
            element = FindElement(identifier.Remove(identifier.Length - 1), element);
            element.References.Add(article);
        }
    }
}
