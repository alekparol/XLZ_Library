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

namespace XLZ_Library
{
    public class Xlf
    {

		/* Fields */

		public XmlDocument xlfDocument;

		public XmlNode head;
		public XmlNode body;

		public XmlNodeList xmlTransUnitList;
		public List<TransUnit> transUnitList;

		/* Properties */


		/* Constructors */

		public Xlf(XmlDocument inputFile)
		{
			/* Here should be added validation of XML file. */
			xlfDocument = inputFile;

			head = xlfDocument.SelectSingleNode("//header");
			body = xlfDocument.SelectSingleNode("//body");

			xmlTransUnitList = inputFile.GetElementsByTagName("trans-unit");
			transUnitList = new List<TransUnit>();

		}

	}
}
