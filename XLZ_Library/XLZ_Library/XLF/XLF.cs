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

namespace XLZ_Library.XLF
{
	public class XLF
	{

		/* Fields */

		public XmlDocument xlfDocument;

		public XmlNode head;
		public XmlNode body;

		public XmlNodeList xmlTransUnitList;

		/* Properties */


		/* Constructors */

		public XLF(XmlDocument inputFile)
		{
			/* Here should be added validation of XML file. */
			xlfDocument = inputFile;

			head = xlfDocument.SelectSingleNode("//header");
			body = xlfDocument.SelectSingleNode("//body");

			xmlTransUnitList = inputFile.GetElementsByTagName("trans-unit");

		}
	}
}


		/*public int MaximalNumericalID
		{
			get
			{
				return transUnitDataList.Last(x => x.DoesHaveNumericalID == true).ID;
			}
		}

		public TransUnitNode GetTransUnitNode(int index)
		{
			return transUnitDoublyLinkedList[index];
		}

		public TransUnitData GetTransUnitData(int index)
		{
			if (GetTransUnitNode(index) != null) return transUnitDoublyLinkedList[index].Data;
			else return null;
		}

		public TransUnitNode GetTransUnitNodeByID(int id)
		{

			TransUnitNode auxiliaryTransUnitNode = transUnitDoublyLinkedList.Tail;

			while (auxiliaryTransUnitNode != null)
			{
				if (auxiliaryTransUnitNode.Data.ID == id)
				{
					return auxiliaryTransUnitNode;
				}

				auxiliaryTransUnitNode = auxiliaryTransUnitNode.NextSibling;

			}

			return null;
		}

		public TransUnitData GetTransUnitDataByID(int id)
		{

			if (GetTransUnitNodeByID(id) != null) return GetTransUnitNodeByID(id).Data;
			else return null;
		}

		public TransUnitNode GetTransUnitNodeByGeneralID(string generalId)
		{

			TransUnitNode auxiliaryTransUnitNode = transUnitDoublyLinkedList.Tail;

			while (auxiliaryTransUnitNode != null)
			{
				if (auxiliaryTransUnitNode.Data.GeneralID == generalId)
				{
					return auxiliaryTransUnitNode;
				}

				auxiliaryTransUnitNode = auxiliaryTransUnitNode.NextSibling;

			}

			return null;
		}

		public TransUnitData GetTransUnitDataByGeneralID(string generalID)
		{

			if (GetTransUnitNodeByGeneralID(generalID) != null) return GetTransUnitNodeByGeneralID(generalID).Data;
			else return null;
		}
		public TransUnitNode GetPreviousTranslatableNode(TransUnitNode searchedNode)
		{

			TransUnitNode auxiliaryNode = null;
			if (searchedNode != null)
			{

				auxiliaryNode = searchedNode.PreviousSibling;
				if (searchedNode != transUnitDoublyLinkedList.Tail)
				{

					while (auxiliaryNode != null)
					{
						if (auxiliaryNode.Data.IsTranslatable)
						{
							return auxiliaryNode;
						}

						auxiliaryNode = auxiliaryNode.PreviousSibling;
					}

				}

				return auxiliaryNode;
			}

			return auxiliaryNode;

		}


		public TransUnitNode GetNextTranslatableNode(TransUnitNode searchedNode)
		{

			TransUnitNode auxiliaryNode = null;
			if (searchedNode != null)
			{

				auxiliaryNode = searchedNode.NextSibling;
				if (searchedNode != transUnitDoublyLinkedList.Head)
				{

					while (auxiliaryNode != null)
					{
						if (auxiliaryNode.Data.IsTranslatable)
						{
							return auxiliaryNode;
						}

						auxiliaryNode = auxiliaryNode.NextSibling;
					}

				}

				return auxiliaryNode;
			}

			return auxiliaryNode;

		}
		public XLF()
		{

			xlfDocument = null;

			transUnitDataList = new List<TransUnitData>();
			transUnitDoublyLinkedList = new DoublyLinkedList();
			isParsedCorrectly = false;

		}
		public XLF(XmlDocument inputFile)
		{

			xlfDocument = inputFile;

			TransUnitData auxiliaryTransUnitData;

			transUnitDataList = new List<TransUnitData>();
			transUnitDoublyLinkedList = new DoublyLinkedList();


			XmlNodeList transUnitList = inputFile.GetElementsByTagName("trans-unit");
			if (transUnitList.Count > 0) isParsedCorrectly = true;

			foreach (XmlNode transUnit in transUnitList)
			{
				if (transUnit != null)
				{

					auxiliaryTransUnitData = new TransUnitData(transUnit);

					if (auxiliaryTransUnitData.IsWellFormed)
					{
						transUnitDataList.Add(auxiliaryTransUnitData);
						transUnitDoublyLinkedList.InsertNext(auxiliaryTransUnitData);
					}
				}
			}

			if (transUnitList.Count == transUnitDoublyLinkedList.Count && transUnitDataList.Contains(null) == false)
			{
				isParsedCorrectly = true;
			}

		}
}*/
