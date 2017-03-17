﻿
namespace Anycmd
{
    using Bus;
    using Engine.Ac.Accounts;
    using Engine.Ac.AppSystems;
    using Engine.Ac.Catalogs;
    using Engine.Ac.Dsd;
    using Engine.Ac.EntityTypes;
    using Engine.Ac.Functions;
    using Engine.Ac.Groups;
    using Engine.Ac.Privileges;
    using Engine.Ac.Roles;
    using Engine.Ac.Ssd;
    using Engine.Ac.UiViews;
    using Engine.Host;
    using Engine.Host.Edi;
    using Engine.Rdb;
    using IdGenerators;
    using Logging;
    using Serialization;
    using System;
    using System.ComponentModel.Design;

    /// <summary>
    /// 它是访问所有系统实体的入口。它确立了一个边界，如果进程中实例化了多个AcDomain实例的话。
    /// </summary>
    public interface IAcDomain : IServiceContainer, IDisposable
    {
        /// <summary>
        /// 宿主标识
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// 宿主名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 标识生成器
        /// </summary>
        IIdGenerator IdGenerator { get; }
        /// <summary>
        /// 序列标识生成器
        /// </summary>
        ISequenceIdGenerator SequenceIdGenerator { get; }
        /// <summary>
        /// 消息分发器
        /// </summary>
        IMessageDispatcher MessageDispatcher { get; }
        /// <summary>
        /// Json序列化器
        /// </summary>
        IObjectSerializer JsonSerializer { get; }
        /// <summary>
        /// 命令总线
        /// </summary>
        ICommandBus CommandBus { get; }
        /// <summary>
        /// 事件总线
        /// </summary>
        IEventBus EventBus { get; }
        /// <summary>
        /// 数据交换宿主
        /// </summary>
        INodeHost NodeHost { get; }
        /// <summary>
        /// 应用配置
        /// </summary>
        IAppConfig Config { get; }

        #region 数据集
        /// <summary>
        /// 应用系统数据集
        /// </summary>
        IAppSystemSet AppSystemSet { get; }
        /// <summary>
        /// 按钮数据集
        /// </summary>
        IButtonSet ButtonSet { get; }
        /// <summary>
        /// 关系数据库数据集
        /// </summary>
        IRdbSet RdbSet { get; }
        /// <summary>
        /// 实体类型数据集
        /// </summary>
        IEntityTypeSet EntityTypeSet { get; }
        /// <summary>
        /// 功能数据集
        /// </summary>
        IFunctionSet FunctionSet { get; }
        /// <summary>
        /// 资源组数据集
        /// </summary>
        IGroupSet GroupSet { get; }
        /// <summary>
        /// 菜单数据集
        /// </summary>
        IMenuSet MenuSet { get; }
        /// <summary>
        /// 目录数据集
        /// </summary>
        ICatalogSet CatalogSet { get; }
        /// <summary>
        /// 界面视图数据集
        /// </summary>
        IUiViewSet UiViewSet { get; }
        /// <summary>
        /// 权限数据集
        /// </summary>
        IPrivilegeSet PrivilegeSet { get; }
        /// <summary>
        /// 角色数据集
        /// </summary>
        IRoleSet RoleSet { get; }
        /// <summary>
        /// 静态职责分离角色集数据集
        /// </summary>
        ISsdSetSet SsdSetSet { get; }
        /// <summary>
        /// 动态职责分离角色集数据集
        /// </summary>
        IDsdSetSet DsdSetSet { get; }
        /// <summary>
        /// 系统用户数据集
        /// </summary>
        ISysUserSet SysUserSet { get; }
        #endregion

        /// <summary>
        /// 根据插件类型获取插件目录地址
        /// </summary>
        /// <param name="pluginType"></param>
        /// <returns></returns>
        string GetPluginBaseDirectory(PluginType pluginType);

        ILoggingService LoggingService { get; }
    }
}
