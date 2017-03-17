
namespace Anycmd.Engine.Ac.UiViews
{
    using Messages;
    using System;

    public sealed class AddButtonCommand : AddEntityCommand<IButtonCreateIo>, IAnycmdCommand
    {
        public AddButtonCommand(IAcSession acSession, IButtonCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddMenuCommand : AddEntityCommand<IMenuCreateIo>, IAnycmdCommand
    {
        public AddMenuCommand(IAcSession acSession, IMenuCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddUiViewButtonCommand : AddEntityCommand<IUiViewButtonCreateIo>, IAnycmdCommand
    {
        public AddUiViewButtonCommand(IAcSession acSession, IUiViewButtonCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddUiViewCommand : AddEntityCommand<IUiViewCreateIo>, IAnycmdCommand
    {
        public AddUiViewCommand(IAcSession acSession, IUiViewCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class RemoveButtonCommand : RemoveEntityCommand, IAnycmdCommand
    {
        public RemoveButtonCommand(IAcSession acSession, Guid buttonId)
            : base(acSession, buttonId)
        {

        }
    }

    public sealed class RemoveMenuCommand : RemoveEntityCommand
    {
        public RemoveMenuCommand(IAcSession acSession, Guid menuId)
            : base(acSession, menuId)
        {

        }
    }

    public sealed class RemoveUiViewButtonCommand : RemoveEntityCommand
    {
        public RemoveUiViewButtonCommand(IAcSession acSession, Guid viewButtonId)
            : base(acSession, viewButtonId)
        {

        }
    }

    public sealed class RemoveUiViewCommand : RemoveEntityCommand
    {
        public RemoveUiViewCommand(IAcSession acSession, Guid viewId)
            : base(acSession, viewId)
        {

        }
    }


    public sealed class UpdateButtonCommand : UpdateEntityCommand<IButtonUpdateIo>, IAnycmdCommand
    {
        public UpdateButtonCommand(IAcSession acSession, IButtonUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateMenuCommand : UpdateEntityCommand<IMenuUpdateIo>, IAnycmdCommand
    {
        public UpdateMenuCommand(IAcSession acSession, IMenuUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateUiViewButtonCommand : UpdateEntityCommand<IUiViewButtonUpdateIo>, IAnycmdCommand
    {
        public UpdateUiViewButtonCommand(IAcSession acSession, IUiViewButtonUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateUiViewCommand : UpdateEntityCommand<IUiViewUpdateIo>, IAnycmdCommand
    {
        public UpdateUiViewCommand(IAcSession acSession, IUiViewUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
