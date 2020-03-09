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
            var tag = doc.DocumentNode.SelectNodes("//p/a");
            StringBuilder a = new StringBuilder();
            foreach (var x in tag)
            {
                a.AppendLine(x.Attributes["href"].Value);
            }

            string document = a.ToString();
            char[] delims = new[] { '\r', '\n' };
            var tags = document.Split(delims, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < tags.Length; i++)
            {
                string tail = tags[i].Substring(tags[i].Length - 4);
                if (tail == ".xml")
                {
                    var splited = tags[i].Split('/');
                    string namewithxmltail = splited[splited.Length-1];
                    var namesplit = namewithxmltail.Split('.');
                    string name = namesplit[0];
                    var item = new itemTag();
                    item.Href = tags[i];
                    item.Name = name;
                    Tags.Add(item);
                }
            }

            //System.IO.File.WriteAllText(@"D://WriteText.txt", a.ToString());
        }
    }
}
