
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Engine.Edi.InOuts;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// ��ʾ�鵵��¼���ݷ���ʵ�塣
    /// </summary>
    public class Archive : ArchiveBase, IAggregateRoot<Guid>
    {
        private Archive() { }

        public Archive(Guid id) : base(id) { }

        public static Archive Create(IArchiveCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Archive(input.Id.Value)
            {
                ArchiveOn = DateTime.Now,
                DataSource = string.Empty,
                Description = input.Description,
                FilePath = string.Empty,
                Password = string.Empty,
                Title = input.Title,
                RdbmsType = input.RdbmsType,
                OntologyId = input.OntologyId,
                UserId = string.Empty
            };
        }

        public void Update(IArchiveUpdateIo input)
        {
            this.Description = input.Description;
            this.Title = input.Title;
        }
    }
}
