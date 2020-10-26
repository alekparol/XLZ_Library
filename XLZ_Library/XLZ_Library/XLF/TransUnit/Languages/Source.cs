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

/* This class intended use is to model structure of the <source></source> elements of the body node.  
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

namespace XLZ_Library.XLF.TransUnit.Languages
{
    public class Source
    {

        /* Fields */

        public XmlNode xmlSourceNode;

        public XmlNodeList xmlBptList;
        public XmlNodeList xmlEptList;
        public XmlNodeList xmlItList;
        public XmlNodeList xmlPhList;

        /* Properties */

        /* Methods */

        /* Constructors */

    }
}
