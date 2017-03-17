﻿
namespace Anycmd.Engine.Host.Ac.Identity
{
    using Engine.Ac.Accounts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示账户数据访问实体。
    /// </summary>
    public class Account : AccountBase, IAggregateRoot<Guid>
    {
        private Account() { }

        public Account(Guid id)
            : base(id)
        {
        }

        public static Account Create(IAccountCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Account(input.Id.Value)
            {
                LoginName = input.LoginName,
                AllowEndTime = input.AllowEndTime,
                AllowStartTime = input.AllowStartTime,
                AuditState = input.AuditState,
                Description = input.Description,
                IsEnabled = input.IsEnabled,
                LockEndTime = input.LockEndTime,
                LockStartTime = input.LockStartTime,
                Code = input.Code,
                Email = input.Email,
                Mobile = input.Mobile,
                Name = input.Name,
                Nickname = input.Nickname,
                CatalogCode = input.CatalogCode,
                Qq = input.Qq,
                QuickQuery = input.QuickQuery,
                QuickQuery1 = input.QuickQuery1,
                QuickQuery2 = input.QuickQuery2,
                Telephone = input.Telephone,
                Question = input.QuickQuery,
                Password = input.Password,
                BlogUrl = input.BlogUrl
            };
        }

        public void Update(IAccountUpdateIo input)
        {
            this.AllowEndTime = input.AllowEndTime;
            this.AllowStartTime = input.AllowStartTime;
            this.AuditState = input.AuditState;
            this.Description = input.Description;
            this.IsEnabled = input.IsEnabled;
            this.LockEndTime = input.LockEndTime;
            this.LockStartTime = input.LockStartTime;
            this.Code = input.Code;
            this.Email = input.Email;
            this.Mobile = input.Mobile;
            this.Name = input.Name;
            this.Nickname = input.Nickname;
            this.CatalogCode = input.CatalogCode;
            this.Qq = input.Qq;
            this.QuickQuery = input.QuickQuery;
            this.QuickQuery1 = input.QuickQuery1;
            this.QuickQuery2 = input.QuickQuery2;
            this.Telephone = input.Telephone;
            this.BlogUrl = input.BlogUrl;
        }
    }
}
