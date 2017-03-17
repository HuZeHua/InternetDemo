
namespace Anycmd.Model
{
    /// <summary>
    /// 表示所有继承于该接口的类型都是工作单元的一种实现。
    /// </summary>
    /// <remarks>有关工作单元的详细信息，请参见UnitOfWork模式：http://martinfowler.com/eaaCatalog/unitOfWork.html。
    /// </remarks>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表示当前的工作单元是否支持Microsoft分布式事务处理机制。
        /// </summary>
        bool DistributedTransactionSupported { get; }
        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表述了当前的工作单元事务是否已被提交。
        /// </summary>
        bool Committed { get; }
        /// <summary>
        /// 提交当前的工作单元事务。
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚当前的工作单元事务。
        /// </summary>
        void Rollback();
    }
}
