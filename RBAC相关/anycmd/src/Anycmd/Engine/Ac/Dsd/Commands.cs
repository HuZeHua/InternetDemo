
namespace Anycmd.Engine.Ac.Dsd
{
    using Messages;
    using System;

    public sealed class AddDsdRoleCommand : AddEntityCommand<IDsdRoleCreateIo>, IAnycmdCommand
    {
        public AddDsdRoleCommand(IAcSession acSession, IDsdRoleCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddDsdSetCommand : AddEntityCommand<IDsdSetCreateIo>, IAnycmdCommand
    {
        public AddDsdSetCommand(IAcSession acSession, IDsdSetCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveDsdRoleCommand : RemoveEntityCommand
    {
        public RemoveDsdRoleCommand(IAcSession acSession, Guid dsdRoleId)
            : base(acSession, dsdRoleId)
        {

        }
    }

    public sealed class RemoveDsdSetCommand : RemoveEntityCommand
    {
        public RemoveDsdSetCommand(IAcSession acSession, Guid dsdSetId)
            : base(acSession, dsdSetId)
        {

        }
    }

    public sealed class UpdateDsdSetCommand : UpdateEntityCommand<IDsdSetUpdateIo>, IAnycmdCommand
    {
        public UpdateDsdSetCommand(IAcSession acSession, IDsdSetUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
