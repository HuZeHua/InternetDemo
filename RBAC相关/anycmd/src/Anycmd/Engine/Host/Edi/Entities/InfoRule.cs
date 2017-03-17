
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Info;
    using Model;
    using System;

    /// <summary>
    /// 表示信息规则数据访问实体。信息项验证器插件库
    /// </summary>
    public sealed class InfoRule : InfoRuleEntityBase, IAggregateRoot<Guid>
    {
        private InfoRule() { }

        public InfoRule(Guid id)
            : base(id)
        {

        }
    }
}
