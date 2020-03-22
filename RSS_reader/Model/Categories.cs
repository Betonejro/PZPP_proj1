using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSS_reader.Model
{
    class Categories
    {
        public string category { get; set; }
        public bool chcecked { get; set; }
        public Categories(string category, bool check = false )
        {
            this.category = category;       
            this.chcecked = check;
        }
    }
}
