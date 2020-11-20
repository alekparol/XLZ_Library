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
    public class It_Tests
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
        public void It_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlItNode = null;
            It itElement = new It(xmlItNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, itElement.GetXmlNode);
            Assert.AreEqual(-1, itElement.ItId);
            Assert.AreEqual(String.Empty, itElement.ItContent);

        }

        /* Test for creating a new object from a null xmlNode got from an empty XML file.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void It_Tests_EmptyFile()
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(empty);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(0);
            It itElement = new It(xmlItNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, itElement.GetXmlNode);
            Assert.AreEqual(-1, itElement.ItId);
            Assert.AreEqual(String.Empty, itElement.ItContent);

        }

        /* Test for creating a new object without passing argument.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void It_Tests_EmptyConstructor()
        {

            /* Initialization. */
            It itElement = new It();

            /* Set of Assertions. */
            Assert.AreEqual(null, itElement.GetXmlNode);
            Assert.AreEqual(-1, itElement.ItId);
            Assert.AreEqual(String.Empty, itElement.ItContent);

        }

        /* Test for creating a new object from a it XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the It element was created and XmlNode got as a field from this object. As well id of It should be string value of it XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 0, 2, "&lt;p&gt;", "open")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 1, 2, "&lt;/p&gt;", "close")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 2, 2, "&lt;p&gt;", "open")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 3, 2, "&lt;/p&gt;", "close")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 6, 3, "&lt;p style=\"text-align: center;\"&gt;", "open")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 7, 4, "&lt;strong&gt;", "open")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 8, 4, "&lt;/strong&gt;", "close")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 61, 4, "&lt;strong&gt;", "open")]
        public void DataTest_It_Tests_Properties(string inputFile, int itPosition, int expectedIndex, string expectedContent, string expectedPos)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(itPosition);
            It itElement = new It(xmlItNode);

            /* Set of Assertions. */
            Assert.AreEqual(xmlItNode, itElement.xmlItNode);
            Assert.AreEqual(expectedIndex, itElement.ItId);
            Assert.AreEqual(expectedPos, itElement.ItPos);
            Assert.AreEqual(expectedContent, itElement.ItContent);

        }


        /* Test for returning value of GetAttributesCount() method.
         * 
         * Expected outcome: In every case the method should return number of XmlAttributes contained in ItElement. In case where ItElement is null, it should of course return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 0, 3)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\EMPTY_1\content.xlf", 0, -1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, 8)]
        public void DataTest_It_Tests_Methods_GetAttributesCount(string inputFile, int eptPosition, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, itElement.GetAttributesCount());

        }


        /* Test for returning value of IsAttributeContained() method.
         * 
         * Expected outcome: In case of all it nodes well formed (containing id attribute) method should return 1 if attribute is contained and 0 if it isn't. In case of non-well formed it node it should return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "id", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "rid", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "ctype", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "ts", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "crc", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "defaultattribute", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "otherattribute", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 0, "id", -1)]
        public void DataTest_It_Tests_Methods_IsAttributeContained(string inputFile, int eptPosition, string expectedAttributeName, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, itElement.IsAttributeContained(expectedAttributeName));

        }


        /* Test for returning value of GetXmlAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "id")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "rid")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "ctype")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "ts")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "crc")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "defaultattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "otherattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 0, "id")]
        public void DataTest_It_Tests_Methods_GetXmlAttribute(string inputFile, int eptPosition, string expectedAttributeName)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            XmlAttribute auxiliaryAttribute = xmlItNode.Attributes[expectedAttributeName];

            /* Set of Assertions. */
            Assert.AreEqual(auxiliaryAttribute, itElement.GetXmlAttribute(expectedAttributeName));

        }


        /* Test for returning value of GetXmlAttributeValue() method.
         * 
         * Expected outcome: In case when the attribute is contained in It element, it should have the same string value as it XmlNode's attribute of the same name. In case if it is not contained, outcome value should be the empty string.
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "id", "2")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "rid", "1")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "ctype", "bold")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "ts", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "crc", "1336")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "defaultattribute", "defaultvalue")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "otherattribute", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 0, "id", "")]
        public void DataTest_It_Tests_Methods_GetXmlAttributeValue(string inputFile, int eptPosition, string expectedAttributeName, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedValue, itElement.GetXmlAttributeValue(expectedAttributeName));

        }

        /* Test for returning value of GetRidAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in it XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "1")]
        public void DataTest_It_Tests_Methods_GetRidAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            XmlAttribute auxiliaryAttribute = xmlItNode.Attributes["rid"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, itElement.GetRidAttribute());

        }

        /* Test for returning value of GetCtypeAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "bold")]
        public void DataTest_It_Tests_Methods_GetCtypeAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            XmlAttribute auxiliaryAttribute = xmlItNode.Attributes["ctype"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, itElement.GetCtypeAttribute());

        }

        /* Test for returning value of GetTsAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ept XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "")]
        public void DataTest_It_Tests_Methods_GetTsAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            XmlAttribute auxiliaryAttribute = xmlItNode.Attributes["ts"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, itElement.GetTsAttribute());

        }

        /* Test for returning value of GetCrcAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in bpt XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_5_IT_ATTRIBUTES\content.xlf", 1, "1336")]
        public void DataTest_It_Tests_Methods_GetCrcAttribute(string inputFile, int eptPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlItNode = xlfDocument.SelectNodes("//it").Item(eptPosition);
            It itElement = new It(xmlItNode);

            XmlAttribute auxiliaryAttribute = xmlItNode.Attributes["crc"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, itElement.GetCrcAttribute());

        }

    }
}
