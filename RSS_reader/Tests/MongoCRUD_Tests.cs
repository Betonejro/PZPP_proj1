using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSS_reader.Model;
using Xunit;

namespace RSS_reader.Tests
{
    public class MongoCRUD_Tests
    {
        MongoCRUD db = new MongoCRUD("DatabaseTesting");
        string collection = "Tests";
        [Fact]
        public void MongoCreateReadTests()
        {
            string testingData = Guid.NewGuid().ToString();
            var data = new itemTag { Href = testingData, Name = testingData };
            db.InsertSourceToDatabase(collection, data);

            var sources = db.returnAllSources<itemTag>(collection);
            Assert.Equal(data.Name, sources.Find(x => x.Name.Equals(data.Name)).Name);
        }

    }
}

