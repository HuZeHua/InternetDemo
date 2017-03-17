
namespace Anycmd.Engine.Ac.Roles
{
    using Messages;
    using System;

    public sealed class AddRoleCommand : AddEntityCommand<IRoleCreateIo>, IAnycmdCommand
    {
        public AddRoleCommand(IAcSession acSession, IRoleCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveRoleCommand : RemoveEntityCommand
    {
        public RemoveRoleCommand(IAcSession acSession, Guid roleId)
            : base(acSession, roleId)
        {

        }
    }

    public sealed class UpdateRoleCommand : UpdateEntityCommand<IRoleUpdateIo>, IAnycmdCommand
    {
        public UpdateRoleCommand(IAcSession acSession, IRoleUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
