
namespace Anycmd.Engine.Ac.Ssd
{
    using Messages;
    using System;

    public sealed class AddSsdRoleCommand : AddEntityCommand<ISsdRoleCreateIo>, IAnycmdCommand
    {
        public AddSsdRoleCommand(IAcSession acSession, ISsdRoleCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddSsdSetCommand : AddEntityCommand<ISsdSetCreateIo>, IAnycmdCommand
    {
        public AddSsdSetCommand(IAcSession acSession, ISsdSetCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveSsdRoleCommand : RemoveEntityCommand
    {
        public RemoveSsdRoleCommand(IAcSession acSession, Guid ssdRoleId)
            : base(acSession, ssdRoleId)
        {

        }
    }

    public sealed class RemoveSsdSetCommand : RemoveEntityCommand
    {
        public RemoveSsdSetCommand(IAcSession acSession, Guid ssdSetId)
            : base(acSession, ssdSetId)
        {

        }
    }

    public sealed class UpdateSsdSetCommand : UpdateEntityCommand<ISsdSetUpdateIo>, IAnycmdCommand
    {
        public UpdateSsdSetCommand(IAcSession acSession, ISsdSetUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
