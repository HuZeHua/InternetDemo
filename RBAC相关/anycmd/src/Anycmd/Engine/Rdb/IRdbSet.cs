
namespace Anycmd.Engine.Rdb
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 表示该接口的实现类是关系数据库集。
    /// </summary>
    public interface IRdbSet : IEnumerable<RdbDescriptor>
    {
        /// <summary>
        /// 关系数据库表列数据集
        /// </summary>
        IDbTableColumnSet DbTableColumns { get; }
        /// <summary>
        /// 关系数据库表数据集
        /// </summary>
        IDbTableSet DbTables { get; }
        /// <summary>
        /// 关系数据库视图列数据集
        /// </summary>
        IDbViewColumnSet DbViewColumns { get; }
        /// <summary>
        /// 关系数据库视图数据集
        /// </summary>
        IDbViewSet DbViews { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool TryDb(Guid dbId, out RdbDescriptor db);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbId"></param>
        /// <returns></returns>
        bool ContainsDb(Guid dbId);
    }
}
