﻿
namespace Anycmd.Engine.Host.Ac.Infra
{
    using Engine.Ac.UiViews;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示系统菜单数据访问实体。
    /// <remarks>
    /// <para>该菜单是首页左侧的导航树中树节点的抽象，不是面上的按钮，按钮是Button</para>
    /// </remarks>
    /// </summary>
    public class Menu : MenuBase, IAggregateRoot<Guid>
    {
        private Menu() { }

        public Menu(Guid id)
            : base(id)
        {

        }

        public static Menu Create(IMenuCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Menu(input.Id.Value)
            {
                AppSystemId = input.AppSystemId,
                Name = input.Name,
                Icon = input.Icon,
                Description = input.Description,
                ParentId = input.ParentId,
                SortCode = input.SortCode,
                Url = input.Url
            };
        }

        public void Update(IMenuUpdateIo input)
        {
            this.AppSystemId = input.AppSystemId;
            this.Description = input.Description;
            this.Icon = input.Icon;
            this.Name = input.Name;
            this.SortCode = input.SortCode;
            this.Url = input.Url;
        }
    }
}
