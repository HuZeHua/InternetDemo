
namespace Anycmd.Engine.Ac.AppSystems
{
    using Messages;
    using System;

    public sealed class AddAppSystemCommand : AddEntityCommand<IAppSystemCreateIo>, IAnycmdCommand
    {
        public AddAppSystemCommand(IAcSession acSession, IAppSystemCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public class RemoveAppSystemCommand : RemoveEntityCommand
    {
        public RemoveAppSystemCommand(IAcSession acSession, Guid appSystemId)
            : base(acSession, appSystemId)
        {

        }
    }

    public class UpdateAppSystemCommand : UpdateEntityCommand<IAppSystemUpdateIo>, IAnycmdCommand
    {
        public UpdateAppSystemCommand(IAcSession acSession, IAppSystemUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
