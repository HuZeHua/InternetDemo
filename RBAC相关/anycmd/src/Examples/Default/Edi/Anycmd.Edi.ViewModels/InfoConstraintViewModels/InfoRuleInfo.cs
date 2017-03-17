
namespace Anycmd.Edi.ViewModels.InfoConstraintViewModels
{
    using Engine.Edi;
    using Exceptions;
    using Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public partial class InfoRuleInfo : Dictionary<string, object>
    {
        public InfoRuleInfo() { }

        public InfoRuleInfo(DicReader dic)
        {
            if (dic == null)
            {
                throw new ArgumentNullException("dic");
            }
            foreach (var item in dic)
            {
                this.Add(item.Key, item.Value);
            }
            InfoRuleState infoRule;
            if (!dic.AcDomain.NodeHost.InfoRules.TryGetInfoRule((Guid)this["Id"], out infoRule))
            {
                throw new GeneralException("�������Ϣ�����ʶ" + this["Id"]);
            }
            if (!this.ContainsKey("Name"))
            {
                this.Add("Name", infoRule.GetType().Name);
            }
            if (!this.ContainsKey("FullName"))
            {
                this.Add("FullName", infoRule.GetType().FullName);
            }
            if (!this.ContainsKey("Title"))
            {
                this.Add("Title", infoRule.InfoRule.Title);
            }
            if (!this.ContainsKey("Description"))
            {
                this.Add("Description", infoRule.InfoRule.Description);
            }
            if (!this.ContainsKey("Author"))
            {
                this.Add("Author", infoRule.InfoRule.Author);
            }
        }
    }
}
