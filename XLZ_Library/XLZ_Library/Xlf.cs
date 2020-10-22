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

/**/

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
		public bool IsXlfValid(string inputFile)
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

		public bool IsTransUnitInBody(TransUnit transUnit)
        {
			if (transUnit.GetXmlNode.ParentNode == body)
			{
				return true;
			}
            else
            {
				return false;
            }
        }

		public bool AreAllTransUnitsInBody(List<TransUnit> transUnitList)
        {
			if (transUnitList.All(transUnit => IsTransUnitInBody(transUnit) == true))
			{
				return true;
			}
            else
            {
				return false;
            }
        }

		/* Checking if the body of the Xlf and the head and metainformation are in a correct format. */
		/*public bool IsXlfParsedCorrectly()
        {

        }*/

		public TransUnit GetPreviousTransUnit(TransUnit transUnit)
        {
			XmlNode previousXmlNode = transUnit.xmlTransUnitNode.PreviousSibling;

			if (transUnitList.Where(node => node.xmlTransUnitNode.Equals(previousXmlNode)).Count() > 0)
			{
				return transUnitList.Where(node => node.xmlTransUnitNode.Equals(previousXmlNode)).ElementAt(0);
			}
            else
            {
				return null;
            }
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

			TransUnit auxiliaryTransUnit = new TransUnit();
			foreach(XmlNode xmlNode in xmlTransUnitList)
            {
				auxiliaryTransUnit = new TransUnit(xmlNode);
				transUnitList.Add(auxiliaryTransUnit);
            }

		}

	}
}
