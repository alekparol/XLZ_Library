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

namespace XLZ_Library_Tests.XLF_Tests
{
    [TestClass]
    public class Xlf_Tests
    {

        public string xliffPath = @"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf";

        [TestMethod]
        public void TestMethod1()
        {

            /* Initialization. */

            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(xliffPath);
            Xlf xlf = new Xlf(xlfDocument);

            TransUnit dd;

            /* Set of Assertions. */

            Assert.AreEqual(169, xlf.xmlTransUnitList.Count);
            Assert.IsTrue(xlf.xmlTransUnitList.Item(0).ParentNode == xlf.body);
            Assert.IsTrue(xlf.xmlTransUnitList.Item(0).ParentNode == xlf.head);



        }
    }
}
