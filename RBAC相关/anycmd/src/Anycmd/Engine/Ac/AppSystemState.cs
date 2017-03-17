﻿
namespace Anycmd.Engine.Ac
{
    using AppSystems;
    using Exceptions;
    using Model;
    using Privileges;
    using System;
    using Util;

    /// <summary>
    /// 表示应用系统业务实体类型。
    /// </summary>
    public sealed class AppSystemState : StateObject<AppSystemState>, IAppSystem, IAcElement
    {
        public static readonly AppSystemState Empty = new AppSystemState(Guid.Empty)
        {
            _code = string.Empty,
            _createOn = SystemTime.MinDate,
            _icon = string.Empty,
            _isEnabled = 0,
            _name = string.Empty,
            _principalId = Guid.Empty,
            _sortCode = 0,
            _ssoAuthAddress = string.Empty
        };

        private string _code;
        private string _name;
        private int _sortCode;
        private Guid _principalId;
        private int _isEnabled;
        private string _ssoAuthAddress;
        private string _icon;
        private DateTime? _createOn;
        private IAcDomain _acDomain;

        private AppSystemState(Guid id) : base(id) { }

        public static AppSystemState Create(IAcDomain acDomain, AppSystemBase appSystem)
        {
            if (acDomain == null)
            {
                throw new ArgumentNullException("acDomain");
            }
            if (appSystem == null)
            {
                throw new ArgumentNullException("appSystem");
            }
            return new AppSystemState(appSystem.Id)
            {
                _acDomain = acDomain,
                _createOn = appSystem.CreateOn
            }.InternalModify(appSystem);
        }

        internal AppSystemState InternalModify(AppSystemBase appSystem)
        {
            if (appSystem == null)
            {
                throw new ArgumentNullException("appSystem");
            }
            AccountState principal;
            if (!_acDomain.SysUserSet.TryGetDevAccount(appSystem.PrincipalId, out principal))
            {
                throw new GeneralException("意外的应用系统负责人标识" + appSystem.PrincipalId);
            }
            _code = appSystem.Code;
            _name = appSystem.Name;
            _sortCode = appSystem.SortCode;
            _principalId = appSystem.PrincipalId;
            _isEnabled = appSystem.IsEnabled;
            _ssoAuthAddress = appSystem.SsoAuthAddress;
            _icon = appSystem.Icon;

            return this;
        }

        public AcElementType AcElementType
        {
            get { return AcElementType.AppSystem; }
        }

        public string Code
        {
            get { return _code; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int SortCode
        {
            get { return _sortCode; }
        }

        public Guid PrincipalId
        {
            get { return _principalId; }
        }

        public int IsEnabled
        {
            get { return _isEnabled; }
        }

        public string SsoAuthAddress
        {
            get { return _ssoAuthAddress; }
        }

        public string Icon
        {
            get { return _icon; }
        }

        public DateTime? CreateOn
        {
            get { return _createOn; }
        }

        public override string ToString()
        {
            return string.Format(
@"{{
    Id:'{0}',
    Code:'{1}',
    Name:'{2}',
    SortCode:{3},
    PrincipalId:'{4}',
    IsEnabled:{5},
    SsoAuthAddress:'{6}',
    Icon:'{7}',
    CreateOn:'{8}'
}}", Id, Code, Name, SortCode, PrincipalId, IsEnabled, SsoAuthAddress, Icon, CreateOn);
        }

        protected override bool DoEquals(AppSystemState other)
        {
            return
                Id == other.Id &&
                Code == other.Code &&
                Name == other.Name &&
                SortCode == other.SortCode &&
                IsEnabled == other.IsEnabled &&
                SsoAuthAddress == other.SsoAuthAddress &&
                Icon == other.Icon;
        }
    }
}
