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
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Ph_Test_Null()
        {

            /* Initialization. */
            XmlNode xmlPhNode = null;
            Ph phElement = new Ph(xmlPhNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, phElement.GetXmlNode);
            Assert.AreEqual(-1, phElement.PhId);
            Assert.AreEqual(String.Empty, phElement.PhContent);

        }

        /* Test for creating a new object from a null xmlNode got from an empty XML file.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
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
            Assert.AreEqual(null, phElement.GetXmlNode);
            Assert.AreEqual(-1, phElement.PhId);
            Assert.AreEqual(String.Empty, phElement.PhContent);

        }

        /* Test for creating a new object without passing argument.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Ph_Tests_EmptyConstructor()
        {

            /* Initialization. */
            Ph phElement = new Ph();

            /* Set of Assertions. */
            Assert.AreEqual(null, phElement.GetXmlNode);
            Assert.AreEqual(-1, phElement.PhId);
            Assert.AreEqual(String.Empty, phElement.PhContent);

        }

        /* Test for creating a new object from a ph XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the Ph element was created and XmlNode got as a field from this object. As well id of Ph should be string value of ph XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1, "&lt;bookmarkStart number=\"0\" w:name=\"_MailOriginal\"/&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 4, 2, "&lt;br/&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf", 0, 2, "&lt;afr story=\"ub35\" id=\"ub32\"/&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\IDML_1\content.xlf", 6, 2, "&lt;afr story=\"ub46\" id=\"ub43\"/&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf", 0, 2, "&lt;afr story=\"ub35\" id=\"ub32\"/&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\PDF_1\content.xlf", 6, 2, "&lt;afr story=\"ub46\" id=\"ub43\"/&gt;")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 0, 3, "{&quot;itemHeader&quot;:")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_1\content.xlf", 130, 2, "&quot;/content/experience-fragments/corteva/ca/primaryCard/acre-value&quot;}")]
        public void DataTest_Ph_Tests_Properties(string inputFile, int phPosition, int expectedIndex, string expectedContent)
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


        /* Test for returning value of GetAttributesCount() method.
         * 
         * Expected outcome: In every case the method should return number of XmlAttributes contained in PhElement. In case where PhElement is null, it should of course return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\EMPTY_1\content.xlf", 0, -1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, 8)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 2, 0)]
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


        /* Test for returning value of IsAttributeContained() method.
         * 
         * Expected outcome: In case of all ph nodes well formed (containing id attribute) method should retur 1 if attribute is contained and 0 if it isn't. In case of non-well formed btp node it should return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "id", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "ts", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "crc", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "ctype", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "assoc", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "tilt:type", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "equiv-text", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "defaultattribute", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "otherattribute", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 2, "id", -1)]
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


        /* Test for returning value of GetXmlAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ph XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "id")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "ts")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "crc")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "ctype")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "assoc")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "tilt:type")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "equiv-text")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "defaultattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "otherattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 2, "id")]
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


        /* Test for returning value of GetXmlAttributeValue() method.
         * 
         * Expected outcome: In case when the attribute is contained in Ph element, it should have the same string value as ph XmlNode's attribute of the same name. In case if it is not contained, outcome value should be the empty string.
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "id", "3")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "ts", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "crc", "1336")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "ctype", "bold")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "assoc", "{&quot;itemHeader&quot;:")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "tilt:type", "do-not-translate")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "equiv-text", "{&quot;itemHeader&quot;:")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "defaultattribute", "defaultvalue")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 0, "otherattribute", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_6_PH_ATTRIBUTES\content.xlf", 2, "id", "")]
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

        /* Test for returning value of GetCtypeAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ph XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "bold")]
        public void DataTest_Ph_Tests_Methods_GetCtypeAttribute(string inputFile, int phPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            XmlAttribute auxiliaryAttribute = xmlPhNode.Attributes["ctype"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, phElement.GetCtypeAttribute());

        }

        /* Test for returning value of GetTsAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ph XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "")]
        public void DataTest_Ph_Tests_Methods_GetTsAttribute(string inputFile, int phPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            XmlAttribute auxiliaryAttribute = xmlPhNode.Attributes["ts"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, phElement.GetTsAttribute());

        }

        /* Test for returning value of GetCrcAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ph XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "1336")]
        public void DataTest_Ph_Tests_Methods_GetCrcAttribute(string inputFile, int phPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            XmlAttribute auxiliaryAttribute = xmlPhNode.Attributes["crc"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, phElement.GetCrcAttribute());

        }

        /* Test for returning value of GetRidAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ph XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "{&quot;itemHeader&quot;:")]
        public void DataTest_Ph_Tests_Methods_GetAssocAttribute(string inputFile, int phPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            XmlAttribute auxiliaryAttribute = xmlPhNode.Attributes["rid"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, phElement.GetAssocAttribute());

        }

        /* Test for returning value of GetRidAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in ph XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "{&quot;itemHeader&quot;:")]
        public void DataTest_Ph_Tests_Methods_GetEquivTextAttribute(string inputFile, int phPosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlPhNode = xlfDocument.SelectNodes("//ph").Item(phPosition);
            Ph phElement = new Ph(xmlPhNode);

            XmlAttribute auxiliaryAttribute = xmlPhNode.Attributes["rid"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, phElement.GetEquivTextAttribute());

        }

    }
}
