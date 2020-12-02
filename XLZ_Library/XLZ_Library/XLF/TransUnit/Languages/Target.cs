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

namespace XLZ_Library.XLF.TransUnit.Languages
{
    public class Target
    {

        /* Fields */

        public XmlNode xmlTargetNode;

        public List<Bpt> bptList = new List<Bpt>();
        public List<Ept> eptList = new List<Ept>();
        public List<It> itList = new List<It>();
        public List<Ph> phList = new List<Ph>();

        //public XmlNodeList xmlBptList;
        //public XmlNodeList xmlEptList;
        //public XmlNodeList xmlItList;
        //public XmlNodeList xmlPhList;

        /* Properties */

        /* Methods */

        /* Constructors */

        public Target()
        {
            xmlTargetNode = null;
        }

        public Target(XmlNode xmlTargetNode)
        {
            this.xmlTargetNode = xmlTargetNode;

            if (xmlTargetNode != null)
            {

                XmlNodeList bptNodeList = xmlTargetNode.OwnerDocument.SelectNodes("\\bpt");
                Bpt auxiliaryBpt;

                if (bptNodeList != null)
                {
                    foreach (XmlNode bptNode in bptNodeList)
                    {
                        auxiliaryBpt = new Bpt(bptNode);
                        bptList.Add(auxiliaryBpt);
                    }
                }
                

                XmlNodeList eptNodeList = xmlTargetNode.OwnerDocument.SelectNodes("\\ept");
                Ept auxiliaryEpt;

                if (eptNodeList != null)
                {
                    foreach (XmlNode eptNode in eptNodeList)
                    {
                        auxiliaryEpt = new Ept(eptNode);
                        eptList.Add(auxiliaryEpt);
                    }
                }
                

                XmlNodeList itNodeList = xmlTargetNode.OwnerDocument.SelectNodes("\\it");
                It auxiliaryIt;

                if (itNodeList != null)
                {
                    foreach (XmlNode itNode in itNodeList)
                    {
                        auxiliaryIt = new It(itNode);
                        itList.Add(auxiliaryIt);
                    }
                }


                XmlNodeList phNodeList = xmlTargetNode.OwnerDocument.SelectNodes("\\ph");
                Ph auxiliaryPh;

                if (phNodeList != null)
                {
                    foreach (XmlNode phNode in phNodeList)
                    {
                        auxiliaryPh = new Ph(phNode);
                        phList.Add(auxiliaryPh);
                    }
                }            
            }
            else
            {

            }

        }
    }
}
