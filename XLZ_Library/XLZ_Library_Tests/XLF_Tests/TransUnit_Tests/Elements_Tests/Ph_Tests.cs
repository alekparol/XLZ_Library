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
using XLZ_Library.XLF.TransUnph.Languages.Elements;

namespace XLZ_Library_Tests.XLF_Tests.TransUnit_Tests.Elements_Tests
{
    [TestClass]
    public class Ph_Tests
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
         * Expected outcome:
         */
        [TestMethod]
        public void Ph_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlPhNode = null;
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(String.Empty, phElement.PhId);
            Assert.AreEqual(String.Empty, phElement.PhContent);

        }

        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [TestMethod]
        public void Ph_Tests_EmptyFile()
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(empty);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(0);
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(String.Empty, phElement.PhId);
            Assert.AreEqual(String.Empty, phElement.PhContent);

        }

        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "1", "&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 45, "3", "&lt;cf style=\"Hyperlink\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        public void DataTest_Ph_Tests_Properties(string inputFile, int phPosition, string expectedIndex, string expectedContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(xmlPhNode, phElement.xmlPhNode);
            Assert.AreEqual(expectedIndex, phElement.PhId);
            Assert.AreEqual(expectedContent, phElement.PhContent);

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1)]
        public void DataTest_Ph_Tests_Methods_GetAttributesCount(string inputFile, int phPosition, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, phElement.GetAttributesCount());

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id", 1)]
        public void DataTest_Ph_Tests_Methods_IsAttributeContained(string inputFile, int phPosition, string expectedAttributeName, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, phElement.IsAttributeContained(expectedAttributeName));

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id")]
        public void DataTest_Ph_Tests_Methods_GetXmlAttribute(string inputFile, int phPosition, string expectedAttributeName)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            XmlAttribute auxiliaryAttribute = xmlPhNode.Attributes[expectedAttributeName];

            /* Set of Assertions. */
            Assert.AreEqual(auxiliaryAttribute, phElement.GetXmlAttribute(expectedAttributeName));

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id", "1")]
        public void DataTest_Ph_Tests_Methods_GetXmlAttributeValue(string inputFile, int phPosition, string expectedAttributeName, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedValue, phElement.GetXmlAttributeValue(expectedAttributeName));

        }

    }
}
