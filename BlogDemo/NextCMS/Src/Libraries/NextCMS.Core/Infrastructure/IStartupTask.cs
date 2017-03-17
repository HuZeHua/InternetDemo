﻿namespace XCode.Core.Infrastructure
{
    /// <summary>
    /// 系统启动时的执行任务
    /// </summary>
    public interface IStartupTask
    {
        void Execute();

        int Order { get; }
    }
}
