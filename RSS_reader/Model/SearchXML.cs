using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RSS_reader.Model
{
    class Xpath
    {
        XmlDocument doc = new XmlDocument();

        private string connectString { get; set; }

        private string searchString { get; set; }

        private string xmlns { get; set; }
        private XmlNode root { get; set; }
        private XmlNamespaceManager nsmgr { get; set; }
        public Xpath(string connectString, string xmlns)
        {
            this.connectString = connectString;
            this.xmlns = xmlns;
            doc.Load(connectString);
            this.root = doc.DocumentElement;
            this.nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", xmlns);
        }

        public XmlNode searchOneByCategory(string category)
        {

            var search = "descendant::bk:item[bk:category='" + category + "']";
            XmlNode node = root.SelectSingleNode(search, nsmgr);

            return node;
        }
        public XmlNodeList searchManyByCategory(string category)
        {

            var search = "descendant::bk:item[bk:category='" + category + "']";
            XmlNodeList node = root.SelectNodes(search, nsmgr);
            return node;
        }
        public XmlNodeList showMeAllTitles()
        {

            var search = "descendant::bk:item";
            XmlNodeList node = root.SelectNodes(search, nsmgr);

            return node;

        }

    }

           

}
    



  