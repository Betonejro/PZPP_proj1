using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace RSS_reader.Model
{
    class itemTag
    {
        public ObjectId id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }

        


    }
}
