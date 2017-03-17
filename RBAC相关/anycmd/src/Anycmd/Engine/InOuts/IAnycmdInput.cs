
namespace Anycmd.Engine.InOuts
{
    using Messages;

    public interface IAnycmdInput : IInputModel
    {
        /// <summary>
        /// 超实体控制协议本体码。
        /// </summary>
        string HecpOntology { get; }

        /// <summary>
        /// 超实体控制协议动词码。
        /// </summary>
        string HecpVerb { get; }

        /// <summary>
        /// 将当前输入对象转化为命令对象。
        /// </summary>
        /// <param name="acSession">访问控制会话</param>
        /// <returns>命令对象</returns>
        IAnycmdCommand ToCommand(IAcSession acSession);
    }
}
