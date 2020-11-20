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

/* This class intended use is to model structure of the <it></it> elements of the source/target node. it as we can read from Xliff documentation stands for "isolated tag". 
 *
 * Documentation Description:
 * The <it> element is used to delimit a beginning/ending sequence of native codes that does not have its corresponding ending/beginning within the segment.
 *
 * Notes:
 * - In our case <it></it> delimits mostly parts of the segments locked by TiLT. Like in the example: 
 * <it id="13" pos="close" tilt:origid="10">&lt;/p&gt;</it>
 * - It is worth to mention that id is not unique for it nor ph tags. If the content of the <it></it> tag is the same, it has the same id. Therefore id is content-invariant. 
 *
 * What is the typical for it node structure in Xlf file? 
 * 1.) Required Attributes:
 *     1.1.) id - The id attribute is used in many elements, usually as a unique reference to the original corresponding format for the given element.
 *                Default value is empty string whereas value should be alpha numeric without spaces.            
 *     1.2.) pos - The beginning or end of an isolated tag.
 *                There is no default value for this attribute. It should have only two possible values - "open" or "close" as a reference of opening or closing of given isolated tag.
 * 2.) Optional Attributes:
 *     2.1.) rid - Reference identifier - The rid attribute is used to link different elements that are related. For example, a reference to its definition, or paragraphs belonging to the same group, etc.
 *               Default value is empty string whereas value should be alpha numeric without spaces. 
 *    2.2.) ctype - Content Type - The type attribute specifies the content and the type of resource or style of the data of a given element. For example, to define if it is a label, or a menu item in the case of resource-type data, or the style in the case of document-related data.
 *               Default value is empty string whereas value should be string name for the attribute like: bold (bold or strong text), font (text with font size, font face, color changes etc. ), italic (italicized text), link (hypertext), underlined (underlined text).
 *    2.3.) ts - Tool-specific data - The ts attribute allows you to include short data understood by a specific toolset. 
 *               Default value is empty string whereas value should be string name not specified by the standard as it is tool specific. 
 *    2.4.) crc - A private crc value used to verify data as it is returned to the producer. The generation and verification of this number is tool-specific.
 *               Default value is null whereas value should be numerical. 
 * 3.) Content which should be native code or zero or more <sub> elements.  
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
 * 1.) XmlNode xmlItNode field to store <it> XmlNode with its XmlAttributes;
 * 2.) XmlAttributeCollection xmlItAttributeCollection field to store the collection of attributes of the xmlItNode;
 * 3.) String itId field to store string value of the id; [id value should be always integer in the case of it tags]
 * 4.) String itcontent field to store native code contained between <it><\it> tags. [Or <sub> elements which we will not model at the time]
 * 
 * Class for modelling should permit to:
 * 1.) Use Properties:
 *      1.1.) GetXmlNode to return the xmlItNode;
 *      1.2.) GetId to return string value of the "id" attribute;
 *      1.3.) GetPos to return string value of the "pos" attribute;
 *      1.4.) GetContent to return string value of the content contained between <it></it> tags.
 * 2.) Use Methods:
 *      2.1.) GetAttributesCount() to return the int value that indicates the number of attributes contained in it XmlNode. Returns -1 if the list of XmlAttributes is null. 
 *      2.2.) IsAttributeContained(string attributeName) to return the int value that indicates whether attirbute of a attributeName is contained in the XmnlAttribute list or not. Returns 1 if is, 0 if not and -1 if XmlAttributeList is empty or null. 
 *      2.3.) GetXmlAttribute(string attributeName) to return XmlAttribute value for the given attributeName. If there is no attribute of that name in the XmlAttributeList (or the list is empty) method returns null. 
 *      2.4.) GetXmlAttributeValue(string attributeName) to return string value of the attribute of the given name. This method works in a same way as GetXmlAttribute(string attributeName) but returns empty string if there is no attribute of a given name.
 * 3.) Use Constructors:
 *      1.1.) Empty constructor to set all fields to null;
 *      1.2.) Constructor to which XmlNode is passed with it node. 
 *		
 * Class for modeling should not permit to:
 * 
 *
 */
namespace XLZ_Library.XLF.TransUnit.Languages.Elements
{
    public class It
    {

        /* Fields */
        public XmlNode xmlItNode;

        public XmlAttributeCollection xmlItAttributeCollection;

        public string itId;
        public string itPos;
        public string itContent;

        /* Properties */

        public XmlNode GetXmlNode
        {
            get
            {
                return xmlItNode;
            }
        }

        public int ItId
        {
            get
            {
                if (itId != String.Empty)
                {
                    if (!itId.Contains(" "))
                    {
                        return Int32.Parse(itId);
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
        }

        public string ItPos
        {
            get
            {
                return itPos;
            }
        }

        public string ItContent
        {
            get
            {
                return itContent;
            }
        }


        /* Methods */

        public int GetAttributesCount()
        {
            if (xmlItAttributeCollection != null)
            {
                return xmlItAttributeCollection.Count;
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
                if (xmlItAttributeCollection[attributeName] != null)
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
                return xmlItAttributeCollection[attributeName];
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

        public XmlAttribute GetRidAttribute()
        {
            return GetXmlAttribute("rid");
        }

        public XmlAttribute GetCtypeAttribute()
        {
            return GetXmlAttribute("ctype");
        }

        public XmlAttribute GetTsAttribute()
        {
            return GetXmlAttribute("ts");
        }

        public XmlAttribute GetCrcAttribute()
        {
            return GetXmlAttribute("crc");
        }


        /* Constructors */

        public It()
        {
            xmlItNode = null;

            itId = "";
            itContent = "";
        }

        public It(XmlNode xmlItNode)
        {
            this.xmlItNode = xmlItNode;

            if (xmlItNode != null)
            {
                xmlItAttributeCollection = xmlItNode.Attributes;

                if (xmlItAttributeCollection != null)
                {
                    if (xmlItAttributeCollection["id"] != null)
                    {
                        itId = xmlItNode.Attributes["id"].Value;
                    }

                    if (xmlItAttributeCollection["pos"] != null)
                    {
                        itPos = xmlItNode.Attributes["pos"].Value;
                    }
                }

                itContent = xmlItNode.InnerXml;
            }
            else
            {
                itId = String.Empty;
                itContent = String.Empty;
            }

        }

    }
}
