using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarManager.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AllowCallAttribute : Attribute
    {
        public string[] AllowActions { get; private set; }

        public AllowCallAttribute(params string[] allowActions)
        {
            this.AllowActions = allowActions;
        }
    }
}