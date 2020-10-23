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
        public List<TransUnit> transUnitList;

		/* Properties */


		/* Methods */

		/* Constructors */

		public Body(XmlNode xmlBody)
		{
			/* Here should be added validation of XML file. */
			this.xmlBody = xmlBody;
				
			xmlTransUnitList = xmlBody.SelectNodes("//trans-unit");
			transUnitList = new List<TransUnit>();

			TransUnit auxiliaryTransUnit = new TransUnit();
			foreach (XmlNode xmlNode in xmlTransUnitList)
			{
				auxiliaryTransUnit = new TransUnit(xmlNode);
				transUnitList.Add(auxiliaryTransUnit);
			}

		}
	}
}
