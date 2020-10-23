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
 *	3) Contain three fields of type of custom class for modellin aforementioned parts of Xlf structure. 
 *	
 *	Class for modelling Xlf file therefore should permit to:
 *	1) Call the Get methods for accsessing meta, head and body fields.  
 *	2)
 *	3) Create an object by passing:
 *	1.1.) No argument;
 *	1.2.) String inputFile argument which will denote the path to the text file.
 *	1.3.) XmlDocument inputFile argument which will denote the XmlDocument. 
 *	
 *	Class for modelling Xlf file should not permint to:
 *	1) Get nor set of based XmlDocument. 
 */

namespace XLZ_Library
{
    public class Xlf
    {

		/* Fields */

		public XmlDocument xlfDocument;

		public XmlNode xmlMeta;
		public XmlNode xmlHead;
		public XmlNode xmlBody;

		public Meta meta;
		public Head head;
		public Body body;

		/* Properties */

		public Meta GetMeta
        {
            get
            {
				return meta;
            }
        }

		
		public Head GetHead
        {
            get
            {
				return head;
            }
        }


		public Body GetBody
        {
            get
            {
				return body;
            }
        }

		/* Methods */
		/* TODO: Add a validation method. */
		


		/* Constructors */

		public Xlf()
        {

			meta = null;
			head = null;
			body = null;

        }

		public Xlf(XmlDocument inputFile)
        {

			xlfDocument = inputFile;

			xmlMeta = xlfDocument.SelectSingleNode("//xliff");
			xmlHead = xlfDocument.SelectSingleNode("//header");
			xmlBody = xlfDocument.SelectSingleNode("//body");

			body = new Body(xmlBody);

		}

		public Xlf(string inputFile)
		{

			xlfDocument = new XmlDocument();
			xlfDocument.Load(inputFile);

			xmlMeta = xlfDocument.SelectSingleNode("//xliff");
			xmlHead = xlfDocument.SelectSingleNode("//header");
			xmlBody = xlfDocument.SelectSingleNode("//body");

			body = new Body(xmlBody);

		}

	}
}
