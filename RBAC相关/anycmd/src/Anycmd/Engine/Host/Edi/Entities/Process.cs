
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示进程数据访问实体
    /// </summary>
    public class Process : ProcessBase, IAggregateRoot<Guid>
    {
        private Process() { }

        public Process(Guid id) : base(id) { }

        public static Process Create(IProcessCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Process(input.Id.Value)
            {
                Type = input.Type,
                IsEnabled = input.IsEnabled,
                Description = input.Description,
                Name = input.Name,
                NetPort = input.NetPort,
                OntologyId = input.OntologyId,
                CatalogCode = input.CatalogCode
            };
        }

        public void Update(IProcessUpdateIo input)
        {
            this.Name = input.Name;
            this.IsEnabled = input.IsEnabled;
        }
    }
}
