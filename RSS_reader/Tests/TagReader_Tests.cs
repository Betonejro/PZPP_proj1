using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using MongoDB.Bson;
using RSS_reader.Model;
using Xunit;

namespace RSS_reader.Tests
{
    public class TagReader_Tests
    {
        void execute()
        {
            
        }

        [Fact]
        public void site_response()
        {
            string url = "https://media2.pl/rss";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Assert.NotNull(response);
        }
        [Fact]
        public void class_gets_new_sources()
        {
            var reader = new TagReader();
            reader.ReadTags();

            Assert.NotNull(reader.Tags);
        }
    }
}
