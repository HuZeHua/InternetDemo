
namespace Anycmd.Engine.Ac.EntityTypes
{

    using Commands;
    using Messages;
    using System;

    public sealed class AddCommonPropertiesCommand : Command, IAnycmdCommand
    {
        public AddCommonPropertiesCommand(IAcSession acSession, Guid entityTypeId)
        {
            this.AcSession = acSession;
            this.EntityTypeId = entityTypeId;
        }

        public IAcSession AcSession { get; private set; }

        public Guid EntityTypeId { get; private set; }
    }

    public sealed class AddEntityTypeCommand : AddEntityCommand<IEntityTypeCreateIo>, IAnycmdCommand
    {
        public AddEntityTypeCommand(IAcSession acSession, IEntityTypeCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddPropertyCommand : AddEntityCommand<IPropertyCreateIo>, IAnycmdCommand
    {
        public AddPropertyCommand(IAcSession acSession, IPropertyCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveEntityTypeCommand : RemoveEntityCommand
    {
        public RemoveEntityTypeCommand(IAcSession acSession, Guid entityTypeId)
            : base(acSession, entityTypeId)
        {

        }
    }


    public sealed class RemovePropertyCommand : RemoveEntityCommand
    {
        public RemovePropertyCommand(IAcSession acSession, Guid propertyId)
            : base(acSession, propertyId)
        {

        }
    }

    public sealed class UpdateEntityTypeCommand : UpdateEntityCommand<IEntityTypeUpdateIo>, IAnycmdCommand
    {
        public UpdateEntityTypeCommand(IAcSession acSession, IEntityTypeUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdatePropertyCommand : UpdateEntityCommand<IPropertyUpdateIo>, IAnycmdCommand
    {
        public UpdatePropertyCommand(IAcSession acSession, IPropertyUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
