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

namespace XLZ_Library_Tests.XLF_Tests.TransUnit_Tests.Elements_Tests
{
    [TestClass]
    public class Bpt_Tests
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
        public void Bpt_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlBptNode = null;
            Bpt bptElement = new Bpt(xmlBptNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, bptElement.GetXmlNode);
            Assert.AreEqual(-1, bptElement.BptId);
            Assert.AreEqual(String.Empty, bptElement.BptContent);

        }

        /* Test for creating a new object from a null xmlNode got from an empty XML file.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Bpt_Tests_EmptyFile()
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(empty);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(0);
            Bpt bptElement = new Bpt(xmlBptNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, bptElement.GetXmlNode);
            Assert.AreEqual(-1, bptElement.BptId);
            Assert.AreEqual(String.Empty, bptElement.BptContent);

        }

        /* Test for creating a new object without passing argument.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Bpt_Tests_EmptyConstructor()
        {

            /* Initialization. */
            Bpt bptElement = new Bpt();

            /* Set of Assertions. */
            Assert.AreEqual(null, bptElement.GetXmlNode);
            Assert.AreEqual(-1, bptElement.BptId);
            Assert.AreEqual(String.Empty, bptElement.BptContent);

        }

        /* Test for creating a new object from a bpt XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the Bpt element was created and XmlNode got as a field from this object. As well id of Bpt should be string value of bpt XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1, "&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 45, 3, "&lt;cf style=\"Hyperlink\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf", 0, 1, "&lt;cf cstyle=\"CharacterStyle/$ID/[No character style]\" color=\"Color/C=0 M=0 Y=0 K=0\" style=\"Semibold\" size=\"12\" leading=\"unit:12\" font=\"string:Diodrum\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf", 259, 1, "&lt;cf cstyle=\"CharacterStyle/$ID/[No character style]\" color=\"Color/C=0 M=0 Y=0 K=100\" style=\"Medium\" size=\"10\" leading=\"unit:10\" font=\"string:Diodrum\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf", 0, 1, "&lt;cf cstyle=\"CharacterStyle/$ID/[No character style]\" color=\"Color/C=0 M=0 Y=0 K=0\" style=\"Semibold\" size=\"12\" leading=\"unit:12\" font=\"string:Diodrum\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf", 259, 1, "&lt;cf cstyle=\"CharacterStyle/$ID/[No character style]\" color=\"Color/C=0 M=0 Y=0 K=100\" style=\"Medium\" size=\"10\" leading=\"unit:10\" font=\"string:Diodrum\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 0, 1, "&lt;p&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 104, 2, "&lt;a href=\"/content/dam/dpagco/corteva/na/ca/fr/files/sustainability/Corteva-2030-Sustainability-Goals_The-Land.pdf\" target=\"_blank\"&gt;")]
        public void DataTest_Bpt_Tests_Properties(string inputFile, int bptPosition, int expectedIndex, string expectedContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(bptPosition);
            Bpt bptElement = new Bpt(xmlBptNode);

            /* Set of Assertions. */
            Assert.AreEqual(xmlBptNode, bptElement.xmlBptNode);
            Assert.AreEqual(expectedIndex, bptElement.BptId);
            Assert.AreEqual(expectedContent, bptElement.BptContent);

        }


        /* Test for returning value of GetAttributesCountMethod().
         * 
         * Expected outcome: In every case the method should return number of XmlAttributes contained in BptElement. In case where BptElement is null, it should of course return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\EMPTY_1\content.xlf", 0, -1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, 6)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 1, 0)]
        public void DataTest_Bpt_Tests_Methods_GetAttributesCount(string inputFile, int bptPosition, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(bptPosition);
            Bpt bptElement = new Bpt(xmlBptNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, bptElement.GetAttributesCount());

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "id", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "rid", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "ctype", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "ts", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "crc", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "defaultattribute", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "otherattribute", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 1, "id", -1)]
        public void DataTest_Bpt_Tests_Methods_IsAttributeContained(string inputFile, int bptPosition, string expectedAttributeName, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(bptPosition);
            Bpt bptElement = new Bpt(xmlBptNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, bptElement.IsAttributeContained(expectedAttributeName));

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id")]
        public void DataTest_Bpt_Tests_Methods_GetXmlAttribute(string inputFile, int bptPosition, string expectedAttributeName)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(bptPosition);
            Bpt bptElement = new Bpt(xmlBptNode);

            XmlAttribute auxiliaryAttribute = xmlBptNode.Attributes[expectedAttributeName];

            /* Set of Assertions. */
            Assert.AreEqual(auxiliaryAttribute, bptElement.GetXmlAttribute(expectedAttributeName));

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id", "1")]
        public void DataTest_Bpt_Tests_Methods_GetXmlAttributeValue(string inputFile, int bptPosition, string expectedAttributeName, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(bptPosition);
            Bpt bptElement = new Bpt(xmlBptNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedValue, bptElement.GetXmlAttributeValue(expectedAttributeName));

        }

    }
}
