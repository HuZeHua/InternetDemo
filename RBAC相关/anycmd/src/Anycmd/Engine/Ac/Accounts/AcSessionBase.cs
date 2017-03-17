
namespace Anycmd.Engine.Ac.Accounts
{
    using Model;
    using System;

    /// <summary>
    /// 用户会话基类。
    /// </summary>
    public abstract class AcSessionBase : EntityBase, IAcSessionEntity
    {
        protected AcSessionBase() { }

        protected AcSessionBase(Guid id)
            : base(id)
        {
        }

        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string LoginName { get; set; }
        public Guid AccountId { get; set; }
        public int IsEnabled { get; set; }
        public string Description { get; set; }
    }
}
