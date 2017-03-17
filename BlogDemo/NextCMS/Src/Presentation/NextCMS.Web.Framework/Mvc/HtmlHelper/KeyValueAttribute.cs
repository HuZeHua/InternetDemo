using System;

namespace XCode.Web.Core.Mvc.HtmlHelper
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class KeyValueAttribute : Attribute
    {
        private string _Text = "Text";
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _Value = "Value";
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _Disable = "Disable";
        public string Disable
        {
            get { return _Disable; }
            set { _Disable = value; }
        }

        public string DisplayProperty { get; set; }
    }
}