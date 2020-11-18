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
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Ept_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlEptNode = null;
            Ept eptElement = new Ept(xmlEptNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, eptElement.GetXmlNode);
            Assert.AreEqual(-1, eptElement.EptId);
            Assert.AreEqual(String.Empty, eptElement.EptContent);

        }

        /* Test for creating a new object from a null xmlNode got from an empty XML file.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
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
            Assert.AreEqual(null, eptElement.GetXmlNode);
            Assert.AreEqual(-1, eptElement.EptId);
            Assert.AreEqual(String.Empty, eptElement.EptContent);

        }

        /* Test for creating a new object without passing argument.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Ept_Tests_EmptyConstructor()
        {

            /* Initialization. */
            Ept eptElement = new Ept();

            /* Set of Assertions. */
            Assert.AreEqual(null, eptElement.GetXmlNode);
            Assert.AreEqual(-1, eptElement.EptId);
            Assert.AreEqual(String.Empty, eptElement.EptContent);

        }

        /* Test for creating a new object from a bpt XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the Bpt element was created and XmlNode got as a field from this object. As well id of Ept should be string value of ept XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1, "&lt;/cf&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 42, 3, "&lt;/cf&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf", 0, 1, "&lt;/cf&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf", 257, 1, "&lt;/cf&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf", 0, 1, "&lt;/cf&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf", 257, 1, "&lt;/cf&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 0, 2, "&lt;/strong&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 104, 1, "&lt;/h3&gt;")]
        public void DataTest_Ept_Tests_Properties(string inputFile, int eptPosition, int expectedIndex, string expectedContent)
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


        /* Test for returning value of GetAttributesCount() method.
         * 
         * Expected outcome: In every case the method should return number of XmlAttributes contained in EptElement. In case where EptElement is null, it should of course return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\EMPTY_1\content.xlf", 0, -1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, 6)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 0, 0)]
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


        /* Test for returning value of IsAttributeContained() method.
         * 
         * Expected outcome: In case of all ept nodes well formed (containing id attribute) method should return 1 if attribute is contained and 0 if it isn't. In case of non-well formed etp node it should return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "id", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "rid", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "ctype", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "ts", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "crc", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "defaultattribute", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "otherattribute", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 0, "id", -1)]
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


        /* Test for returning value of GetXmlAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "id")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "rid")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "ctype")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "ts")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "crc")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "defaultattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "otherattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 0, "id")]
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


        /* Test for returning value of GetXmlAttributeValue() method.
         * 
         * Expected outcome: In case when the attribute is contained in Ept element, it should have the same string value as ept XmlNode's attribute of the same name. In case if it is not contained, outcome value should be the empty string.
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "id", "1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "rid", "3")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "ctype", "bold")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "ts", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "crc", "1336")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "defaultattribute", "defaultvalue")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "otherattribute", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 0, "id", "")]
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

        /* Test for returning value of GetRidAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "3")]
        public void DataTest_Ept_Tests_Methods_GetRidAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            XmlAttribute auxiliaryAttribute = xmlEptNode.Attributes["rid"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, eptElement.GetRidAttribute());

        }

        /* Test for returning value of GetCtypeAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "bold")]
        public void DataTest_Ept_Tests_Methods_GetCtypeAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            XmlAttribute auxiliaryAttribute = xmlEptNode.Attributes["ctype"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, eptElement.GetCtypeAttribute());

        }

        /* Test for returning value of GetTsAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "")]
        public void DataTest_Ept_Tests_Methods_GetTsAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            XmlAttribute auxiliaryAttribute = xmlEptNode.Attributes["ts"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, eptElement.GetTsAttribute());

        }

        /* Test for returning value of GetCrcAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in bpt XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_4_EPT_ATTRIBUTES\content.xlf", 1, "1336")]
        public void DataTest_Ept_Tests_Methods_GetCrcAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlEptNode = xlfDocument.SelectNodes("//ept").Item(eptPosition);
            Ept eptElement = new Ept(xmlEptNode);

            XmlAttribute auxiliaryAttribute = xmlEptNode.Attributes["crc"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, eptElement.GetCrcAttribute());

        }

    }
}
