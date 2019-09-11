using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EJ.Models.UI;
using Microsoft.AspNetCore.Mvc;

namespace EJ.Web.ViewComponents
{
    public class MenuItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Tuple<MenuLinkItem, bool> item)
        {
            return View("Item", item);
        }

    }
}
