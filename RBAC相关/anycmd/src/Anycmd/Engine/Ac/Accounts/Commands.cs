
namespace Anycmd.Engine.Ac.Accounts
{
    using Commands;
    using Messages;
    using System;

    public sealed class AddAccountCommand : AddEntityCommand<IAccountCreateIo>, IAnycmdCommand
    {
        public AddAccountCommand(IAcSession acSession, IAccountCreateIo input)
            : base(acSession, input)
        {
        }
    }

    public sealed class AddDeveloperCommand : Command, IAnycmdCommand
    {
        public AddDeveloperCommand(IAcSession acSession, Guid accountId)
        {
            this.AcSession = acSession;
            this.AccountId = accountId;
        }

        public IAcSession AcSession { get; private set; }
        public Guid AccountId { get; private set; }
    }

    public sealed class AddVisitingLogCommand : Command, IAnycmdCommand
    {
        public AddVisitingLogCommand(IAcSession acSession)
        {
            this.AcSession = acSession;
        }

        public IAcSession AcSession { get; private set; }

        /// <summary>
        /// 账户表示
        /// </summary>
        public Guid? AccountId { get; set; }
        /// <summary>
        /// Gets or sets 登录名.
        /// </summary>
        /// <value>登录名.</value>
        public string LoginName { get; set; }
        /// <summary>
        /// Gets or sets the visit on.
        /// </summary>
        /// <value>The visit on.</value>
        public DateTime VisitOn { get; set; }
        /// <summary>
        /// Gets or sets the visited on.
        /// </summary>
        /// <value>The visited on.</value>
        public DateTime? VisitedOn { get; set; }
        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string IpAddress { get; set; }
        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>The state code.</value>
        public int StateCode { get; set; }
        /// <summary>
        /// Gets or sets the reason phrase.
        /// </summary>
        /// <value>The reason phrase.</value>
        public string ReasonPhrase { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }

    public sealed class AssignPasswordCommand : Command, IAnycmdCommand
    {
        public AssignPasswordCommand(IAcSession acSession, IPasswordAssignIo input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            this.AcSession = acSession;
            this.Input = input;
        }

        public IPasswordAssignIo Input { get; private set; }

        public IAcSession AcSession { get; private set; }
    }

    public sealed class ChangePasswordCommand : Command, IAnycmdCommand
    {
        public ChangePasswordCommand(IAcSession acSession, IPasswordChangeIo input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            this.AcSession = acSession;
            this.Input = input;
        }

        public IPasswordChangeIo Input { get; private set; }

        public IAcSession AcSession { get; private set; }
    }

    public sealed class DisableAccountCommand : Command, IAnycmdCommand
    {
        public DisableAccountCommand(IAcSession acSession, Guid accountId)
        {
            this.AcSession = acSession;
            this.AccountId = accountId;
        }

        public IAcSession AcSession { get; private set; }

        public Guid AccountId { get; private set; }
    }

    public sealed class EnableAccountCommand : Command
    {
        public EnableAccountCommand(IAcSession acSession, Guid accountId)
        {
            this.AcSession = acSession;
            this.AccountId = accountId;
        }

        public IAcSession AcSession { get; private set; }

        public Guid AccountId { get; private set; }
    }

    public sealed class RemoveAccountCommand : RemoveEntityCommand
    {
        public RemoveAccountCommand(IAcSession acSession, Guid accountId)
            : base(acSession, accountId)
        {

        }
    }

    public sealed class RemoveDeveloperCommand : Command, IAnycmdCommand
    {
        public RemoveDeveloperCommand(IAcSession acSession, Guid accountId)
        {
            this.AcSession = acSession;
            this.AccountId = accountId;
        }

        public IAcSession AcSession { get; private set; }

        public Guid AccountId { get; private set; }
    }

    public sealed class UpdateAccountCommand : UpdateEntityCommand<IAccountUpdateIo>, IAnycmdCommand
    {
        public UpdateAccountCommand(IAcSession acSession, IAccountUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
