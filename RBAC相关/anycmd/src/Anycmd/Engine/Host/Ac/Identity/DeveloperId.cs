
namespace Anycmd.Engine.Host.Ac.Identity
{
    using Engine.Ac.Accounts;
    using Model;
    using System;

    /// <summary>
    /// 表示开发者数据访问实体。
    /// </summary>
    public class DeveloperId : EntityBase, IDeveloperId, IAggregateRoot<Guid>
    {
        private DeveloperId() { }

        public DeveloperId(Guid id)
            : base(id)
        {

        }
    }
}
