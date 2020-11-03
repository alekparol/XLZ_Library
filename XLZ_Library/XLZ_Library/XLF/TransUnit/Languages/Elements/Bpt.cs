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
using XLZ_Library.XLF.TransUnit;
using XLZ_Library.XLF;

/* This class intended use is to model structure of the <bpt></bpt> elements of the source/target node. bpt as we can read from Xliff documentation stands for "begin paired tag" and is coupled with ept. 
 *
 * Documentation Description:
 * The <bpt> element is used to delimit the beginning of a paired sequence of native codes. Each <bpt> has a corresponding <ept> element within the segment. 
 * 
 * Notes:
 * From the Xliff version 1.1. above <bpt><ept> nodes are paired by "rid" attribute, which is used in version 1.0. but not as a standard.
 *
 * What is the typical for bpt node structure in Xlf file? 
 * 1.) Required Attributes:
 *     1.1.) id attribute which is occupied until there will be <ept> node with the same id. Then the id will be released.
 * 2.) Optional Attributes:
 *     2.1.) rid attribute;
 *     2.2.) ctype attribute;
 *     2.3.) ts attribute;
 *     2.4.) crc attribute;
 * 3.) Content which should be native code. 
 * 
 * Definition:
 * 
 * Machine code: This is the most well-defined one. It is code that uses the byte-code instructions which your processor (the physical piece of metal that does the actual work) understands and executes directly. 
 * All other code must be translated or transformed into machine code before your machine can execute it.
 * 
 * Native code: This term is sometimes used in places where machine code (see above) is meant. However, it is also sometimes used to mean unmanaged code (see below).
 * 
 * Unmanaged code and managed code: Unmanaged code refers to code written in a programming language such as C or C++, which is compiled directly into machine code.
 * It contrasts with managed code, which is written in C#, VB.NET, Java, or similar, and executed in a virtual environment (such as .NET or the JavaVM) which kind of “simulates” a processor in software.
 * The main difference is that managed code “manages” the resources (mostly the memory allocation) for you by employing garbage collection and by keeping references to objects opaque. 
 * Unmanaged code is the kind of code that requires you to manually allocate and de-allocate memory, sometimes causing memory leaks (when you forget to de-allocate) and sometimes segmentation faults (when you de-allocate too soon).
 * Unmanaged also usually implies there are no run-time checks for common errors such as null-pointer dereferencing or array bounds overflow.
 * 
 * This definition is taken from https://stackoverflow.com/questions/3434202/what-is-the-difference-between-native-code-machine-code-and-assembly-code. If you find more suitable, please change it. 
 * 
 * Class for modelling should contain:
 * 1.) XmlNode xmlBptNode field to store <bpt> XmlNode with its XmlAttributes;
 * 2.) XmlAttributeCollection xmlBptAttributeCollection field to store the collection of attributes of the xmlBprNode;
 * 3.) String bpId field to store string value of the id; [id value should be always integer in the case of bpt tags]
 * 4.) String bptcontent field to store native code contained between <bpt><\bpt> tags.
 * 
 * Class for modelling should permit to:
 * 1.) Use Properties:
 *      1.1.) GetXmlNode to return the xmlTransUnitNode;
 *      1.2.) GetId to return string value of the "id" attribute;
 *      1.3.) GetTranslate to return string value of the "translatable" attribute.
 * 2.) Use Methods:
 * 3.) Use Constructors:
 *      1.1.) Empty constructor to set all fields to null;
 *      1.2.) Constructor to which XmlNode is passed with trans-unit node. 
 *		
 * Class for modeling should not permit to:
 * 
 *
 */
namespace XLZ_Library.XLF.TransUnit.Languages.Elements
{
    public class Bpt
    {

        /* Fields */
        public XmlNode xmlBptNode;

        public XmlAttributeCollection xmlBptAttributeCollection;

        public string bptId;
        public string bptContent;

        /* Properties */

        public string BptId
        {
            get
            {
                return bptId;
            }
        }

        public string BptContent
        {
            get
            {
                return bptContent;
            }
        }


        /* Methods */

        public int GetAttributesCount()
        {
            if (xmlBptAttributeCollection != null)
            {
                return xmlBptAttributeCollection.Count;
            }
            else
            {
                return -1;
            }
        }

        public int IsAttributeContained(string attributeName)
        {
            if(GetAttributesCount() > 0)
            {
                if (xmlBptAttributeCollection[attributeName].Value != null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }


        /* Constructors */

        public Bpt()
        {
            xmlBptNode = null;

            bptId = "";
            bptContent = "";
        }

        public Bpt(XmlNode xmlBptNode)
        {
            this.xmlBptNode = xmlBptNode;

            if (xmlBptNode != null)
            {
                xmlBptAttributeCollection = xmlBptNode.Attributes;

                if (xmlBptAttributeCollection != null)
                {
                    if (xmlBptAttributeCollection["id"] != null)
                    {
                        bptId = xmlBptNode.Attributes["id"].Value;
                    }
                }
            }
            
            bptContent = xmlBptNode.InnerText;

        }

    }
}
