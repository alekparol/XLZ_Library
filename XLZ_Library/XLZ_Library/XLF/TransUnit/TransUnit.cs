﻿using System;
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
 * 1.
 * 
 * Class for modelling should contain:
 * 
 * Class for modelling should permit to:
 * 1) Use methods:
 * 2) Use Constructors:
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

        /* Not all ids are int. For example document.xml.3 */
        public int GetId
        {
            get
            {
                return Int32.Parse(xmlTransUnitNode.Attributes["id"].Value);
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
