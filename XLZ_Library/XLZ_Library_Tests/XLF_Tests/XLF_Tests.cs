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


        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 169)]
        public void DataTest_Xlf_ListCount(string inputFile, int listCount)
        {

            /* Initialization. */
            Xlf testXlf = new Xlf(inputFile);

            /* Set of Assertions. */
            Assert.AreEqual(listCount, testXlf.GetBody.GetLengthOfTransUnitList);

        }

        /* Some Id might have not int but string for example id="document.xml.3" */
        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 0)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 1)]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 90)]
        public void DataTest_Xlf_NodeId(string inputFile, int nodeId)
        {

            /* Initialization. */
            Xlf testXlf = new Xlf(inputFile);

            /* Set of Assertions. */
            TransUnit testTransUnit = testXlf.GetBody.GetTransUnit(nodeId);

            Assert.IsNotNull(testTransUnit);
            Assert.AreEqual(nodeId + 1, testTransUnit.GetId);
        }


        [TestMethod]
        public void TestMethod1()
        {

            /* Initialization. */

            XmlDocument xlfDocument = new XmlDocument();
            xlfDocument.Load(xliffPath);
            //Xlf xlf = new Xlf(xlfDocument);

            TransUnit dd;

            /* Set of Assertions. */

            //Assert.AreEqual(169, xlf.xmlTransUnitList.Count);
           // Assert.IsTrue(xlf.xmlTransUnitList.Item(0).ParentNode == xlf.body);
            //Assert.IsTrue(xlf.xmlTransUnitList.Item(0).ParentNode == xlf.head);



        }
    }
}
