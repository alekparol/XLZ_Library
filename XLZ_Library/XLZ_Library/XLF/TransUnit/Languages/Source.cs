using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using XLZ_Library;
using XLZ_Library.XLF.TransUnit.Languages.Elements;
using XLZ_Library.XLF.TransUnit.Languages.Elements;

/* This class intended use is to model structure of the <source></source> elements of the trans-unit node.  
 *
 * What is the typical for source node structure in Xlf file? 
 * 1.) <it> node;
 * 2.) <ph> node;
 * 3.) <bpt> node;
 * 4.) <ept> node;
 * 5.) Expressions for translation.
 * 
 * Class for modelling should contain:
 * 1.) XmlNode xmlSourceNode field to store <source> XmlNode with its XmlAttributes;
 * 2.) XmlNodeList xmlItList field to store the XmlNodeList for all child nodes which are <it> nodes;
 * 3.) XmlNodeList xmlPhList field to store the XmlNodeList for all child nodes which are <ph> nodes;
 * 4.) XmlNodeList xmlBptList field to store the XmlNodeList for all child nodes which are <bpt> nodes;
 * 5.) XmlNodeList xmlEptList field to store the XmlNodeList for all child nodes which are <ept> nodes;
 * 6.) It, Ph, Bpt, Ept class fields to model all aforementioned XmlNodes fields.
 * 
 * Class for modelling should permit to:
 * 1.) Use Properties:
 *      1.1.) GetXmlNode to return the xmlTransUnitNode;
 *      1.2.) GetId to return string value of the "id" attribute;
 *      1.3.) GetTranslate to return string value of the "translatable" attribute.
 * 2.) Use Methods:
 * 3.) Use Constructors:
 *      1.1.) Empty constructor to set all fields to null;
 *      1.2.) Constructor to which XmlNode is passed with trans-unit node. 
 *		
 * Class for modeling should not permit to:
 * 
 *
 */

namespace XLZ_Library.XLF.TransUnit.Languages
{
    public class Source
    {

        /* Fields */

        public XmlNode xmlSourceNode;

        public XmlNodeList xmlItList;
        public XmlNodeList xmlPhList;
        public XmlNodeList xmlBptList;
        public XmlNodeList xmlEptList;

        public LinkedList<It> itList;
        public LinkedList<Ph> phList;
        public LinkedList<Bpt> bptList;
        public LinkedList<Ept> eptList;

        /* Properties */

        /* Methods */

        /* Constructors */

        public Source()
        {

        }

        public Source(XmlNode xmlSourceNode)
        {
            this.xmlSourceNode = xmlSourceNode;

            xmlItList = xmlSourceNode.SelectNodes("//it");
            xmlPhList = xmlSourceNode.SelectNodes("//ph");
            xmlBptList = xmlSourceNode.SelectNodes("//bpt");
            xmlEptList = xmlSourceNode.SelectNodes("//ept");

            itList = new LinkedList<It>();
            var auxiliaryElement = new It();

            foreach(XmlNode itElement in xmlItList)
            {
                auxiliaryElement = new It(itElement);
                itList.AddLast(auxiliaryElement);
            }

        }

    }
}
