
namespace Anycmd.Transactions
{
    using Model;
    using System.Transactions;


    /// <summary>
    /// 分布式事务协调器。其依赖于微软的<see cref="TransactionScope"/>类实现分布式事务协调
    /// </summary>
    internal sealed class DistributedTransactionCoordinator : TransactionCoordinator
    {
        private readonly TransactionScope _scope = new TransactionScope();

        /// <summary>
        /// 初始化一个<c>DistributedTransactionCoordinator</c>类的实例。
        /// </summary>
        /// <param name="unitOfWorks"></param>
        public DistributedTransactionCoordinator(params IUnitOfWork[] unitOfWorks)
            : base(unitOfWorks)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _scope.Dispose();
        }


        public override void Commit()
        {
            base.Commit();
            _scope.Complete();
        }
    }
}
