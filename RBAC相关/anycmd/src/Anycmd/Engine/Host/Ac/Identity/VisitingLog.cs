
namespace Anycmd.Engine.Host.Ac.Identity
{
    using Logging;
    using Model;
    using System;

    /// <summary>
    /// 表示来访日志数据访问实体。
    /// </summary>
    public class VisitingLog : VisitingLogBase, IAggregateRoot<Guid>
    {
        private VisitingLog() { }

        private VisitingLog(Guid id) : base(id) { }
    }
}
