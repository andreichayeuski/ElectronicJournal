using System;
using System.Text;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class CoreMultiSelectRenderer
	{
		private CoreMultiSelect _model;

		public CoreMultiSelectRenderer(CoreMultiSelect model)
		{
			_model = model;
		}

		public string RenderHtml()
		{
			//if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
			//{
			//	return "This is a 30-day trial version of jqSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
			//}
			Guard.IsNotNullOrEmpty(_model.ID, "ID", "You need to set ID for this CoreComboBox instance.");
			return GetStandaloneJavascript();
		}

		private string GetStandaloneJavascript()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<span id='{0}'></span>", _model.ID);
			stringBuilder.Append("<script type='text/javascript'>\n");
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.AppendFormat("jQuery('#{0}').jqMultiSelect({{", _model.ID);
			stringBuilder.Append(GetStartupOptions());
			stringBuilder.Append("});");
			stringBuilder.Append("});");
			stringBuilder.Append("</script>");
			return stringBuilder.ToString();
		}

		private string GetStartupOptions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			CoreMultiSelect model = _model;
			MultiSelectClientSideEvents clientSideEvents = _model.ClientSideEvents;
			stringBuilder.AppendFormat("id:'{0}'", model.ID);
			stringBuilder.AppendFormat(",width:{0}", model.Width.ToString());
			stringBuilder.AppendFormat(",height:{0}", model.Height.ToString());
			if (model.DropDownWidth.HasValue)
			{
				stringBuilder.AppendFormat(",dropDownWidth:{0}", model.DropDownWidth.ToString());
			}
			if (model.TabIndex != 0)
			{
				stringBuilder.AppendFormat(",tabIndex:{0}", model.TabIndex.ToString());
			}
			if (model.Items.Count > 0)
			{
				stringBuilder.AppendFormat(",items:{0}", JsonConvert.SerializeObject((object)model.SerializeItems(model.Items)));
			}
			if (!string.IsNullOrEmpty(model.ItemTemplateID))
			{
				stringBuilder.AppendFormat(",itemTemplateID:'{0}'", model.ItemTemplateID);
			}
			if (!string.IsNullOrEmpty(model.HeaderTemplateID))
			{
				stringBuilder.AppendFormat(",headerTemplateID:'{0}'", model.HeaderTemplateID);
			}
			if (!string.IsNullOrEmpty(model.FooterTemplateID))
			{
				stringBuilder.AppendFormat(",footerTemplateID:'{0}'", model.FooterTemplateID);
			}
			if (!string.IsNullOrEmpty(model.ToggleImageCssClass))
			{
				stringBuilder.AppendFormat(",toggleImageCssClass:'{0}'", model.ToggleImageCssClass);
			}
			if (model.Filter != 0)
			{
				stringBuilder.AppendFormat(",filter:'{0}'", model.Filter.ToString().ToLower());
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Show))
			{
				stringBuilder.AppendFormat(",onShow:{0}", clientSideEvents.Show);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Hide))
			{
				stringBuilder.AppendFormat(",onHide:{0}", clientSideEvents.Hide);
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
			if (!string.IsNullOrEmpty(clientSideEvents.Select))
			{
				stringBuilder.AppendFormat(",onInitialized:{0}", clientSideEvents.Initialized);
			}
			if (!string.IsNullOrEmpty(clientSideEvents.Select))
			{
				stringBuilder.AppendFormat(",onKeyDown:{0}", clientSideEvents.KeyDown);
			}
			return stringBuilder.ToString();
		}

		private string GetButtonText()
		{
			string text = "";
			foreach (CoreListItem item in _model.Items)
			{
				if (item.Selected)
				{
					text = item.Text;
					break;
				}
			}
			if (string.IsNullOrEmpty(text) && _model.Items.Count > 0)
			{
				text = _model.Items[0].Text;
			}
			return text;
		}
	}
}
