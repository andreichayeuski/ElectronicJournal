using Microsoft.AspNetCore.Html;
using SHARED.Common.Utils;
using SHARED.Web.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Web.Other
{
    public class UserMessage
    {
        public UserMessage()
        {
            MessageTitle = "Вы уверены?";
            MessageIdentifier = DateTime.Now.Ticks.ToString();
            Buttons = Arr.Empty<IHtmlContent>();
        }

        public string MessageIdentifier { get; private set; }
        public IHtmlContent Content { get; set; }
        public string MessageTitle { get; set; }
        public IEnumerable<IHtmlContent> Buttons { get; set; }
        public UserMessageTypeEnum Type { get; set; }
        public bool ImmediatelyRan { get; set; }
    }
}
