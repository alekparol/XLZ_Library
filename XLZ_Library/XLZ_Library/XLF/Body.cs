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

/* This class intended use is to model structure of the <body></body> part of the Xlf document.  
 *
 * What is the typical body structure in Xlf file? 
 * 1. Between <body> tags you can find the list of <trans-unit> nodes which are the only childres of body node. [Add if something is missing]
 * 
 * Class for modelling should contain:
 * 1) XmlNode xmlBody which stores <body></body> node of the XmlDocument;
 * 2) XmlNodeList xmlTransUnitList list of all XML <trans-unit> nodes within body;
 * 3) LinkedList of TransUnit elements initialied with aforementioned XmlNodeList.
 * 
 * Class for modelling should permit to:
 * 1) Call the Get method for accessing the value of LinkedList length;
 * 2) Call the GetTransUnit(int integer) method to get the TransUnit element by its position in the list;
 * 3) Call the GetTransUnitByAttribute(string attributeName, string attributeValue) method to get the TransUnit element by chosen attribute's value;
 * 4) Call the GetTransUnitById(string id) method to get the TransUnit element by the attribute "id" whereas passing value is string [This will be extensively covered in Trans-Unit class];
 * 5) Call the Get[First/Last]TransUnitNode() method to get the first or last TransUnit element of the LinkedList;
 * 6) Call the Get[Previous/Next]TransUnitNode(TransUnit currentTransUnit) method to get the previous or the next TransUnit element on the LinkedList;
 * 7) 
 *
 */

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

		public TransUnit GetFirstTransUnit()
		{
			return transUnitList.First();
		}

		public TransUnit GetLastTransUnit()
        {
			return transUnitList.Last();
        }

		public TransUnit GetPreviousTransUnit(TransUnit currentTransUnit)
        {
			return transUnitList.Find(currentTransUnit).Previous.Value;

		}

		public TransUnit GetNextTransUnit(TransUnit currentTransUnit)
        {
			return transUnitList.Find(currentTransUnit).Next.Value;
        }

		public IEnumerable<TransUnit> GetTransUnitSublistOnAttributes(string attributeName, string attributeValue)
        {
			IEnumerable<TransUnit> subList = transUnitList.Where(node => node.GetXmlNode.Attributes[attributeName].Value == attributeValue);

			return subList;
		}

		public IEnumerable<TransUnit> GetUntranslatableNodes()
		{
			return GetTransUnitSublistOnAttributes("translate", "no");
		}

		public IEnumerable<TransUnit> GetTranslatableNodes()
		{
			return GetTransUnitSublistOnAttributes("translate", "yes");
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
