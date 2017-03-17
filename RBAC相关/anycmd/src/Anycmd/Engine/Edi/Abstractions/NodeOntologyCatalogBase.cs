
namespace Anycmd.Engine.Edi.Abstractions
{
    using Model;
    using System;

    public class NodeOntologyCatalogBase : EntityBase, IAggregateRoot<Guid>, INodeOntologyCatalog
    {
        protected NodeOntologyCatalogBase() { }

        protected NodeOntologyCatalogBase(Guid id)
            : base(id)
        {

        }

        public Guid NodeId { get; set; }

        public Guid OntologyId { get; set; }

        public Guid CatalogId { get; set; }

        public string Actions { get; set; }
    }
}
