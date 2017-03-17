
namespace Anycmd.Commands
{
    using System;
    using Util;

    /// <summary>
    /// 表示命令的基类。
    /// </summary>
    [Serializable]
    public class Command : ICommand
    {
        private readonly Guid _id;
        #region Ctor
        /// <summary>
        /// 初始化一个 <c>Command</c> 类型的对象。
        /// </summary>
        public Command()
        {
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// 初始化一个 <c>Command</c> 类型的对象。
        /// </summary>
        /// <param name="id">命令标识。</param>
        public Command(Guid id)
        {
            _id = id;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 返回当前命令对象的哈希代码。
        /// </summary>
        /// <returns>为当前命令对象计算得到的哈希代码。</returns>
        public override int GetHashCode()
        {
            return ReflectionHelper.GetHashCode(_id.GetHashCode());
        }

        /// <summary>
        /// 返回一个 <see cref="System.Boolean"/> 类型的值以表名当前命令对象是否和给定的命令对象相同。
        /// </summary>
        /// <remarks>
        /// 命令对象是不可变对象，命令对象的相等只需引用相等或标识值相等。
        /// </remarks>
        /// <param name="obj">给定的对象。</param>
        /// <returns>如果给定的对象是 <see cref="Anycmd.Commands.ICommand"/> 类型且与当前命令相等。</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            var other = obj as ICommand;
            if ((object)other == (object)null)
                return false;
            return _id == other.Id;
        }
        #endregion

        #region IEntity Members
        /// <summary>
        /// 读取命令的标识。
        /// </summary>
        public Guid Id
        {
            get { return _id; }
        }

        #endregion
    }
}
