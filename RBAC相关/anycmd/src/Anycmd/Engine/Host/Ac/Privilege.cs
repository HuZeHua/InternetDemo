
namespace Anycmd.Engine.Host.Ac
{
    using Engine.Ac.Privileges;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示权限二元组数据访问实体。
    /// </summary>
    public class Privilege : PrivilegeBase, IAggregateRoot<Guid>
    {
        private Privilege() { }

        public Privilege(Guid id)
            : base(id)
        {

        }

        public static Privilege Create(IPrivilegeCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new Privilege(input.Id.Value)
            {
                SubjectType = input.SubjectType,
                SubjectInstanceId = input.SubjectInstanceId,
                ObjectType = input.ObjectType,
                ObjectInstanceId = input.ObjectInstanceId,
                AcContent = input.AcContent,
                AcContentType = input.AcContentType
            };
        }

        public void Update(IPrivilegeUpdateIo input)
        {
            this.AcContent = input.AcContent;
        }
    }
}
