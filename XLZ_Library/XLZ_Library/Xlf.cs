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

/* This is class to model Xlf structure. Xlf is extension of XML format, which is customized for translation process. 
 *
 * 1. What is the typical Xlf structure?
 * 1.1. Metainformation (to be described). 
 * 1.2. <Header> Tag:
 *	1.2.1. <SKL> Tag. In case of Xlf file from XLZ file, it contains tag <skl> to indicate that the file specified between those tags is related.
 *	1.2.2. <Tool> Tag. (to be described).
 * 1.3. <Body> Tag:
 *	1.3.1. <trans-unit> Tags. Body contains multiple <trans-unit> nodes which are the main point of Xlf file as they contains the strings for translation process. Those strings are displayed in two child nodes:
 *		1.3.1.1. <source> Tag. 
 *		1.3.1.2. <target Tag.
 *			1.3.1.1/2.1. Both <source> and <target> tags contains few types of content between them:
 *			- One of them is plain text for translation which is not marked in any way defaultly
 *			- Other type of content are tags for text formatting. Those tags are contained between <bpt></bpt> <ept></ept> tags, where <bpt></bpt> indicates beginning of the text formatting tag and <ept></ept> indicates the end of it. 
 *			- <it> (to be described).
 *			- <ph> (to be described).
 *			
 *	Class for modelling Xlf file should:
 *	1) Contain XmlDocument field to store the actual XML document which is parsed in this class. This is done by the field of the name "xlfDocument". 
 *	2) Contain three fields modeling three main parts of Xlf structure:
 *	 2.1.) XmlNode metaInformation;
 *	 2.2.) XmlNode header;
 *	 2.3.) XmlNode body;
 *	3) 
 *	
 *	Class for modelling Xlf file therefore should permit to:
 *	1) Create and call 
 *	
 *	
 *	Class for modelling Xlf file should not permint to:
 *	1) Get nor set of based XmlDocument. 
 *	2) 
 */

namespace XLZ_Library
{
    public class Xlf
    {

		/* Fields */

		public XmlDocument xlfDocument;

		public XmlNode meta;
		public XmlNode head;
		public XmlNode body;

		public XmlNodeList xmlTransUnitList;
		public List<TransUnit> transUnitList;

		/* Properties */

		public int GetLengthOfTransUnitList
        {
            get
            {
				return transUnitList.Count;
            }
        }



		/* Methods */
		/* TODO: Add a validation method. */

		public TransUnit GetTransUnit(int Id)
		{
			if (Id <= transUnitList.Count)
			{
				return transUnitList[Id];
			}
			else
			{
				return null;
			}
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

			xlfDocument = new XmlDocument();
			xlfDocument.Load(inputFile);

			meta = xlfDocument.SelectSingleNode("//xliff");
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
