
namespace Anycmd.Model
{

    /// <summary>
    /// 实体对象抽象基类
    /// </summary>
    public abstract class EntityObject<TEntityId> : IEntity<TEntityId>
    {
        private TEntityId _id;

        protected EntityObject() { }

        protected EntityObject(TEntityId id)
        {
            _id = id;
        }

        /// <summary>
        /// 实体标识
        /// </summary>
        public virtual TEntityId Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        /// <summary>
        /// 访问控制内容类型。
        /// 取值形如：text/javascript、text/xacml、text/javascript,fileLocation、text/xacml,fileLocation。
        /// </summary>
        public string AcContentType { get; set; }

        /// <summary>
        /// 访问控制内容。一些作用域为当前实体的javascript或xacml脚本。
        /// </summary>
        public string AcContent { get; set; }

        /// <summary>
        /// Gets or sets the entity's ETag（电子标签）. Set this value to '*' in order to force an
        /// overwrite to an entity as part of an update operation.
        /// </summary>
        public virtual byte[] ETag { get; set; }
    }
}
