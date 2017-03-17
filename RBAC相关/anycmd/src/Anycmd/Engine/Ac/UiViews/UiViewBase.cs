
namespace Anycmd.Engine.Ac.UiViews
{
    using Model;
    using System;

    /// <summary>
    /// 界面视图基类<see cref="IUiView"/>
    /// </summary>
    public abstract class UiViewBase : EntityBase, IUiView
    {
        protected UiViewBase()
        {

        }

        protected UiViewBase(Guid id)
            : base(id)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tooltip { get; set; }
    }
}
