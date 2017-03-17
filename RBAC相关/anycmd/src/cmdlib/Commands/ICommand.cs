namespace Anycmd.Commands
{
    using Bus;
    using Model;
    using System;

    /// <summary>
    /// 标记接口。表示该接口的实现类是命令。
    /// <remarks>命令是一种穿梭在总线上的消息。</remarks>
    /// </summary>
    public interface ICommand : IMessage, IEntity<Guid>
    {

    }
}
