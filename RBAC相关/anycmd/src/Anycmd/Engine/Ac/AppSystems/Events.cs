﻿
namespace Anycmd.Engine.Ac.AppSystems
{
    using Events;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public sealed class AppSystemAddedEvent : DomainEvent
    {
        public AppSystemAddedEvent(IAcSession acSession, AppSystemBase source, IAppSystemCreateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal AppSystemAddedEvent(IAcSession acSession, AppSystemBase source, IAppSystemCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IAppSystemCreateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AppSystemRemovedEvent : DomainEvent
    {
        public AppSystemRemovedEvent(IAcSession acSession, AppSystemBase source)
            : base(acSession, source)
        {
        }

        internal AppSystemRemovedEvent(IAcSession acSession, AppSystemBase source, bool isPrivate)
            : this(acSession, source)
        {
            this.IsPrivate = isPrivate;
        }

        internal bool IsPrivate { get; private set; }
    }

    public sealed class AppSystemRemovingEvent : DomainEvent
    {
        public AppSystemRemovingEvent(IAcSession acSession, AppSystemBase source)
            : base(acSession, source)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class AppSystemUpdatedEvent : DomainEvent
    {
        public AppSystemUpdatedEvent(IAcSession acSession, AppSystemBase source, IAppSystemUpdateIo input)
            : base(acSession, source)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            this.Input = input;
        }

        internal AppSystemUpdatedEvent(IAcSession acSession, AppSystemBase source, IAppSystemUpdateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IAppSystemUpdateIo Input { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
