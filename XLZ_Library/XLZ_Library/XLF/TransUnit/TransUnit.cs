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
using XLZ_Library.XLF.TransUnit.Languages;

/* This class intended use is to model structure of the <trans-unit></trans-unit> elements of the body node.  
 *
 * What is the typical trans-unit structure in Xlf file? 
 * 1.) Attribute "id" which can have numerical or string value; 
 * 2.) Attribute "translation" which can have string value "no" or "yes" representing boolean value. This attribute provides the CAT tools with the information if the segment contained in the given trans-unit should be exposed or blocked for translation. 
 * 3.) source node; 
 * 4.) target node (if the translation was provided); 
 * 
 * Class for modelling should contain:
 * 1.) XmlNode xmlTransUnitNode field to store the information about the XmlNode for a given trams-unit;
 * 2.) XmlNode xmlSourceNode field to store the information about the XmlNode for a source subnode of a given trans-unit node;
 * 3.) XmlNode xmlTargetNode field to store the information about the XmlNode for a target subnode of a given trans-unit node;
 * 4.) Source and Target class fields to model both source and target xml subnodes. 
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
namespace XLZ_Library.XLF.TransUnit
{
    public class TransUnit
    {

        /* Fields */

        public XmlNode xmlTransUnitNode;

        public XmlNode xmlSourceNode;
        public XmlNode xmlTargetNode;

        public Source sourceNode;
        public Target targetNode;

        /* Properties */

        public XmlNode GetXmlNode
        {
            get
            {
                return xmlTransUnitNode;
            }
        } 

        public string GetId
        {
            get
            {
                return xmlTransUnitNode.Attributes["id"].Value;
            }
        }

        public string GetTranslate
        {
            get
            {
                return xmlTransUnitNode.Attributes["translate"].Value;
            }
        }

        /* Methods */

        /* Constructors */

        public TransUnit()
        {

        }

        public TransUnit(XmlNode xmlTransUnitNode)
        {

            this.xmlTransUnitNode = xmlTransUnitNode;

            xmlSourceNode = xmlTransUnitNode.SelectSingleNode("//source");
            xmlTargetNode = xmlTransUnitNode.SelectSingleNode("//target");

        }
    }
}
