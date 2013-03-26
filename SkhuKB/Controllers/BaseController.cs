using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SkhuKB.Models;

namespace SkhuKB.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(SecurityVO.Username))
            {
                filterContext.HttpContext.User =
                    new CustomPrincipal(new CustomIdentity(SecurityVO.Username));
            }
            base.OnAuthorization(filterContext);
        }
    }
}
