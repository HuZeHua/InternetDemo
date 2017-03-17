﻿
namespace Anycmd.Engine.Ac.Ssd
{
    using Exceptions;
    using Model;
    using System;

    /// <summary>
    /// 静态职责分离角色集基类。
    /// </summary>
    public abstract class SsdSetBase : EntityBase, ISsdSet
    {
        protected SsdSetBase() { }

        protected SsdSetBase(Guid id)
            : base(id)
        {

        }

        private string _name;

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
        public int SsdCard { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnabled { get; set; }

        public string Description { get; set; }
    }
}
