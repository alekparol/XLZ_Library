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

namespace XLZ_Library.XLF.TransUnit
{
    public class TransUnit
    {

        /* Fields */

        private XmlNode xmlTransUnitNode;

        private XmlNode sourceNode;
        private XmlNode targetNode;

        /* Methods */

        /* Constructors */

        public TransUnit(XmlNode xmlTransUnitNode)
        {

            this.xmlTransUnitNode = xmlTransUnitNode;

            sourceNode = xmlTransUnitNode.SelectSingleNode("//source");
            targetNode = xmlTransUnitNode.SelectSingleNode("//target");

        }


    }
}
