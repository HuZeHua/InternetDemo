﻿
namespace Anycmd.Engine.Host.Ac.MessageHandlers
{
    using Commands;
    using Engine.Ac;
    using Engine.Ac.Accounts;
    using Exceptions;
    using Host;
    using Identity;
    using Repositories;
    using System;
    using System.Linq;

    public class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand>
    {
        private readonly IAcDomain _acDomain;

        public ChangePasswordCommandHandler(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
        }

        public override void Handle(ChangePasswordCommand command)
        {
            var accountRepository = _acDomain.RetrieveRequiredService<IRepository<Account, Guid>>();
            if (command.Input == null)
            {
                throw new InvalidOperationException("command.Input == null");
            }
            if (string.IsNullOrEmpty(command.Input.LoginName))
            {
                throw new ValidationException("登录名不能为空");
            }

            var entity = accountRepository.AsQueryable().FirstOrDefault(a => a.LoginName == command.Input.LoginName);
            if (entity == null)
            {
                throw new NotExistException("用户名" + command.Input.LoginName + "不存在");
            }
            bool loginNameChanged = !string.Equals(command.Input.LoginName, entity.LoginName);
            AccountState developer;
            if (_acDomain.SysUserSet.TryGetDevAccount(command.Input.LoginName, out developer) && !command.AcSession.IsDeveloper())
            {
                throw new ValidationException("对不起，您不能修改开发人员的密码。");
            }
            if (!command.AcSession.IsDeveloper() && "admin".Equals(entity.LoginName, StringComparison.OrdinalIgnoreCase))
            {
                throw new ValidationException("对不起，您无权修改admin账户的密码");
            }
            #region 更改登录名
            if (string.IsNullOrEmpty(command.Input.LoginName))
            {
                throw new ValidationException("登录名不能为空");
            }
            if (loginNameChanged)
            {
                entity.LoginName = command.Input.LoginName;
            }
            #endregion
            #region 更改密码
            if (string.IsNullOrEmpty(command.Input.OldPassword))
            {
                throw new ValidationException("旧密码不能为空");
            }
            if (string.IsNullOrEmpty(command.Input.NewPassword))
            {
                throw new ValidationException("新密码不能为空");
            }
            var passwordEncryptionService = _acDomain.RetrieveRequiredService<IPasswordEncryptionService>();
            var oldPwd = passwordEncryptionService.Encrypt(command.Input.OldPassword);
            if (!string.Equals(entity.Password, oldPwd))
            {
                throw new ValidationException("旧密码不正确");
            }
            var newPassword = passwordEncryptionService.Encrypt(command.Input.NewPassword);
            if (oldPwd != newPassword)
            {
                entity.Password = newPassword;
                entity.LastPasswordChangeOn = DateTime.Now;
                _acDomain.EventBus.Publish(new PasswordUpdatedEvent(command.AcSession, entity));
            }
            #endregion
            if (loginNameChanged)
            {
                _acDomain.EventBus.Publish(new LoginNameChangedEvent(command.AcSession, entity));
                if (_acDomain.SysUserSet.TryGetDevAccount(entity.Id, out developer))
                {
                    _acDomain.MessageDispatcher.DispatchMessage(new DeveloperUpdatedEvent(command.AcSession, entity));
                }
            }
            accountRepository.Update(entity);
            accountRepository.Context.Commit();
            _acDomain.EventBus.Commit();
        }
    }
}
