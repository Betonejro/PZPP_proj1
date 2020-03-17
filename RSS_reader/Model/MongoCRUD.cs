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
            //var collection = db.GetCollection<itemRSS>(table);
            //bool exists = collection.AsQueryable().Any(iRSS=>iRSS.Guid == Id) != false;

           
            //return exists;
            var collection = db.GetCollection<itemRSS>(table);
            var exists = collection.AsQueryable().FirstOrDefault(avm => avm.Guid == Id) != null;
            return exists;
        }


    }
}
