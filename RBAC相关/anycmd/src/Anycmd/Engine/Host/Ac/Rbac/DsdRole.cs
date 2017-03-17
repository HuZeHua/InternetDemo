
namespace Anycmd.Engine.Host.Ac.Rbac
{
    using Engine.Ac.Dsd;
    using Model;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 表示i动态职责分离角色数据访问实体。
    /// </summary>
    public class DsdRole : DsdRoleBase, IAggregateRoot<Guid>
    {
        private DsdRole() { }

        public DsdRole(Guid id)
            : base(id)
        {

        }

        public static DsdRole Create(IDsdRoleCreateIo input)
        {
            Debug.Assert(input.Id != null, "input.Id != null");
            return new DsdRole(input.Id.Value)
            {
                RoleId = input.RoleId,
                DsdSetId = input.DsdSetId
            };
        }
    }
}
