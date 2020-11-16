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
using XLZ_Library;
using XLZ_Library.XLF.TransUnit.Languages.Elements;

/* This class intended use is to model structure of the <ept></ept> elements of the source/target node. ept as we can read from Xliff documentation stands for "end paired tag" and is coupled with bpt. 
 *
 * Documentation Description:
 * The <ept> element is used to delimit the end of a paired sequence of native codes. Each <ept> has a corresponding <bpt> element within the segment. 
 * 
 * Notes:
 * From the Xliff version 1.1. above <bpt><ept> nodes are paired by "rid" attribute, which is used in version 1.0. but not as a standard.
 *
 * What is the typical for ept node structure in Xlf file? 
 * 1.) Required Attributes:
 *     1.1.) id attribute which is occupied from the moment of <bpt> element with id of that value until there will be <ept> node with the same id. Then the id will be released.
 * 2.) Optional Attributes:
 *     2.1.) rid attribute;
 *     2.2.) ts attribute;
 *     2.3.) crc attribute;
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
 * 1.) XmlNode xmlEptNode field to store <ept> XmlNode with its XmlAttributes;
 * 2.) XmlAttributeCollection xmlEptAttributeCollection field to store the collection of attributes of the xmlEptNode;
 * 3.) String epId field to store string value of the id; [id value should be always integer in the case of ept tags]
 * 4.) String eptcontent field to store native code contained between <ept><\ept> tags. [Or <sub> elements which we will not model at the time]
 * 
 * Class for modelling should permit to:
 * 1.) Use Properties:
 *      1.1.) GetXmlNode to return the xmlEptNode;
 *      1.2.) GetId to return string value of the "id" attribute;
 *      1.3.) GetContent to return string value of the content contained between <ept></ept> tags.
 * 2.) Use Methods:
 *      2.1.) GetAttributesCount() to return the int value that indicates the number of attributes contained in ept XmlNode. Returns -1 if the list of XmlAttributes is null. 
 *      2.2.) IsAttributeContained(string attributeName) to return the int value that indicates whether attirbute of a attributeName is contained in the XmnlAttribute list or not. Returns 1 if is, 0 if not and -1 if XmlAttributeList is empty or null. 
 *      2.3.) GetXmlAttribute(string attributeName) to return XmlAttribute value for the given attributeName. If there is no attribute of that name in the XmlAttributeList (or the list is empty) method returns null. 
 *      2.4.) GetXmlAttributeValue(string attributeName) to return string value of the attribute of the given name. This method works in a same way as GetXmlAttribute(string attributeName) but returns empty string if there is no attribute of a given name.
 * 3.) Use Constructors:
 *      1.1.) Empty constructor to set all fields to null;
 *      1.2.) Constructor to which XmlNode is passed with ept node. 
 *		
 * Class for modeling should not permit to:
 * 
 *
 */
namespace XLZ_Library.XLF.TransUnit.Languages.Elements
{
    public class Ept
    {

        /* Fields */
        public XmlNode xmlEptNode;

        public XmlAttributeCollection xmlEptAttributeCollection;

        public string eptId;
        public string eptContent;

        /* Properties */

        public XmlNode GetXmlNode
        {
            get
            {
                return xmlEptNode;
            }
        }

        public string EptId
        {
            get
            {
                return eptId;
            }
        }

        public string EptContent
        {
            get
            {
                return eptContent;
            }
        }


        /* Methods */

        public int GetAttributesCount()
        {
            if (xmlEptAttributeCollection != null)
            {
                return xmlEptAttributeCollection.Count;
            }
            else
            {
                return -1;
            }
        }

        public int IsAttributeContained(string attributeName)
        {
            if (GetAttributesCount() > 0)
            {
                if (xmlEptAttributeCollection[attributeName].Value != null)
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

        public XmlAttribute GetXmlAttribute(string attributeName)
        {
            if (IsAttributeContained(attributeName) == 1)
            {
                return xmlEptAttributeCollection[attributeName];
            }
            else
            {
                return null;
            }
        }

        public string GetXmlAttributeValue(string attributeName)
        {
            XmlAttribute auxiliaryAttribute;

            if ((auxiliaryAttribute = GetXmlAttribute(attributeName)) != null)
            {
                return auxiliaryAttribute.Value;
            }
            else
            {
                return "";
            }
        }


        /* Constructors */

        public Ept()
        {
            xmlEptNode = null;

            eptId = "";
            eptContent = "";
        }

        public Ept(XmlNode xmlEptNode)
        {
            this.xmlEptNode = xmlEptNode;

            if (xmlEptNode != null)
            {
                xmlEptAttributeCollection = xmlEptNode.Attributes;

                if (xmlEptAttributeCollection != null)
                {
                    if (xmlEptAttributeCollection["id"] != null)
                    {
                        eptId = xmlEptNode.Attributes["id"].Value;
                    }
                }

                eptContent = xmlEptNode.InnerXml;
            }
            else
            {
                eptId = String.Empty;
                eptContent = String.Empty;
            }

        }
    }
}
