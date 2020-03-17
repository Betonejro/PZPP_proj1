using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RSS_reader.Model;
using Xunit;

namespace RSS_reader.Testy
{
    public class Media2SiteExplorations
    {
        string execute()
        {
            string url = "https://media2.pl/rss";

            return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;

        }

        string xmlResponse(string url)
        {
            System.IO.File.WriteAllText(@"D:\WriteText.txt", new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result);
            return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        [Fact]
        public void returns_response()
        {
            string result = execute();
            Assert.NotEmpty(result);
        }

        [Fact]
        public void having_xmlLinks()
        {
            var reader = new TagReader();
            reader.ReadTags();

            Assert.NotNull(reader.Tags);
        }

    }
}
