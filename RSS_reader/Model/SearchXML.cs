using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RSS_reader.Model
{
    class SearchXML
    {
       
        XmlDocument doc = new XmlDocument();

        private string connectString { get; set; }

        private string searchString { get; set; }

        private string xmlns { get; set; }

        public SearchXML(string connectString, string xmlns)
        {
            this.connectString = connectString;
            this.xmlns = xmlns;
        }

        public string searchOneByCategory(string category)
        {
            doc.Load(connectString);
            XmlNode root = doc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", xmlns);
            var search = "descendant::bk:item[bk:category='" + category + "']";
            XmlNode node = root.SelectSingleNode(search, nsmgr);
            return search;
        }
        public XmlNodeList searchManyByCategory(string category)
        {
            doc.Load(connectString);
            XmlNode root = doc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", xmlns);
            var search = "descendant::bk:item[bk:category='" + category + "']";
            XmlNodeList node = root.SelectNodes(search, nsmgr);
            return node;
        }
        public void showMeAllTitles()
        {
            doc.Load(connectString);
            XmlNode root = doc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", xmlns);
            var search = "descendant::bk:item";
            XmlNodeList node = root.SelectNodes(search, nsmgr);

            List<XmlNode> list = new List<XmlNode>(); //(item.FirstChild.InnerText);
            foreach (XmlNode item in node)
            {
                list.Add(item);
            }
        }

    }

           

}
    



  