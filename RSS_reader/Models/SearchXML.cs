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
        private string mainSearch { get; set; }
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
            this.mainSearch = "descendant::bk:item";
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
        public XmlNodeList allInOne()
        {

            XmlNodeList node = root.SelectNodes(mainSearch, nsmgr);
            return node;

        }

        public List<XmlNode> listOfTitles()
        {

            XmlNodeList node = root.SelectNodes(mainSearch, nsmgr);
            List<XmlNode> listOfNodes = new List<XmlNode>();

            foreach (XmlNode item in node)
            {

                listOfNodes.Add(item.FirstChild);
            }
            return listOfNodes;
        }

        public List<XmlNode> listOfLinks()
        {
            
            XmlNodeList node = root.SelectNodes(mainSearch, nsmgr);
            List<XmlNode> listOfNodes = new List<XmlNode>();

            foreach (XmlNode item in node)
            {

                listOfNodes.Add(item.FirstChild.NextSibling);
            }
            return listOfNodes;
        }
        public List<XmlNode> listOfComments()
        {

            XmlNodeList node = root.SelectNodes(mainSearch, nsmgr);
            List<XmlNode> listOfNodes = new List<XmlNode>();

            foreach (XmlNode item in node)
            {

                listOfNodes.Add(item.FirstChild.NextSibling.NextSibling);
            }
            return listOfNodes;
        }
        public List<XmlNode> listOfGuid()
        {

            XmlNodeList node = root.SelectNodes(mainSearch, nsmgr);
            List<XmlNode> listOfNodes = new List<XmlNode>();

            foreach (XmlNode item in node)
            {

                listOfNodes.Add(item.FirstChild.NextSibling.NextSibling.NextSibling);
            }
            return listOfNodes;
        }
    }

    }

           


    



  