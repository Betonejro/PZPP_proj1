using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_reader.Model
{
    class MongoCRUD
    {
        private IMongoDatabase db;
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table , T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
            
        }
        public bool CheckThisGuidInMongo<T>(string table, string Id)
        {
            var collection = db.GetCollection<itemRSS>(table);
            var exists = collection.AsQueryable().FirstOrDefault(avm => avm.Guid == Id) != null;
            return exists;
        }
        //Ponizej są nowe 
        public List<itemRSS> returnItemRSSFindedByCategory<T>(string table, List<string> categories)
        {
            List<itemRSS> listOfFindedItemRSS = new List<itemRSS>();
            var collection = db.GetCollection<itemRSS>(table);
           
            foreach (var categoryInListWithOneComeHere in categories)
            {
                var filter = Builders<itemRSS>.Filter.Eq("Categories", categoryInListWithOneComeHere);
                var ToReturn = collection.Find(filter).ToList();
                foreach (var item in ToReturn)
                {
                    listOfFindedItemRSS.Add(item);
                }

            }
            //Działa dla jednej kategorji
            //var filter = Builders<itemRSS>.Filter.Eq("Categories", "IMM");
            //var collection = db.GetCollection<itemRSS>(table);
            //var ToReturn = collection.Find(filter).ToList();
            //List<itemRSS> listOfFindedItemRSS = new List<itemRSS>();
            return listOfFindedItemRSS;
        }


    }
}
