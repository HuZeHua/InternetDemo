
namespace Anycmd.Engine.Host.Ac.Rbac
{
    using Engine.Ac.Accounts;
    using Model;
    using System;

    /// <summary>
    /// 表示用户会话数据访问实体。
    /// </summary>
    public class AcSession : AcSessionBase, IAggregateRoot<Guid>
    {
        private AcSession() { }

        public AcSession(Guid id)
            : base(id)
        {

        }
    }
}
