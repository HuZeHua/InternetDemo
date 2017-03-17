
namespace Anycmd.Engine.Host.Ac.Infra
{
    using Engine.Ac.UiViews;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示系统界面视图数据访问实体。
    /// </summary>
    public class UiView : UiViewBase, IAggregateRoot<Guid>
    {
        private UiView() { }

        public UiView(Guid id)
            : base(id)
        {

        }

        public static UiView Create(IUiViewCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new UiView(input.Id.Value)
            {
                Icon = input.Icon,
                Tooltip = input.Tooltip
            };
        }

        public void Update(IUiViewUpdateIo input)
        {
            this.Tooltip = input.Tooltip;
            this.Icon = input.Icon;
        }
    }
}
