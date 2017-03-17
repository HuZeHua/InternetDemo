
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 节点关心本体元素。节点和本体元素的多对多映射
    /// </summary>
    public class NodeElementCare : NodeElementCareBase, IAggregateRoot<Guid>
    {
        private NodeElementCare() { }

        public NodeElementCare(Guid id) : base(id) { }

        public static NodeElementCare Create(INodeElementCareCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new NodeElementCare(input.Id.Value)
            {
                ElementId = input.ElementId,
                NodeId = input.NodeId,
                IsInfoIdItem = input.IsInfoIdItem
            };
        }
    }
}
