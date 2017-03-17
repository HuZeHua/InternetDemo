
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示话题数据访问实体。
    /// </summary>
    public class Topic : TopicBase, IAggregateRoot<Guid>
    {
        private Topic() { }

        public Topic(Guid id) : base(id) { }

        public static Topic Create(ITopicCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Topic(input.Id.Value)
            {
                Code = input.Code,
                Description = input.Description,
                IsAllowed = input.IsAllowed,
                Name = input.Name,
                OntologyId = input.OntologyId
            };
        }

        public void Update(ITopicUpdateIo input)
        {
            this.Code = input.Code;
            this.Name = input.Name;
            this.Description = input.Description;
        }
    }
}
