using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_reader.Model
{
    class itemRSS
    {
        public ObjectId id { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string Parent { get; set; }
        public string Comments { get; set; }
        public string Guid { get; set; }
        public List<string> Categories = new List<string>();
        public string Description { get; set; }
        public string PubDate { get; set; }
      

        public DateTime GetDatatime()
        {
            var time = new DateTime();

            time = DateTime.Parse(this.PubDate);

            return time;
        }
        public string GetDescriptionText()
        {
            string descriptionText = "";

            var descriptionSplit = this.Description.Split('>');
            descriptionText = descriptionSplit[descriptionSplit.Length - 1];

            return descriptionText;
        }
    }
}
