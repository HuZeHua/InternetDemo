
namespace Anycmd.Engine.Ac.Groups
{
    using Messages;
    using System;

    public sealed class AddGroupCommand : AddEntityCommand<IGroupCreateIo>, IAnycmdCommand
    {
        public AddGroupCommand(IAcSession acSession, IGroupCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveGroupCommand : RemoveEntityCommand
    {
        public RemoveGroupCommand(IAcSession acSession, Guid groupId)
            : base(acSession, groupId)
        {

        }
    }

    public sealed class UpdateGroupCommand : UpdateEntityCommand<IGroupUpdateIo>, IAnycmdCommand
    {
        public UpdateGroupCommand(IAcSession acSession, IGroupUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
