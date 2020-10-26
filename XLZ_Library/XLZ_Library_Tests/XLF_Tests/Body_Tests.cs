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
    public class Body_Tests
    {
        /* Set of test Xlf files. */
        public string xliffPath = @"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf";


        /* Tests */

        [TestMethod]
        public void DataTest_Body_GetLengthOfTransUnitList_Tests_1()
        {

            /* Initialization. */
            Body testBody = new Body();

            /* Set of Assertions. */
            Assert.AreEqual(0, testBody.GetLengthOfTransUnitList);

        }

        [TestMethod]
        public void DataTest_Body_GetLengthOfTransUnitList_Tests_2()
        {

            /* Initialization. */
            Xlf testXlf = new Xlf();

            /* Set of Assertions. */
            Assert.AreEqual(0, testXlf.GetBody.GetLengthOfTransUnitList);

        }

        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 169)]
        public void DataTest_Body_GetLengthOfTransUnitList_Tests_3(string inputFile, int listCount)
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
        public void DataTest_Body_GetTransUnit_Tests_1(string inputFile, int nodeId)
        {

            /* Initialization. */
            Xlf testXlf = new Xlf(inputFile);
            TransUnit testTransUnit = testXlf.GetBody.GetTransUnit(nodeId);

            /* Set of Assertions. */

            Assert.IsNotNull(testTransUnit);
            Assert.AreEqual((nodeId + 1).ToString(), testTransUnit.GetId);
        }

        [DataTestMethod]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 0, "no")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 1, "no")]
        [DataRow(@"C:\Users\Aleksander.Parol\Desktop\GLT_Engineering\Documentation\Script\C# Script Block all except yellow highlight\Blocked by the existing script\content.xlf", 8, "yes")]
        public void DataTest_Body_GetTransUnit_Tests_2(string inputFile, int nodePosition, string nodeTranslatable)
        {

            /* Initialization. */
            Xlf testXlf = new Xlf(inputFile);
            TransUnit testTransUnit = testXlf.GetBody.GetTransUnit(nodePosition);

            /* Set of Assertions. */

            Assert.IsNotNull(testTransUnit);
            Assert.AreEqual(nodeTranslatable, testTransUnit.GetTranslate);
        }

    }
}
