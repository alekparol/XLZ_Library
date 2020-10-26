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
using XLZ_Library.XLF.TransUnit;
using XLZ_Library.XLF;

namespace XLZ_Library.XLF.TransUnit.Languages.Elements
{
    public class Bpt
    {

        /* Fields */
        public XmlNode xmlBptNode;

        public string bptId;
        public string bptContent;

        /* Properties */

        public string BptId
        {
            get
            {
                return bptId;
            }
        }

        public string BptContent
        {
            get
            {
                return bptContent;
            }
        }


        /* Methods */


        /* Constructors */

        public Bpt()
        {
            xmlBptNode = null;

            bptId = "";
            bptContent = "";
        }

        public Bpt(XmlNode xmlBptNode)
        {
            this.xmlBptNode = xmlBptNode;

            bptId = xmlBptNode.Attributes["id"].Value;
            bptContent = xmlBptNode.InnerText;

        }

    }
}
