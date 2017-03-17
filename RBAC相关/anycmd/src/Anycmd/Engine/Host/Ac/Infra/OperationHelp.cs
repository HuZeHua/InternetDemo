
namespace Anycmd.Engine.Host.Ac.Infra
{
    using Engine.Ac.Functions;
    using Model;
    using System;

    /// <summary>
    /// 表示系统操作帮助数据访问实体。
    /// </summary>
    public class OperationHelp : OperationHelpBase, IAggregateRoot<Guid>
    {
        private OperationHelp() { }

        public OperationHelp(Guid id)
            : base(id)
        {

        }
    }
}
