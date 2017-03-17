
namespace Anycmd.Engine.Ac.UiViews
{
    using Events;
    using Messages;

    /// <summary>
    /// 
    /// </summary>
    public sealed class ButtonRemovedEvent : DomainEvent
    {
        public ButtonRemovedEvent(IAcSession acSession, ButtonBase source)
            : base(acSession, source)
        {
        }

        internal ButtonRemovedEvent(IAcSession acSession, ButtonBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class ButtonRemovingEvent : DomainEvent
    {
        public ButtonRemovingEvent(IAcSession acSession, ButtonBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ButtonUpdatedEvent : DomainEvent
    {
        public ButtonUpdatedEvent(IAcSession acSession, ButtonBase source, IButtonUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal ButtonUpdatedEvent(IAcSession acSession, ButtonBase source, IButtonUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IButtonUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class MenuAddedEvent : EntityAddedEvent<IMenuCreateIo>
    {
        public MenuAddedEvent(IAcSession acSession, MenuBase source, IMenuCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal MenuAddedEvent(IAcSession acSession, MenuBase source, IMenuCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class MenuRemovedEvent : DomainEvent
    {
        public MenuRemovedEvent(IAcSession acSession, MenuBase source)
            : base(acSession, source)
        {
        }

        internal MenuRemovedEvent(IAcSession acSession, MenuBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class MenuRemovingEvent : DomainEvent
    {
        public MenuRemovingEvent(IAcSession acSession, MenuBase source)
            : base(acSession, source)
        {
        }
    }

    public sealed class MenuUpdatedEvent : DomainEvent
    {
        public MenuUpdatedEvent(IAcSession acSession, MenuBase source, IMenuUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal MenuUpdatedEvent(IAcSession acSession, MenuBase source, IMenuUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IMenuUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    public sealed class UiViewAddedEvent : EntityAddedEvent<IUiViewCreateIo>
    {
        public UiViewAddedEvent(IAcSession acSession, UiViewBase source, IUiViewCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal UiViewAddedEvent(IAcSession acSession, UiViewBase source, IUiViewCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class UiViewButtonAddedEvent : EntityAddedEvent<IUiViewButtonCreateIo>
    {
        public UiViewButtonAddedEvent(IAcSession acSession, UiViewButtonBase source, IUiViewButtonCreateIo input)
            : base(acSession, source, input)
        {
        }

        internal UiViewButtonAddedEvent(IAcSession acSession, UiViewButtonBase source, IUiViewButtonCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class UiViewButtonRemovedEvent : DomainEvent
    {
        public UiViewButtonRemovedEvent(IAcSession acSession, UiViewButtonBase source)
            : base(acSession, source)
        {
        }

        internal UiViewButtonRemovedEvent(IAcSession acSession, UiViewButtonBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class UiViewButtonUpdatedEvent : DomainEvent
    {
        public UiViewButtonUpdatedEvent(IAcSession acSession, UiViewButtonBase source, IUiViewButtonUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal UiViewButtonUpdatedEvent(IAcSession acSession, UiViewButtonBase source, IUiViewButtonUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IUiViewButtonUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class UiViewRemovedEvent : DomainEvent
    {
        public UiViewRemovedEvent(IAcSession acSession, UiViewBase source)
            : base(acSession, source)
        {
        }

        internal UiViewRemovedEvent(IAcSession acSession, UiViewBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class UiViewRemovingEvent : DomainEvent
    {
        public UiViewRemovingEvent(IAcSession acSession, UiViewBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class UiViewUpdatedEvent : DomainEvent
    {
        public UiViewUpdatedEvent(IAcSession acSession, UiViewBase source, IUiViewUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new System.ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal UiViewUpdatedEvent(IAcSession acSession, UiViewBase source, IUiViewUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IUiViewUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
