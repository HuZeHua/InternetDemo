
namespace Anycmd
{
    using System;

    /// <summary>
    /// 表示一个特性，在该特性中指明标定的类或方法的作者。如果您熟悉了或改了原作者的代码，留下你的名字。系统异常日志记录的时候使用这个名字关联上作者。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false)]
    public class ByAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="developerCode">开发人员编码</param>
        public ByAttribute(string developerCode)
        {
            this.DeveloperCode = developerCode;
        }

        /// <summary>
        /// 开发人员编码
        /// </summary>
        public string DeveloperCode { get; set; }
    }

    /// <summary>
    /// 表示该接口的实现类是标识特性类型。
    /// <para>
    /// 运行时托管资源标识。运行时托管资源是什么？运行时托管资源可以是：Method、Class、Property。
    /// 权限系统干了什么？给出一套方法，将系统中的所有功能标识出来，组织起来，托管起来，将所有的数据组织起来标识出来托管起来，
    /// 然后提供一个简单的唯一的接口，这个接口的一端是应用系统一端是权限引擎。权限引擎所回答的只是：谁是否对某资源具有实施
    /// 某个动作（运动、计算）的权限。返回的结果只有：有、没有、权限引擎异常了。
    /// </para>
    /// <remarks>
    /// 注意这个标识不是存储层的主键标识。这个标识在存储层对应名称为“Guid”的字段而不是对应名称为“Id”的字段。
    /// 将资源标识出来从而进行持久跟踪一般有两种方法：1通过命名约定进行一一对应；2构建独立的标识系统。
    /// 推荐使用第二种方案，因为命名约定容易变化，而对于人类来说不具有意义的guid或纯数字是没有理由变更的。
    /// </remarks>
    /// </summary>
    public interface IdAttribute
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class GuidAttribute : Attribute, IdAttribute
    {
        public Guid Value { get; private set; }

        public GuidAttribute(string value)
        {
            this.Value = new Guid(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Int32IdAttribute : Attribute, IdAttribute
    {
        public Int32 Value { get; private set; }

        public Int32IdAttribute(Int32 value)
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Int64IdAttribute : Attribute, IdAttribute
    {
        public Int64 Value { get; private set; }

        public Int64IdAttribute(Int64 value)
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StringIdAttribute : Attribute, IdAttribute
    {
        public string Value { get; private set; }

        /// <summary>
        /// 不建议在标识上构建层级，比如不建议构建像"Person.User.Add"这样的标识，层级结构体现在别处。
        /// </summary>
        /// <param name="value"></param>
        public StringIdAttribute(string value)
        {
            this.Value = value;
        }
    }
}
