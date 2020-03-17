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
              
            }
            
            
        }
        private string convertToJSON(itemRSS rssItem)
        {
           
            var sJSONResponse = JsonConvert.SerializeObject(rssItem);
            return sJSONResponse;

           
        }
      
        public void test()
        {
            MongoCRUD db = new MongoCRUD("TestBase");
            db.InsertRecord("Rss", new itemRSS());
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
