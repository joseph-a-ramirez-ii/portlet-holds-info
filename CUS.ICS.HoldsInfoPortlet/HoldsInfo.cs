using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jenzabar.Common.Web.UI.Controls;
using Jenzabar.Portal.Framework.Web.UI;
using Jenzabar.Portal.Framework.Security.Authorization;

namespace CUS.ICS.HoldsInfo
{
    //[PortletOperation(
    //"CanAdminPortlet",
    //"Can Administer  Portlet",
    //"Whether a user can administer this portlet or not",
    //PortletOperationScope.Portlet)]

    public class HoldsInfo : Jenzabar.Portal.Framework.Web.UI.PortletBase
    {

        protected override Jenzabar.Portal.Framework.Web.UI.PortletViewBase GetCurrentScreen()
        {
           // Jenzabar.Portal.Framework.Web.UI.PortletViewBase screen = null;

           // switch (this.CurrentPortletScreenName)
           // {
           //     case "Holds":
           //     case "Default":
           //     default:
           //         screen = this.LoadPortletView("ICS/Portlet.HoldsInfoTLU/Holds_View.ascx");
           //         break;
           // }

           // return screen;

            return (this.LoadPortletView("ICS/HoldsInfoPortlet/Holds_View.ascx"));
        }
    }
}
