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

namespace XLZ_Library.XLF.TransUnit.Languages.Elements
{
    public class Ept
    {

        /* Fields */

        public XmlNode xmlEptNode;

        /* Properties */

        /* Methods */

        /* Constructors */

        public Ept()
        {

        }

        public Ept(XmlNode xmlEptNode)
        {

            this.xmlEptNode = xmlEptNode;

        }

    }
}
