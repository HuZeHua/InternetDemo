namespace Anycmd.Util
{
    /// <summary>
    /// 表示该类下放置的是anycmd framework的常数。
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 表示系统运行时所使用的不变常量和静态只读量。
        /// </summary>
        public static class ApplicationRuntime
        {
            /// <summary>
            /// 表示聚合根实体的默认版本号。
            /// </summary>
            public static readonly long DefaultVersion = 0;

            /// <summary>
            /// 表示聚合根实体的默认分支号。
            /// </summary>
            public static readonly long DefaultBranch = 0;

            /// <summary>
            /// IoC容器对象缓存键
            /// </summary>
            public static readonly string ContainerCacheKey = "_containerInstance";
        }
    }
}
