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
        public string xliffPath = @"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf";

        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 0, "1", "&lt;AlternateContent&gt;")]
        public void DataTest_Bpt_Tests_1(string inputFile, int bptPosition, string expectedIndex, string expectedContent)
        {

            /* Initialization. */
            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(inputFile);

            XmlNode xmlBptNode = xlfDocument.SelectNodes("//bpt").Item(bptPosition);
            Bpt bptElement = new Bpt(xmlBptNode);
 
            /* Set of Assertions. */
            Assert.AreEqual(expectedIndex, bptElement.BptId);
            Assert.AreEqual(expectedContent, bptElement.BptContent);

        }

    }
}
