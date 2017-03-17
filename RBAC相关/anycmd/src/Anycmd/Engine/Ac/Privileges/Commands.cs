
namespace Anycmd.Engine.Ac.Privileges
{
    using Messages;
    using System;

    public sealed class AddPrivilegeCommand : AddEntityCommand<IPrivilegeCreateIo>, IAnycmdCommand
    {
        public AddPrivilegeCommand(IAcSession acSession, IPrivilegeCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdatePrivilegeCommand : UpdateEntityCommand<IPrivilegeUpdateIo>, IAnycmdCommand
    {
        public UpdatePrivilegeCommand(IAcSession acSession, IPrivilegeUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemovePrivilegeCommand : RemoveEntityCommand
    {
        public RemovePrivilegeCommand(IAcSession acSession, Guid privilegeBigramId)
            : base(acSession, privilegeBigramId)
        {

        }
    }
}
