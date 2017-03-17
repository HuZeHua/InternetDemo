
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示信息组数据访问实体。本体元素的分组
    /// </summary>
    public class InfoGroup : InfoGroupBase, IAggregateRoot<Guid>
    {
        private InfoGroup() { }

        public InfoGroup(Guid id) : base(id) { }

        public static InfoGroup Create(IInfoGroupCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new InfoGroup(input.Id.Value)
            {
                Code = input.Code,
                Description = input.Description,
                Name = input.Name,
                SortCode = input.SortCode,
                OntologyId = input.OntologyId
            };
        }

        public void Update(IInfoGroupUpdateIo input)
        {
            this.Code = input.Code;
            this.Description = input.Description;
            this.Name = input.Name;
            this.SortCode = input.SortCode;
        }
    }
}
