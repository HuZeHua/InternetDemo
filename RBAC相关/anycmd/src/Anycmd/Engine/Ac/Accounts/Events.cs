
namespace Anycmd.Engine.Ac.Accounts
{
    using Events;
    using Host.Ac.Identity;

    public sealed class AccountAddedEvent : DomainEvent
    {
        public AccountAddedEvent(IAcSession acSession, AccountBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AccountLoginedEvent : DomainEvent
    {
        public AccountLoginedEvent(IAcSession acSession, IAccount source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AccountLogoutedEvent : DomainEvent
    {
        public AccountLogoutedEvent(IAcSession acSession, IAccount source)
            : base(acSession, source)
        {
        }
    }

    public sealed class AccountRemovedEvent : DomainEvent
    {
        public AccountRemovedEvent(IAcSession acSession, AccountBase source) : base(acSession, source) { }
    }

    public sealed class AccountUpdatedEvent : DomainEvent
    {
        public AccountUpdatedEvent(IAcSession acSession, AccountBase source) : base(acSession, source) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class DeveloperAddedEvent : DomainEvent
    {
        public DeveloperAddedEvent(IAcSession acSession, DeveloperId source) : base(acSession, source) { }

        internal DeveloperAddedEvent(IAcSession acSession, DeveloperId source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class DeveloperRemovedEvent : DomainEvent
    {
        public DeveloperRemovedEvent(IAcSession acSession, DeveloperId source) : base(acSession, source) { }

        internal DeveloperRemovedEvent(IAcSession acSession, DeveloperId source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class DeveloperUpdatedEvent : DomainEvent
    {
        public DeveloperUpdatedEvent(IAcSession acSession, AccountBase source) : base(acSession, source) { }
    }

    public sealed class LoginNameChangedEvent : DomainEvent
    {
        public LoginNameChangedEvent(IAcSession acSession, AccountBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PasswordUpdatedEvent : DomainEvent
    {
        public PasswordUpdatedEvent(IAcSession acSession, AccountBase source)
            : base(acSession, source)
        {
            this.Password = source.Password;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; private set; }
    }
}
