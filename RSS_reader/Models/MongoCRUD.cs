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

        public void InsertSourceToDatabase<T>(string collectionName, T source)
        {
            var collection = db.GetCollection<T>(collectionName);
            collection.InsertOne(source);
        }

        public bool CheckRecordInCollection(string collectionName, string primaryKey)
        {
            var collection = db.GetCollection<itemTag>(collectionName);
            var isExist = collection.AsQueryable().FirstOrDefault(tag => tag.Name == primaryKey) != null;
            return isExist;
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
            return listOfFindedItemRSS;

        }
        //Działa dla jednej kategorji
        public List<itemRSS> returnAllForOneCategory<t>(string table , string category)
        {
          
            var filter = Builders<itemRSS>.Filter.Eq("Categories", category);
            var collection = db.GetCollection<itemRSS>(table);
            var ToReturn = collection.Find(filter).ToList();
            //string taa = " ";
            //foreach (var item in ToReturn)
            //{
            //    taa += item.Title;
            //}
            //System.IO.File.WriteAllText(@"C:\Users\Krute\OneDrive\Pulpit\TO.txt", taa);
            return ToReturn;
        }
        
        public List<itemTag> returnAllSources<T>(string table)
        {
            var collection = db.GetCollection<itemTag>(table);
            List<itemTag> returnList = new List<itemTag>();
            foreach (var item in collection.AsQueryable())
            {
                returnList.Add(item);
            }
            
            return returnList;
        }
        public List<itemRSS> returnAllRSSItems<T>(string table)
        {
            var collection = db.GetCollection<itemRSS>(table);
            List<itemRSS> listOdRSSItem = new List<itemRSS>();
            foreach (var item in collection.AsQueryable())
            {
                listOdRSSItem.Add(item);  
            }
            return listOdRSSItem;

        }

        public List<itemRSS> returnXRSSItems<T>(string table, int count)
        {
            if (count <= 0 ) throw new Exception("returnXRSSItems wrong count");

            var collection = db.GetCollection<itemRSS>(table);
            List<itemRSS> itemList = new List<itemRSS>();
            foreach (var item in collection.Find(x => true).Limit(count).ToList())
            {
                itemList.Add(item);
            }

            return itemList;
        }
        public List<string> returnAllCategoiresInMongo<T>(string table)
        {
            List<string> toReturn = new List<string>();
            List<string> allCategories = new List<string>();
            

            var collection = db.GetCollection<itemRSS>(table);
            var querableCollection = collection.AsQueryable();
            foreach (var ItemInCollection in querableCollection)
            {

                foreach (var simplyCategory in ItemInCollection.Categories)
                { 

                    allCategories.Add(simplyCategory.ToString());
                }

            }

            return allCategories.Distinct().ToList();
        }
        public List<Categories> returnOnlyAllCategoiresInMongoToList<T>(string table)
        {
            List<string> toReturn = new List<string>();
            List<Categories> allCategories = new List<Categories>();


            var collection = db.GetCollection<itemRSS>(table);
            var querableCollection = collection.AsQueryable();
            foreach (var ItemInCollection in querableCollection)
            {

                foreach (var simplyCategory in ItemInCollection.Categories.Distinct())
                {
                    toReturn.Add(simplyCategory);

                }



            }
            foreach (var item in toReturn.Distinct())
            {
                allCategories.Add(new Categories(item));
            }

           

            return allCategories.Distinct().ToList();
        }


    }
}
