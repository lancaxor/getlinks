using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

namespace GetLinks
{
    abstract class XmlWorker
    {
        public static void Save(List<SiteItem> collection, String pathToFile)
        {
            //var collection = Collection;

            var xml = new XElement("SiteRoot",
                new XAttribute("Title", collection[0].Title),
                new XAttribute("Link", collection[0].URL),
                new XAttribute("Depth", collection[0].Depth));     //root element

            collection.RemoveAt(0);


            int maxDepth = (from md in collection orderby md.Depth descending select md.Depth).ToArray()[0];

            for (int i = 1; i <= maxDepth; i++)      //add to xml in depth
            {
                var currLvlEls = from col in collection where (col.Depth==i) select col;
                if (currLvlEls.Count() == 0)
                    break;      //no items in current and more levels

                foreach (var element in currLvlEls)
                {
                    var curXmlElem = new XElement("Site",
                        new XAttribute("Title", element.Title),
                        new XAttribute("Link", element.URL),
                        new XAttribute("Depth", element.Depth));                //current element

                    var parXmlElem = new XElement("Site",
                        new XAttribute("Title", element.Parent.Title),
                        new XAttribute("Link", element.Parent.URL),
                        new XAttribute("Depth", element.Parent.Depth));       //parent element

                    if (xml.Elements().Count() == 0)        //no elements
                        xml.Add(curXmlElem);
                    else
                    {
                        var parInXml = from el in xml.DescendantsAndSelf("Site") where el.Attribute("Link").Value.Equals(parXmlElem.Attribute("Link").Value) select el; //link is unique
                        if (parInXml.Count() == 0)
                            xml.Add(curXmlElem);
                        else
                            parInXml.ToArray()[0].Add(curXmlElem);
                    }
                    //collection.Remove(element);
                }
            }

            xml.Save(pathToFile);
            System.Windows.Forms.MessageBox.Show("File saved successfully!");
        }

        private static SiteItem[] GetChildren (SiteItem e)
        {
            return null;
        }
    }
}