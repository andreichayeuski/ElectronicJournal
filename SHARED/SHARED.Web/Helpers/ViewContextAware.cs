using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED.Web.Helpers
{

    public partial class ViewContextAware : IViewContextAware
    {
        public ViewContextAware(IHtmlHelper htmlHelper)
        {
            this.Html = htmlHelper;
        }
        public IHtmlHelper Html
        {
            get;
            private set;
        }

        void IViewContextAware.Contextualize(ViewContext viewContext)
        {
            if (Html is IViewContextAware)
            {
                ((IViewContextAware)Html).Contextualize(viewContext);
            }
        }
    }
}
