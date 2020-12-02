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
using XLZ_Library.XLF.TransUnit.Languages.Elements;

/* This class intended use is to model structure of the <source></source> elements of the trans-unit node.  
 * 
 * Documentation Descrition:
 * Source text - The <source> element is used to delimit a unit of text that could be a paragraph, a title, a menu item, a caption, etc. 
 * 
 * Notes:
 * 
 * What is the typical for source node structure in Xlf file? 
 * 1.) Required Attributes: None;
 * 2.) Optional Attributes:
 *     2.1.) xml:lang - Language - The xml:lang attribute specifies the locale of the text of a given element.
 *               Default value is undefined whereas value should be A language code as described in the [RFC 3066]. This declared value is considered to apply to all elements within the content of the element where it is specified, unless overridden with another instance of the xml:lang attribute. Unlike the other XLIFF attributes, the values for xml:lang are not case-sensitive. For more information see the section on xml:lang in the XML specification, and the erratum E11 (which replaces RFC 1766 by RFC 3066).
 *     2.2.) ts - Tool-specific data - The ts attribute allows you to include short data understood by a specific toolset. 
 *               Default value is empty string whereas value should be string name not specified by the standard as it is tool specific. 
 * 3.) Content, which should be Text or zero or more of the following elements in any order: <g>, <x/>, <bx/>, <ex/>, <bpt>, <ept>, <ph>, <it>, <mrk>.
 * 
 * Class for modelling should contain:
 * 1.) XmlNode xmlSourceNode field to store <source> XmlNode with its XmlAttributes;
 * 2.) List<Bpt> bptList field to store the list of all Bpt objects created from <bpt> child nodes;
 * 3.) List<Ept> eptList field to store the list of all Ept objects created from <ept> child nodes;
 * 4.) List<It> itList field to store the list of all It objects created from <it> child nodes;
 * 5.) List<Ph> phList field to store the list of all Ph objects created from <ph> child nodes;
 * 
 * Class for modelling should permit to:
 * 1.) Use Properties:
 *      1.1.) GetXmlNode to return the xmlTransUnitNode;
 *      1.2.) GetId to return string value of the "id" attribute;
 *      1.3.) GetTranslate to return string value of the "translatable" attribute.
 * 2.) Use Methods:
 * 3.) Use Constructors:
 *      1.1.) Empty constructor to set all fields to null;
 *      1.2.) Constructor to which XmlNode is passed with source node. 
 *		
 * Class for modeling should not permit to:
 * 
 *
 */

namespace XLZ_Library.XLF.TransUnit.Languages
{
    public class Source
    {

        /* Fields */

        public XmlNode xmlSourceNode;

        public List<Bpt> bptList = new List<Bpt>();
        public List<Ept> eptList = new List<Ept>();
        public List<It> itList = new List<It>();
        public List<Ph> phList = new List<Ph>();

        public string sourceContent;

        //public XmlNodeList xmlBptList;
        //public XmlNodeList xmlEptList;
        //public XmlNodeList xmlItList;
        //public XmlNodeList xmlPhList;

        /* Properties */

        public XmlNode GetXmlNode
        {
            get
            {
                return xmlSourceNode;
            }
        }

        public List<Bpt> GetBptList
        {
            get
            {
                return bptList;
            }
        }

        public List<Ept> GetEptList
        {
            get
            {
                return eptList;
            }
        }

        public List<It> GetItList
        {
            get
            {
                return itList;
            }
        }

        public List<Ph> GetPhList
        {
            get
            {
                return phList;
            }
        }

        public string SourceContent
        {
            get
            {
                return sourceContent;
            }
        }

        public int CountBptEpt
        {
            get
            {
                if(bptList != null)
                {
                    if (eptList != null)
                    {
                        if (bptList.Count == eptList.Count)
                        {
                            for (int i = 1; i < bptList.Count + 1; i++)
                            {
                                if (bptList[i].bptId != eptList[i].eptId)
                                {
                                    return -1;
                                }
                            }

                            return bptList.Count;

                        }
                        else
                        {
                            return -1; // Here should be returned maximal count of those bpt elements that have corresponding ept element. 
                        }
                    }
                    else
                    {
                        return -1; 
                    }
                }
                else
                {
                    if (eptList != null)
                    {
                        return -1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        /* Methods */

        /* Constructors */

        public Source()
        {
            xmlSourceNode = null;
        }

        public Source(XmlNode xmlSourceNode)
        {
            this.xmlSourceNode = xmlSourceNode;

            if (xmlSourceNode != null)
            {

               XmlNodeList bptNodeList = xmlSourceNode.OwnerDocument.SelectNodes("\\bpt");
               Bpt auxiliaryBpt;

                if (bptNodeList != null)
                {
                    foreach (XmlNode bptNode in bptNodeList)
                    {
                        auxiliaryBpt = new Bpt(bptNode);
                        bptList.Add(auxiliaryBpt);
                    }
                }


                XmlNodeList eptNodeList = xmlSourceNode.OwnerDocument.SelectNodes("\\ept");
                Ept auxiliaryEpt;

                if (eptNodeList != null)
                {
                    foreach (XmlNode eptNode in eptNodeList)
                    {
                        auxiliaryEpt = new Ept(eptNode);
                        eptList.Add(auxiliaryEpt);
                    }
                }


                XmlNodeList itNodeList = xmlSourceNode.OwnerDocument.SelectNodes("\\it");
                It auxiliaryIt;

                if (itNodeList != null)
                {
                    foreach (XmlNode itNode in itNodeList)
                    {
                        auxiliaryIt = new It(itNode);
                        itList.Add(auxiliaryIt);
                    }
                }


                XmlNodeList phNodeList = xmlSourceNode.OwnerDocument.SelectNodes("\\ph");
                Ph auxiliaryPh;

                if (phNodeList != null)
                {
                    foreach (XmlNode phNode in phNodeList)
                    {
                        auxiliaryPh = new Ph(phNode);
                        phList.Add(auxiliaryPh);
                    }
                }

                sourceContent = xmlSourceNode.InnerText;

            }
            else
            {

            }

        }
    }
}
