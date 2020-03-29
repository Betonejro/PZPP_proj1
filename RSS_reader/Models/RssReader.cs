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
        MongoCRUD mg = new MongoCRUD("BaseOfRssItems");
        const string itemCollection = "Collection";

        public void ReadItemsFromCanal(string url)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            var itemNodes = xmlDoc.SelectNodes("//rss/channel/item");

            if (itemNodes.Count < 1) return;

            var Parent = GetParent(url);

            foreach (XmlNode item in itemNodes)
            {
                if (mg.CheckThisGuidInMongo<itemRSS>(itemCollection, item.SelectSingleNode("guid").InnerText) == true)
                {
                    return;
                }
                var rssItem = new itemRSS();
                rssItem.Title = item.SelectSingleNode("title").InnerText;
                rssItem.Href = item.SelectSingleNode("link").InnerText;
                rssItem.Parent = Parent;
                if (item.SelectSingleNode("comments") != null)
                {
                    rssItem.Comments = item.SelectSingleNode("comments").InnerText;
                }
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
                rssItem.PubDate = GetDateFromElement(item.SelectSingleNode("pubDate").InnerText).ToString();
                Items.Add(rssItem);
            }

            SaveToDB();
            Items.Clear();
        }

        private DateTime GetDateFromElement(string element)
        {
            var time = new DateTime();

            time = DateTime.Parse(element);

            return time;
        }
        public void ReadItemsFromMultipleSources(List<itemTag> tags)
        {
            foreach (var source in tags)
            {
                ReadItemsFromCanal(source.Href);
            }
        }

        private string convertToJSON(itemRSS rssItem)
        {
            var sJSONResponse = JsonConvert.SerializeObject(rssItem);
            return sJSONResponse;
        }

        private string convertToJSONFromList(List<itemRSS> rssItem)
        {
            var sJSONResponse = JsonConvert.SerializeObject(rssItem);
            return sJSONResponse;
        }

        public string GetParent(string url)
        {
            var itemSplit = url.Split('/');
            var itemTail = itemSplit[itemSplit.Length - 1];
            var itemName = itemTail.Split('.');
            string name = itemName[0];

            return name;
        }

        private void SaveToDB()
        {
            foreach (var item in Items)
            {

                if (mg.CheckThisGuidInMongo<itemRSS>(itemCollection, item.Guid) != true)
                {
                    mg.InsertRecord(itemCollection, item);
                }

            }
        }
    }

}
