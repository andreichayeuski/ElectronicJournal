using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	public class CoreTreeView
	{
		private CoreTreeNode _nodeByValue;

		public string ID
		{
			get;
			set;
		}

		public string DataUrl
		{
			get;
			set;
		}

		public string DragAndDropUrl
		{
			get;
			set;
		}

		public string Height
		{
			get;
			set;
		}

		public string Width
		{
			get;
			set;
		}

		public bool HoverOnMouseOver
		{
			get;
			set;
		}

		public bool CheckBoxes
		{
			get;
			set;
		}

		public bool MultipleSelect
		{
			get;
			set;
		}

		public TreeViewClientSideEvents ClientSideEvents
		{
			get;
			set;
		}

		public string NodeTemplateID
		{
			get;
			set;
		}

		public bool DragAndDrop
		{
			get;
			set;
		}

		public CoreTreeView()
		{
			ID = "";
			DataUrl = "";
			DragAndDropUrl = "";
			Width = "";
			Height = "";
			HoverOnMouseOver = true;
			CheckBoxes = false;
			MultipleSelect = false;
			NodeTemplateID = "";
			ClientSideEvents = new TreeViewClientSideEvents();
			DragAndDrop = false;
		}

		public JsonResult DataBind(List<CoreTreeNode> nodes)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			return new JsonResult((object)JsonConvert.SerializeObject((object)SerializeNodes(nodes)));
		}

		private List<Hashtable> SerializeNodes(List<CoreTreeNode> nodes)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (CoreTreeNode node in nodes)
			{
				list.Add(node.ToHashtable());
			}
			return list;
		}

		public List<CoreTreeNode> GetAllNodesFlat(List<CoreTreeNode> nodes)
		{
			List<CoreTreeNode> list = new List<CoreTreeNode>();
			foreach (CoreTreeNode node in nodes)
			{
				list.Add(node);
				if (node.Nodes.Count > 0)
				{
					GetNodesFlat(node.Nodes, list);
				}
			}
			return list;
		}

		private void GetNodesFlat(List<CoreTreeNode> nodes, List<CoreTreeNode> result)
		{
			foreach (CoreTreeNode node in nodes)
			{
				result.Add(node);
				if (node.Nodes.Count > 0)
				{
					GetNodesFlat(node.Nodes, result);
				}
			}
		}

		public CoreTreeNodeDropEventArgs GetDragDropInfo(HttpContext context)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			string text =context.Request.Query["args"];
			return JsonConvert.DeserializeObject<CoreTreeNodeDropEventArgs>(text);
		}

		public CoreTreeNode FindNodeByValue(List<CoreTreeNode> nodes, string value)
		{
			_nodeByValue = null;
			FindNodeByValueInternal(nodes, value);
			return _nodeByValue;
		}

		private void FindNodeByValueInternal(List<CoreTreeNode> nodes, string value)
		{
			foreach (CoreTreeNode node in nodes)
			{
				if (node.Value == value)
				{
					_nodeByValue = node;
					break;
				}
				if (node.Nodes.Count > 0)
				{
					FindNodeByValueInternal(node.Nodes, value);
				}
			}
		}
	}
}
