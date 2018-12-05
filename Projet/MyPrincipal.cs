using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;


namespace Projet
{
    public class MyPrincipal : IPrincipal
    {
        private readonly MyIdentity MyIdentity;

        public MyPrincipal(MyIdentity _myIdentity)
        {
            MyIdentity = _myIdentity;
        }


        public IIdentity Identity
        {
            get { return MyIdentity; }
        }

        public bool IsInRole(string role)
        {
            return System.Web.Security.Roles.IsUserInRole(role);
        }
    }
}