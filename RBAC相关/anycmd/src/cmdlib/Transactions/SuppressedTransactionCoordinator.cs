
namespace Anycmd.Transactions
{
    using Model;

    /// <summary>
    /// 将事务镇压下去，表示该事务协调器下辖的工作单元不是每一个都支持事务。
    /// </summary>
    internal sealed class SuppressedTransactionCoordinator : TransactionCoordinator
    {
        /// <summary>
        /// 初始化一个<c>SuppressedTransactionCoordinator</c>类的实例。
        /// </summary>
        /// <param name="unitOfWorks"></param>
        public SuppressedTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
        }
    }
}
