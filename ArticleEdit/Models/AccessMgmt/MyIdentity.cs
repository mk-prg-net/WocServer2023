using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Principal;

namespace ArticleEdit.Models.AccessMgmt
{
    public class MyIdentity
        : IIdentity
    {


        public string Name => throw new NotImplementedException();

        public string AuthenticationType => throw new NotImplementedException();

        public bool IsAuthenticated => true;
    }
}