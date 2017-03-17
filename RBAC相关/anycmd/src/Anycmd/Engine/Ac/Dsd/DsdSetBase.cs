﻿
namespace Anycmd.Engine.Ac.Dsd
{
    using Exceptions;
    using Model;
    using System;

    /// <summary>
    /// 动态职责分离角色集基类。
    /// </summary>
    public abstract class DsdSetBase : EntityBase, IDsdSet
    {
        private string _name;

        protected DsdSetBase() { }

        protected DsdSetBase(Guid id)
            : base(id)
        {
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ValidationException("名称是必须的");
                }
                _name = value;
            }
        }

        /// <summary>
        /// 阀值
        /// </summary>
        public int DsdCard { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }

        public string Description { get; set; }
    }
}
