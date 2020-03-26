using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;


namespace RSS_reader.Model
{
    class TagReader
    {
        public List<itemTag> Tags = new List<itemTag>();
        MongoCRUD mg = new MongoCRUD("BaseOfRssItems");
        const string sourcesCollection = "Sources";

        public void ReadTags()
        {
      
            var url = "https://media2.pl/rss";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            //var container = doc.DocumentNode.SelectSingleNode();
            var htmlCollection = doc.DocumentNode.SelectNodes("//p/a");
            var htmlLinks = new StringBuilder();
            foreach (var current in htmlCollection)
            {
                htmlLinks.AppendLine(current.Attributes["href"].Value);
            }
          
            var document = htmlLinks.ToString();
            var newLine = new[] { '\r', '\n' };
            var tags = document.Split(newLine, StringSplitOptions.RemoveEmptyEntries);
           
            foreach (var item in tags)
            {
                var tail = item.Substring(item.Length - 4);

                if (tail != ".xml") continue;

                var itemSplit = item.Split('/');
                var itemTail = itemSplit[itemSplit.Length-1];
                var itemName = itemTail.Split('.');
                var name = itemName[0];
                var currentTag = new itemTag {Href = item, Name = name};
                Tags.Add(currentTag);

            }

            SaveToDB();
         
        }

        private void SaveToDB()
        {
            foreach (var sources in Tags)
            {
                
                if (mg.CheckRecordInCollection(sourcesCollection,sources.Name) != true)
                {
                    mg.InsertSourceToDatabase(sourcesCollection, sources);
                }

            }
        }

        private void toJSON()
        {
            
            string sJSONResponse = JsonConvert.SerializeObject(Tags);

        }
    }
}
