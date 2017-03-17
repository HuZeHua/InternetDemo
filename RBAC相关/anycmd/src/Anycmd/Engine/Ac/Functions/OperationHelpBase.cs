
namespace Anycmd.Engine.Ac.Functions
{
    using Model;
    using System;

    /// <summary>
    /// 操作帮助基类。<see cref="IOperationHelp"/>
    /// </summary>
    public abstract class OperationHelpBase : EntityBase, IOperationHelp
    {
        protected OperationHelpBase() { }

        protected OperationHelpBase(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsEnabled { get; set; }
    }
}
