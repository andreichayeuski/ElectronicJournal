using System.Collections;
using System.Text;
using Newtonsoft.Json;

namespace Trirand.Web.Core.Trirand.Web.Core
{
	internal class JsonCustomButton
	{
		private Hashtable _jsonValues;

		private CoreGridToolBarButton _button;

		public JsonCustomButton(CoreGridToolBarButton button)
		{
			_jsonValues = new Hashtable();
			_button = button;
		}

		public string Process()
		{
			string value = string.IsNullOrEmpty(_button.Text) ? " " : _button.Text;
			if (!string.IsNullOrEmpty(_button.Text))
			{
				_jsonValues["caption"] = value;
			}
			if (!string.IsNullOrEmpty(_button.ButtonIcon))
			{
				_jsonValues["buttonicon"] = _button.ButtonIcon;
			}
			_jsonValues["position"] = _button.Position.ToString().ToLower();
			if (!string.IsNullOrEmpty(_button.ToolTip))
			{
				_jsonValues["title"] = _button.ToolTip;
			}
			string text = JsonConvert.SerializeObject((object)_jsonValues);
			StringBuilder stringBuilder = new StringBuilder();
			RenderClientSideEvent(text, stringBuilder, "onClickButton", _button.OnClick);
			return text.Insert(text.Length - 1, stringBuilder.ToString());
		}

		private void RenderClientSideEvent(string json, StringBuilder sb, string jsName, string eventName)
		{
			if (!string.IsNullOrEmpty(eventName))
			{
				sb.AppendFormat(",{0}:function() {{ {1}(); }}", jsName, eventName);
			}
		}
	}
}
