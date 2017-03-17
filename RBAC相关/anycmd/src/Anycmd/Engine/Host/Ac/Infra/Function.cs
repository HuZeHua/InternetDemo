
namespace Anycmd.Engine.Host.Ac.Infra
{
    using Engine.Ac.Functions;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示功能数据访问实体。
    /// <remarks>
    /// 而权限是“访问受控”的操作。
    /// </remarks>
    /// </summary>
    public class Function : FunctionBase, IAggregateRoot<Guid>
    {
        private Function() { }

        public Function(Guid id) : base(id) { }

        public static Function Create(IFunctionCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Function(input.Id.Value)
            {
                Code = input.Code,
                Description = input.Description,
                IsEnabled = input.IsEnabled,
                DeveloperId = input.DeveloperId,
                ResourceTypeId = input.ResourceTypeId,
                SortCode = input.SortCode,
                IsManaged = input.IsManaged
            };
        }

        public void Update(IFunctionUpdateIo input)
        {
            this.Code = input.Code;
            this.IsManaged = input.IsManaged;
            this.IsEnabled = input.IsEnabled;
            this.Description = input.Description;
            this.DeveloperId = input.DeveloperId;
            this.SortCode = input.SortCode;
        }
    }
}
