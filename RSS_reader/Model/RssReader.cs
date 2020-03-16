using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml;
using Newtonsoft.Json;

namespace RSS_reader.Model
{
    class RssReader
    {
        public List<itemRSS> Items = new List<itemRSS>();
        public void ReadItemsFromCanal(string url)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            var itemNodes = xmlDoc.SelectNodes("//rss/channel/item");

            if (itemNodes.Count < 1) return;

            var Parent = GetParent(url);

            foreach (XmlNode item in itemNodes)
            {
                var rssItem = new itemRSS();
                rssItem.Title = item.SelectSingleNode("title").InnerText;
                rssItem.Href = item.SelectSingleNode("link").InnerText;
                rssItem.Parent = Parent;
                rssItem.Comments = item.SelectSingleNode("comments").InnerText;
                rssItem.Guid = item.SelectSingleNode("guid").InnerText;
                var categories = item.SelectNodes("category");
                if (categories.Count > 0)
                {
                    foreach (XmlNode category in categories)
                    {
                        rssItem.Categories.Add(category.InnerText);
                    }
                }
                rssItem.Description = item.SelectSingleNode("description").InnerText;
                rssItem.PubDate = item.SelectSingleNode("pubDate").InnerText;
                convertToJSON(rssItem);
               
                //  System.IO.File.WriteAllText(@"D://WriteText.txt", rssItem.GetDatatime().ToString());
            }
            
            
        }
        private void convertToJSON(itemRSS rssItem)
        {
           StreamWriter streamWriter = new StreamWriter(@"C:\Users\Krute\OneDrive\Pulpit/TO.txt", true);
            var sJSONResponse = JsonConvert.SerializeObject(rssItem);

            streamWriter.WriteLine(sJSONResponse);

            //foreach (var item in sJSONResponse)
            //{
            //    streamWriter.Write(item);
            //}
            
            
            streamWriter.Close();
        }

     


        public string GetParent(string url)
        {
            var itemSplit = url.Split('/');
            var itemTail = itemSplit[itemSplit.Length - 1];
            var itemName = itemTail.Split('.');
            string name = itemName[0];

            return name;
        }

    
    }

}
