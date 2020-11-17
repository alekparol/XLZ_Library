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
            Assert.AreEqual(String.Empty, bptElement.BptId);
            Assert.AreEqual(String.Empty, bptElement.BptContent);

        }

        /* Test for creating a new object from a null xmlNode.
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
            Assert.AreEqual(String.Empty, bptElement.BptId);
            Assert.AreEqual(String.Empty, bptElement.BptContent);

        }

        /* Test for creating a new object from a null xmlNode.
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
            Assert.AreEqual(String.Empty, bptElement.BptId);
            Assert.AreEqual(String.Empty, bptElement.BptContent);

        }

        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "1", "&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 45, "3", "&lt;cf style=\"Hyperlink\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        public void DataTest_Bpt_Tests_Properties(string inputFile, int bptPosition, string expectedIndex, string expectedContent)
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


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1)]
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
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id",  1)]
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
