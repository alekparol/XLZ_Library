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


		/* Methods */
		public bool IsXLFValid(string inputFile)
        {
			try
			{
				XmlDocument document = new XmlDocument();
				document.LoadXml(inputFile);
			}
			catch
			{
				return false;
			}
			return true;
		}

		/* Constructors */

		public Xlf(string inputFile)
		{
			/* Here should be added validation of XML file. */
			xlfDocument.LoadXml(inputFile);
			
			head = xlfDocument.SelectSingleNode("//header");
			body = xlfDocument.SelectSingleNode("//body");

			xmlTransUnitList = xlfDocument.GetElementsByTagName("trans-unit");
			transUnitList = new List<TransUnit>();

		}

	}
}
