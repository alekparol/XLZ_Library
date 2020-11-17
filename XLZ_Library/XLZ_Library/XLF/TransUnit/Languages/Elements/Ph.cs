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
using XLZ_Library.XLF.TransUnph.Languages.Elements;


/* This class intended use is to model structure of the <ph></ph> elements of the source/target node. ph as we can read from Xliff documentation stands for "isolated tag". 
 *
 * Documentation Description:
 * The <ph> element is used to delimit a sequence of native stand-alone codes in the segment.
 *
 * Note:
 * In our case <ph></ph> delimits mostly native code parts of the segments locked by TiLT. Like in the example: 
 * <ph equiv-text="&quot;}" id="1" tilt:type="do-not-translate">&amp;quot;}</ph>
 *
 * What is the typical for ph node structure in Xlf file? 
 * 1.) Required Attributes:
 *     1.1.) id - The id attribute is used in many elements, usually as a unique reference to the original corresponding format for the given element.
 *                Default value is empty sting whereas value should be alpha numeric wphhout spaces.            
 * 2.) Optional Attributes:
 *     2.1.) ctype
 *     2.2.) ts
 *     2.3.) crc
 *     2.4.) assoc
 *     2.5.) equiv-test
 * 3.) Content which should be native code or zero or more <sub> elements.  
 * 
 * Definphion:
 * 
 * Machine code: This is the most well-defined one. Ph is code that uses the byte-code instructions which your processor (the physical piece of metal that does the actual work) understands and executes directly. 
 * All other code must be translated or transformed into machine code before your machine can execute ph.
 * 
 * Native code: This term is sometimes used in places where machine code (see above) is meant. However, ph is also sometimes used to mean unmanaged code (see below).
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
 * 1.) XmlNode xmlPhNode field to store <ph> XmlNode with XmlAttributes;
 * 2.) XmlAttributeCollection xmlPhAttributeCollection field to store the collection of attributes of the xmlPhNode;
 * 3.) String phId field to store string value of the id; [id value should be always integer in the case of ph tags]
 * 4.) String phContent field to store native code contained between <ph><\ph> tags. [Or <sub> elements which we will not model at the time]
 * 
 * Class for modelling should permit to:
 * 1.) Use Properties:
 *      1.1.) GetXmlNode to return the xmlPhNode;
 *      1.2.) GetId to return string value of the "id" attribute;
 *      1.3.) GetEquivText to return string value of the "equiv-text" attribute;
 *      1.4.) GetContent to return string value of the content contained between <ph></ph> tags.
 * 2.) Use Methods:
 *      2.1.) GetAttributesCount() to return the int value that indicates the number of attributes contained in ph XmlNode. Returns -1 if the list of XmlAttributes is null. 
 *      2.2.) IsAttributeContained(string attributeName) to return the int value that indicates whether attirbute of a attributeName is contained in the XmnlAttribute list or not. Returns 1 if is, 0 if not and -1 if XmlAttributeList is empty or null. 
 *      2.3.) GetXmlAttribute(string attributeName) to return XmlAttribute value for the given attributeName. If there is no attribute of that name in the XmlAttributeList (or the list is empty) method returns null. 
 *      2.4.) GetXmlAttributeValue(string attributeName) to return string value of the attribute of the given name. This method works in a same way as GetXmlAttribute(string attributeName) but returns empty string if there is no attribute of a given name.
 * 3.) Use Constructors:
 *      1.1.) Empty constructor to set all fields to null;
 *      1.2.) Constructor to which XmlNode is passed wphh ph node. 
 *		
 * Class for modeling should not permit to:
 * 
 *
 */
namespace XLZ_Library.XLF.TransUnph.Languages.Elements
{
    public class Ph
    {

        /* Fields */
        public XmlNode xmlPhNode;

        public XmlAttributeCollection xmlPhAttributeCollection;

        public string phId;
        public string phContent;

        /* Properties */

        public XmlNode GetXmlNode
        {
            get
            {
                return xmlPhNode;
            }
        }

        public string PhId
        {
            get
            {
                return phId;
            }
        }

        public string PhContent
        {
            get
            {
                return phContent;
            }
        }


        /* Methods */

        public int GetAttributesCount()
        {
            if (xmlPhAttributeCollection != null)
            {
                return xmlPhAttributeCollection.Count;
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
                if (xmlPhAttributeCollection[attributeName].Value != null)
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
                return xmlPhAttributeCollection[attributeName];
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

        public Ph()
        {
            xmlPhNode = null;

            phId = "";
            phContent = "";
        }

        public Ph(XmlNode xmlPhNode)
        {
            this.xmlPhNode = xmlPhNode;

            if (xmlPhNode != null)
            {
                xmlPhAttributeCollection = xmlPhNode.Attributes;

                if (xmlPhAttributeCollection != null)
                {
                    if (xmlPhAttributeCollection["id"] != null)
                    {
                        phId = xmlPhNode.Attributes["id"].Value;
                    }
                }

                phContent = xmlPhNode.InnerXml;
            }
            else
            {
                phId = String.Empty;
                phContent = String.Empty;
            }

        }

    }
}
