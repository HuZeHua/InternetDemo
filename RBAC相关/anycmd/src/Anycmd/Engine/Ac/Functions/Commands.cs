
namespace Anycmd.Engine.Ac.Functions
{
    using Commands;
    using Messages;
    using System;

    public sealed class AddFunctionCommand : AddEntityCommand<IFunctionCreateIo>, IAnycmdCommand
    {
        public AddFunctionCommand(IAcSession acSession, IFunctionCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveFunctionCommand : RemoveEntityCommand
    {
        public RemoveFunctionCommand(IAcSession acSession, Guid functionId)
            : base(acSession, functionId)
        {

        }
    }

    public sealed class SaveHelpCommand : Command, IAnycmdCommand
    {
        public SaveHelpCommand(IAcSession acSession, Guid functionId, string content, int? isEnabled)
        {
            this.AcSession = acSession;
            this.FunctionId = functionId;
            this.Content = content;
            this.IsEnabled = isEnabled;
        }

        public IAcSession AcSession { get; private set; }

        public Guid FunctionId { get; private set; }
        public string Content { get; private set; }
        public int? IsEnabled { get; private set; }
    }

    public sealed class UpdateFunctionCommand : UpdateEntityCommand<IFunctionUpdateIo>, IAnycmdCommand
    {
        public UpdateFunctionCommand(IAcSession acSession, IFunctionUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
