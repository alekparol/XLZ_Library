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
 * 1) Use methods:
 *		1.1.) GetLengthOfTransUnitList method for accessing the value of LinkedList length;
 *		1.2.) GetTransUnit(int integer) method to get the TransUnit element by its position in the list;
 *		1.3.) GetTransUnitByAttribute(string attributeName, string attributeValue) method to get the TransUnit element by chosen attribute's value;
 *		1.4.) GetTransUnitById(string id) method to get the TransUnit element by the attribute "id" whereas passing value is string [This will be extensively covered in Trans-Unit class];
 *		1.5.) Get[First/Last]TransUnitNode() method to get the first or last TransUnit element of the LinkedList;
 *		1.6.) Get[Previous/Next]TransUnitNode(TransUnit currentTransUnit) method to get the previous or the next TransUnit element on the LinkedList;
 *		1.7.) GetTransUnitSublistOnAttribute(string attributeName, string attributeValue) method to get the sublist of a LinkedList based on the value of a chosen attribute;
 *		1.8.) Get[Untranslatable/Translatable]Nodes method to get a sublist of a LinkedList of all TransUnit nodes which has "translate" attribute set on "no" or "yes" value respecitevely.
 * 2) Use Constructors:
 *		1.1.) Without arguments passed, to set all fields to null; 
 *		1.2.) With <body> node argument. 
 *		
 * Class for modeling should not permit to:
 * 
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

		public Body()
        {
			xmlBody = null;

			xmlTransUnitList = null;
			transUnitList = new LinkedList<TransUnit>();
		}

		public Body(XmlNode xmlBody)
		{
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
