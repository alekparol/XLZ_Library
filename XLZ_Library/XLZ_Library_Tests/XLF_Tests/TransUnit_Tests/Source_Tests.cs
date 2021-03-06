﻿using System;
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

        /* Test for creating a new object from a null xmlNode got from an empty XML file.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Source_Tests_EmptyFile()
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(empty);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(0);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(null, sourceElement.GetXmlNode);
            Assert.AreEqual(0, sourceElement.GetBptList.Count);
            Assert.AreEqual(0, sourceElement.GetEptList.Count);
            Assert.AreEqual(0, sourceElement.GetItList.Count);
            Assert.AreEqual(0, sourceElement.GetPhList.Count);
            Assert.AreEqual(String.Empty, sourceElement.SourceContent);

        }

        /* Test for creating a new object without passing argument.
         * 
         * Expected outcome: XmlNode is null, both string fields are initialized with empty string values.
         */
        [TestMethod]
        public void Source_Tests_EmptyConstructor()
        {

            /* Initialization. */
            Source sourceElement = new Source();

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
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 0, 0, 0, 2, "<ph id=\"1\">&lt;bookmarkStart number=\"0\" w:name=\"_MailOriginal\"/&gt;</ph><ph id=\"2\">&lt;w:drawing/&gt;</ph>")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, 1, 1, 0, 0, "<bpt id=\"1\">&lt;cf bold=\"on\" complexscriptsbold=\"on\" italic=\"on\" complexscriptsitalic=\"on\" size=\"9\" complexscriptssize=\"9\"&gt;</bpt>* This communication is targeted to all STIP-eligible employees.<ept id=\"1\">&lt;/cf&gt;</ept>")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 2, 0, 0, 0, 1, "<ph id=\"1\">&lt;bookmarkStart number=\"1\" w:name=\"_GoBack\"/&gt;</ph>")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 6, 1, 1, 0, 0, "<ept id=\"1\">&lt;/cf&gt;</ept><bpt id=\"2\">&lt;cf fontcolor=\"000000\"&gt;</bpt>Your daily efforts drive DuPont’s results.")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 5, 5, 0, 1, "<ept id=\"1\">&lt;/cf&gt;</ept><ph id=\"2\">&lt;br/&gt;</ph><bpt id=\"3\">&lt;hyperlink Id=\"1\" tkn=\"276\"&gt;</bpt><bpt id=\"4\">&lt;cf style=\"Hyperlink\"&gt;</bpt>Click <ept id=\"4\">&lt;/cf&gt;</ept><ept id=\"3\">&lt;/hyperlink&gt;</ept><bpt id=\"5\">&lt;hyperlink Id=\"2\" tkn=\"287\"&gt;</bpt><bpt id=\"6\">&lt;cf style=\"Hyperlink\"&gt;</bpt>here for an overview of the plan.<ept id=\"6\">&lt;/cf&gt;</ept><ept id=\"5\">&lt;/hyperlink&gt;</ept><bpt id=\"7\">&lt;cf fontcolor=\"000000\"&gt;</bpt>")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 34, 3, 3, 0, 0, "<bpt id=\"1\">&lt;cf bold=\"on\" complexscriptsbold=\"on\" fontcolor=\"C00000\" size=\"9\" complexscriptssize=\"9\"&gt;</bpt>HR Direct Service Center or visit <ept id=\"1\">&lt;/cf&gt;</ept><bpt id=\"2\">&lt;hyperlink Id=\"1\" tkn=\"507\"&gt;</bpt><bpt id=\"3\">&lt;cf style=\"Hyperlink\" size=\"9\" complexscriptssize=\"9\"&gt;</bpt>https://dupont.sharepoint.com/sites/HR/en<ept id=\"3\">&lt;/cf&gt;</ept><ept id=\"2\">&lt;/hyperlink&gt;</ept>")]
        public void DataTest_Source_Tests_Properties(string inputFile, int sourcePosition, int expectedBptCount, int expectedEptCount, int expectedItCount, int expectedPhCount, string expectedContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(xmlSourceNode, sourceElement.GetXmlNode);
            Assert.AreEqual(expectedBptCount, sourceElement.GetBptList.Count);
            Assert.AreEqual(expectedEptCount, sourceElement.GetEptList.Count);
            Assert.AreEqual(expectedItCount, sourceElement.GetItList.Count);
            Assert.AreEqual(expectedPhCount, sourceElement.GetPhList.Count);
            Assert.AreEqual(expectedContent, sourceElement.SourceContent);

        }


        /* Test for creating a new object from a bpt XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the Bpt element was created and XmlNode got as a field from this object. As well id of Bpt should be string value of bpt XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 0, 0, 0, 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, 0, 0, 1, 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 2, 0, 0, 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 6, 1, 1, 0, 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 1, 1, 4, 4)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 34, 0, 0, 3, 3)]
        public void DataTest_Source_Tests_BptEpt_Properties(string inputFile, int sourcePosition, int expectedBptNotClosed, int expectedEptNotStarted, int expectedBptClosed, int expectedEptStarted)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedBptNotClosed, sourceElement.GetBptNotClosed.Count);
            Assert.AreEqual(expectedEptNotStarted, sourceElement.GetEptNotStarted.Count);
            Assert.AreEqual(expectedBptClosed, sourceElement.GetBptClosed.Count);
            Assert.AreEqual(expectedEptStarted, sourceElement.GetEptStarted.Count);

        }

        /* Test for creating a new object from a bpt XmlNode.
         * 
         * Expected outcome: Reference to XmlNode should be the same for XmlNode from which the Bpt element was created and XmlNode got as a field from this object. As well id of Bpt should be string value of bpt XmlNode converted to Int32 and content should match InnerXml. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 1, 1, "* This communication is targeted to all STIP-eligible employees.")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 3, 1, "2020 Short-Term Incentive Plan (STIP) Overview")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 4, 1, "What you do matters.")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 6, 1, "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 6, 2, "Your daily efforts drive DuPont’s results.")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 8, 2, "That’s why our rewards philosophy")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 1, "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 3, "<bpt id=\"4\">&lt;cf style=\"Hyperlink\"&gt;</bpt>Click <ept id=\"4\">&lt;/cf&gt;</ept>")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 4, "Click ")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 4, "Click ")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 5, "<bpt id=\"6\">&lt;cf style=\"Hyperlink\"&gt;</bpt>here for an overview of the plan.<ept id=\"6\">&lt;/cf&gt;</ept>")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 6, "here for an overview of the plan.")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\DOCX_1\content.xlf", 14, 7, "")]
        public void DataTest_Source_Tests_Methods_GetBptEptContent(string inputFile, int sourcePosition, int bptEptId, string expectedBptEptContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedBptEptContent, sourceElement.GetBptEptContent(bptEptId));

        }


        /* Test for returning value of IsAttributeContained() method.
         * 
         * Expected outcome: In case of all bpt nodes well formed (containing id attribute) method should retur 1 if attribute is contained and 0 if it isn't. In case of non-well formed btp node it should return -1. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "xml:lang", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "ts", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "defaultattribute", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "id", -1)]
        public void DataTest_Source_Tests_Methods_IsAttributeContained(string inputFile, int sourcePosition, string expectedAttributeName, int expectedOutcome)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedOutcome, sourceElement.IsAttributeContained(expectedAttributeName));

        }

        /* Test for returning value of GetXmlAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in bpt XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "xml:lang")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "ts")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "defaultattribute")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "id")]
        public void DataTest_Source_Tests_Methods_GetXmlAttribute(string inputFile, int sourcePosition, string expectedAttributeName)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            XmlAttribute auxiliaryAttribute = xmlSourceNode.Attributes[expectedAttributeName];

            /* Set of Assertions. */
            Assert.AreEqual(auxiliaryAttribute, sourceElement.GetXmlAttribute(expectedAttributeName));

        }

        /* Test for returning value of GetXmlAttributeValue() method.
         * 
         * Expected outcome: In case when the attribute is contained in Bpt element, it should have the same string value as bpt XmlNode's attribute of the same name. In case if it is not contained, outcome value should be the empty string.
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "xml:lang", "en-gb")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "ts", "")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "defaultattribute", "defaultvalue")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "id", "")]
        public void DataTest_Source_Tests_Methods_GetXmlAttributeValue(string inputFile, int sourcePosition, string expectedAttributeName, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            /* Set of Assertions. */
            Assert.AreEqual(expectedValue, sourceElement.GetXmlAttributeValue(expectedAttributeName));

        }

        /* Test for returning value of GetRidAttribute() method.
        * 
        * Expected outcome: In every case (even if the attribute is not contained in bpt XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
        */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_7_Source_ATTRIBUTES\content.xlf", 0, "")]
        public void DataTest_Source_Tests_Methods_GetTsAttribute(string inputFile, int sourcePosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            XmlAttribute auxiliaryAttribute = xmlSourceNode.Attributes["ts"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, sourceElement.GetTsAttribute());

        }

        /* Test for returning value of GetCtypeAttribute() method.
         * 
         * Expected outcome: In every case (even if the attribute is not contained in bpt XmlNode), reference of the attribute found by its name, should be the same as reference of XmlNode's attribute of that name. 
         */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\XLZ Example\XML_3_BPT_ATTRIBUTES\content.xlf", 0, "en-gb")]
        public void DataTest_Source_Tests_Methods_GetXmlLangAttribute(string inputFile, int sourcePosition, string expectedValue)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlSourceNode = xlfDocument.SelectNodes("//source").Item(sourcePosition);
            Source sourceElement = new Source(xmlSourceNode);

            XmlAttribute auxiliaryAttribute = xmlSourceNode.Attributes["xml:lang"];

            /* Set of Assertions. */
            Assert.IsNotNull(auxiliaryAttribute);
            Assert.AreEqual(expectedValue, auxiliaryAttribute.Value);
            Assert.AreEqual(auxiliaryAttribute, sourceElement.GetXmlLangAttribute());

        }

    }
}
