using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;

namespace ArticleEdit.Models.AccessMgmt
{
    public class MyPrincipal
        : IPrincipal
    {
        public MyPrincipal(AppUser appUser)
        {
            Identity = appUser;
        }


        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}