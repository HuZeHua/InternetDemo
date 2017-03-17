﻿
namespace Anycmd.Engine.Ac.Privileges
{
    /// <summary>
    /// 表示访问控制记录类型。
    /// <remarks>
    /// 该枚举成员的命名模式是SubjectType + ObjectType。
    /// 注意：存储层存储的是枚举值的名称，不使用枚举的数字。
    /// </remarks>
    /// </summary>
    public enum AcRecordType
    {
        Undefined,
        /// <summary>
        /// 主体是账户，客体也是账户。
        /// </summary>
        AccountAccount = 20,
        /// <summary>
        /// 主体是账户，客体是目录。
        /// </summary>
        AccountCatalog,
        AccountRole,
        AccountGroup,
        AccountFunction,
        AccountMenu,
        AccountAppSystem,
        AccountPrivilege,
        CatalogCatalog,
        CatalogRole,
        CatalogGroup,
        CatalogFunction,
        CatalogMenu,
        CatalogAppSystem,
        CatalogPrivilege,
        RoleRole,
        RoleGroup,
        RoleFunction,
        RoleMenu,
        RoleAppSystem,
        RolePrivilege,
        GroupGroup,
        GroupFunction,
        GroupMenu,
        GroupAppSystem,
        GroupPrivilege,
        FunctionFunction,
        FunctionMenu,
        FunctionAppSystem,
        FunctionPrivilege,
        MenuMenu,
        MenuAppSystem,
        MenuPrivilege,
        AppSystemAppSystem,
        AppSystemPrivilege,
        PrivilegePrivilege
    }
}
