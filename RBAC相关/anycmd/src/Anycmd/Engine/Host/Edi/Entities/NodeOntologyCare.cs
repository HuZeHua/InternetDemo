
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 节点关心本体。将节点和本体的关系视作实体。
    /// </summary>
    public class NodeOntologyCare : NodeOntologyCareBase, IAggregateRoot<Guid>
    {
        private NodeOntologyCare() { }

        public NodeOntologyCare(Guid id) : base(id) { }

        public static NodeOntologyCare Create(INodeOntologyCareCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new NodeOntologyCare(input.Id.Value)
            {
                NodeId = input.NodeId,
                OntologyId = input.OntologyId
            };
        }
    }
}
