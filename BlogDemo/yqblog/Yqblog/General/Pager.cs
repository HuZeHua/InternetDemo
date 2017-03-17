using System.Linq;

namespace Yqblog.General
{
    public class Pager
    {
        private int _pageSize;
        private int _pageCount;
        public int Amount { get; set; }

        public int PageSize
        {
            get { return _pageSize == 0 ? 10 : _pageSize; }
            set { _pageSize = value; }
        }

        public int PageCount
        {
            get { return CounterPageCount(); }
            set { _pageCount = value; }
        }

        public int PageNo { get; set; }

        public IQueryable Entity { get; set; }

        private int CounterPageCount()
        {
            if (Amount % PageSize == 0)
            {
                return Amount / PageSize > 0 ? Amount / PageSize : 0;
            }
            return Amount / PageSize > 0 ? Amount / PageSize + 1 : 0;
        }
    }
}