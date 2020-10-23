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
    public class Body
    {

        /* Fields */

        public XmlNode xmlBody;

        public XmlNodeList xmlTransUnitList;
        public LinkedList<TransUnit> transUnitList;

		/* Properties */

		public int GetLengthOfTransUnitList
		{
			get
			{
				return transUnitList.Count;
			}
		}

		/* Methods */

		public TransUnit GetTransUnit(int position)
		{
			if (position <= transUnitList.Count)
			{
				return transUnitList.ElementAt(position);
			}
			else
			{
				return null;
			}
		}

		public TransUnit GetTransUnitByAttributeValue(string attributeName, string attributeValue)
        {

			IEnumerable<TransUnit> subList = transUnitList.Where(node => node.GetXmlNode.Attributes[attributeName].Value == attributeValue);

			if (subList.Count() > 0)
            {
				return subList.ElementAt(0);
            }
            else
            {
				return null;
            }

        }

		public TransUnit GetTransUnitByID(string id)
        {
			return GetTransUnitByAttributeValue("id", id);
        }

		public IEnumerable<TransUnit> GetUntranslatableNodes()
        {
			IEnumerable<TransUnit> subList = transUnitList.Where(node => node.GetXmlNode.Attributes["translate"].Value == "no");
			
			return subList;

		}

		public IEnumerable<TransUnit> GetTranslatableNodes()
        {
			IEnumerable<TransUnit> subList = transUnitList.Where(node => node.GetXmlNode.Attributes["translate"].Value == "yes");

			return subList;
		}

		/*public TransUnit GetTransUnit(int Id)
        {
			if (transUnitList.Where(node => node.GetId.Equals(Id)).Count() > 0)
			{
				return transUnitList.Where(node => node.GetId.Equals(Id)).ElementAt(0);
			}
			else
			{
				return null;
			}
		}*/

		public bool IsTransUnitInBody(TransUnit transUnit)
		{
			if (transUnit.GetXmlNode.ParentNode == xmlBody)
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

		public Body(XmlNode xmlBody)
		{
			/* Here should be added validation of XML file. */
			this.xmlBody = xmlBody;
				
			xmlTransUnitList = xmlBody.SelectNodes("//trans-unit");
			transUnitList = new LinkedList<TransUnit>();

			TransUnit auxiliaryTransUnit = new TransUnit();
			foreach (XmlNode xmlNode in xmlTransUnitList)
			{
				auxiliaryTransUnit = new TransUnit(xmlNode);
				transUnitList.AddLast(auxiliaryTransUnit);
			}

		}
	}
}
