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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XLZ_Library.XLF;
using XLZ_Library.XLF.TransUnit;
using XLZ_Library;
using XLZ_Library.XLF.TransUnit.Languages.Elements;
using XLZ_Library.XLF.TransUnit.Languages;

namespace XLZ_Library_Tests.XLF_Tests.TransUnit_Tests
{
    [TestClass]
    public class Source_Tests
    {

        /* Set of test Xlf files. */
        public string firstDOCX = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf";
        public string firstIDML = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf";
        public string firstPDF = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf";
        public string firstXML = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf";
        public string secondXML = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_2\content.xlf";
        public string empty = @"C:\Users\Aleksander.Parol\Desktop\XLZ Example\EMPTY_1\content.xlf";

        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */

        [TestMethod]
        public void Source_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlSourceNode = null;
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, sourceElement.GetXmlNode);
            Assert.AreEqual(0, sourceElement.GetBptList.Count);
            Assert.AreEqual(0, sourceElement.GetEptList.Count);
            Assert.AreEqual(0, sourceElement.GetItList.Count);
            Assert.AreEqual(0, sourceElement.GetPhList.Count);
            Assert.AreEqual(String.Empty, sourceElement.SourceContent);

        }

        /* Test for creating a new object from a bpt XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the Bpt element was created and XmlNode got as a field from this object. As well id of Bpt should be string value of bpt XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 0, "&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, 1, "&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        public void DataTest_Bpt_Tests_Properties(string inputFile, int sourcePosition, int expectedIndex, string expectedContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */

            Assert.AreEqual(expectedIndex, sourceElement.CountBpt);

        }

    }
}
