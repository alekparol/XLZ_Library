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

namespace XLZ_Library.XLF.TransUnit
{
    public class TransUnit
    {

        /* Fields */

        public XmlNode xmlTransUnitNode;

        public XmlNode sourceNode;
        public XmlNode targetNode;

        /* Methods */

        public XmlNode GetPreviousTransUnit()
        {
            return xmlTransUnitNode.PreviousSibling;
        }

        /* Constructors */

        public TransUnit(XmlNode xmlTransUnitNode)
        {

            this.xmlTransUnitNode = xmlTransUnitNode;

            sourceNode = xmlTransUnitNode.SelectSingleNode("//source");
            targetNode = xmlTransUnitNode.SelectSingleNode("//target");

        }


    }
}
