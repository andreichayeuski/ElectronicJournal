using System;
using System.Text;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class CoreTreeViewRenderer
	{
		private CoreTreeView _model;

		public CoreTreeViewRenderer(CoreTreeView model)
		{
			_model = model;
		}

		public string RenderHtml()
		{
			//if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			//{
			//	return "This is a 30-day trial version of CoreSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			//}
			Guard.IsNotNullOrEmpty(_model.ID, "ID", "You need to set ID for this CoreTreeView instance.");
			Guard.IsNotNullOrEmpty(_model.DataUrl, "DataUrl", "You need to set DataUrl to the Action of the tree returning nodes.");
			return GetStandaloneJavascript();
		}

		private string GetStandaloneJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<div id='{0}'/>", _model.ID);
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.AppendFormat("jQuery('#{0}').jqTreeView({{", _model.ID);
			stringBuilder.Append(GetStartupOptions());
			stringBuilder.Append("});");
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private string GetStartupOptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			CoreTreeView model = _model;
			TreeViewClientSideEvents clientSideEvents = model.ClientSideEvents;
			stringBuilder.AppendFormat("id: '{0}'", model.ID);
			if (!string.IsNullOrEmpty(model.Height))
			{
				stringBuilder.AppendFormat(",height: '{0}'", model.Height);
			}
			if (!string.IsNullOrEmpty(model.Width))
			{
				stringBuilder.AppendFormat(",width: '{0}'", model.Height);
			}
			if (!string.IsNullOrEmpty(model.DataUrl))
			{
				stringBuilder.AppendFormat(",dataUrl: '{0}'", model.DataUrl);
			}
			if (!string.IsNullOrEmpty(model.DragAndDropUrl))
			{
				stringBuilder.AppendFormat(",dragAndDropUrl: '{0}'", model.DragAndDropUrl);
			}
			if (!model.HoverOnMouseOver)
			{
				stringBuilder.AppendFormat(",hoverOnMouseOver:false");
			}
			if (model.CheckBoxes)
			{
				stringBuilder.Append(",checkBoxes:true");
			}
			if (model.MultipleSelect)
			{
				stringBuilder.Append(",multipleSelect:true");
			}
			if (model.DragAndDrop)
			{
				stringBuilder.Append(",dragAndDrop:true");
			}
			if (!string.IsNullOrEmpty(model.NodeTemplateID))
			{
				stringBuilder.AppendFormat(",nodeTemplateID:'{0}'", model.NodeTemplateID);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Check))
			{
				stringBuilder.AppendFormat(",onCheck:{0}", clientSideEvents.Check);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Collapse))
			{
				stringBuilder.AppendFormat(",onCollapse:{0}", clientSideEvents.Collapse);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Expand))
			{
				stringBuilder.AppendFormat(",onExpand:{0}", clientSideEvents.Expand);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.MouseOut))
			{
				stringBuilder.AppendFormat(",onMouseOut:{0}", clientSideEvents.MouseOut);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.MouseOver))
			{
				stringBuilder.AppendFormat(",onMouseOver:{0}", clientSideEvents.MouseOver);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Select))
			{
				stringBuilder.AppendFormat(",onSelect:{0}", clientSideEvents.Select);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.NodesDragged))
			{
				stringBuilder.AppendFormat(",onNodesDragged:{0}", clientSideEvents.NodesDragged);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.NodesMoved))
			{
				stringBuilder.AppendFormat(",onNodesMoved:{0}", clientSideEvents.NodesMoved);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.NodesDropped))
			{
				stringBuilder.AppendFormat(",onNodesDropped:{0}", clientSideEvents.NodesDropped);
			}
			return stringBuilder.ToString();
		}
	}
}
