using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace SkhuKB.Models
{
    public class CustomIdentity : System.Security.Principal.IIdentity
    {
        public string Name { get; private set; }

        public CustomIdentity(String name)
        {
            this.Name = name;
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.Name); }
        }
    }

    public class CustomPrincipal : System.Security.Principal.IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(CustomIdentity identity)
        {
            this.Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }

    public static class SecurityVO
    {
        static string userNameSessionVar = "username";
        public static string Username
        {
            get
            {
                if (HttpContext.Current == null) return string.Empty;
                var sessionVar = HttpContext.Current.Session[userNameSessionVar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[userNameSessionVar] = value; }
        }

    }
}
