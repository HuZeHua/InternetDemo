
namespace Anycmd.Engine.Ac.Catalogs
{
    using Messages;
    using System;

    public sealed class AddCatalogCommand : AddEntityCommand<ICatalogCreateIo>, IAnycmdCommand
    {
        public AddCatalogCommand(IAcSession acSession, ICatalogCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveCatalogCommand : RemoveEntityCommand
    {
        public RemoveCatalogCommand(IAcSession acSession, Guid catalogId)
            : base(acSession, catalogId)
        {

        }
    }

    public sealed class UpdateCatalogCommand : UpdateEntityCommand<ICatalogUpdateIo>, IAnycmdCommand
    {
        public UpdateCatalogCommand(IAcSession acSession, ICatalogUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
