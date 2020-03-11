using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace RSS_reader.Model
{
    class TagReader
    {
        public List<itemTag> Tags = new List<itemTag>();

        
        public void ReadTags()
        {
            var url = "https://media2.pl/rss";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            //var container = doc.DocumentNode.SelectSingleNode();
            var htmlCollection = doc.DocumentNode.SelectNodes("//p/a");
            StringBuilder htmlLinks = new StringBuilder();
            foreach (var current in htmlCollection)
            {
                htmlLinks.AppendLine(current.Attributes["href"].Value);
            }

            var document = htmlLinks.ToString();
            var delims = new[] { '\r', '\n' };
            var tags = document.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in tags)
            {
                var tail = item.Substring(item.Length - 4);
                if (tail == ".xml")
                {
                    var itemSplit = item.Split('/');
                    var itemTail = itemSplit[itemSplit.Length-1];
                    var itemName = itemTail.Split('.');
                    var name = itemName[0];
                    var currentTag = new itemTag {Href = item, Name = name};
                    Tags.Add(currentTag);
                }
            }

            //System.IO.File.WriteAllText(@"D://WriteText.txt", a.ToString());
        }
    }
}
