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
    public class Ept_Tests
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
        public void Ept_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlEptNode = null;
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(String.Empty, eptElement.EptId);
            Assert.AreEqual(String.Empty, eptElement.EptContent);

        }

        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [TestMethod]
        public void Ept_Tests_EmptyFile()
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(empty);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(0);
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(String.Empty, eptElement.EptId);
            Assert.AreEqual(String.Empty, eptElement.EptContent);

        }

        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "1", "&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 45, "3", "&lt;cf style=\"Hyperlink\" size=\"9\" complexscriptssize=\"9\"&gt;")]
        public void DataTest_Ept_Tests_Properties(string inputFile, int eptPosition, string expectedIndex, string expectedContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(xmlEptNode, eptElement.xmlEptNode);
            Assert.AreEqual(expectedIndex, eptElement.EptId);
            Assert.AreEqual(expectedContent, eptElement.EptContent);

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1)]
        public void DataTest_Ept_Tests_Methods_GetAttributesCount(string inputFile, int eptPosition, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, eptElement.GetAttributesCount());

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id", 1)]
        public void DataTest_Ept_Tests_Methods_IsAttributeContained(string inputFile, int eptPosition, string expectedAttributeName, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, eptElement.IsAttributeContained(expectedAttributeName));

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id")]
        public void DataTest_Ept_Tests_Methods_GetXmlAttribute(string inputFile, int eptPosition, string expectedAttributeName)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            XmlAttribute auxiliaryAttribute = xmlEptNode.Attributes[expectedAttributeName];

            /* Set of Assertions. */
            Assert.AreEqual(auxiliaryAttribute, eptElement.GetXmlAttribute(expectedAttributeName));

        }


        /* Test for creating a new object from a null xmlNode.
         * 
         * Expected outcome:
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, "id", "1")]
        public void DataTest_Ept_Tests_Methods_GetXmlAttributeValue(string inputFile, int eptPosition, string expectedAttributeName, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedValue, eptElement.GetXmlAttributeValue(expectedAttributeName));

        }

    }
}
