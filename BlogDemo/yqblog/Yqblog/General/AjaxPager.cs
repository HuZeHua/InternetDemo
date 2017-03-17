using res = Resource.Models.Web.Web;

namespace Yqblog.General
{
    public class AjaxPager
    {
        private readonly int _pagesize = 4;
        private readonly int _currentpage = 1;
        private readonly int _maxpage = 1;
        private int _prevPage;
        private int _nextPage;

        public AjaxPager(int pSize, int pCurrentpage, int pMaxpage)
        {
            _pagesize = pSize;
            _currentpage = pCurrentpage;
            _maxpage = pMaxpage;
            SetPrevPage();
            SetNextPage();
        }

        public void SetPrevPage()
        {
            if (_currentpage > 1)
                _prevPage = _currentpage - 1;
            else
                _prevPage = _currentpage;
        }

        public void SetNextPage()
        {
            if (_currentpage < _maxpage)
                _nextPage = _currentpage + 1;
            else
                _nextPage = _currentpage;
        }

        public string GetPageInfoHtml()
        {
            if (_maxpage < 2)
                return "";
            var btnPrevPage = "<a href='javascript:;' p=" + _prevPage + ">" + res.Preview + "</a>&nbsp;";
            var btnNextPage = "&nbsp;<a href='javascript:;' p=" + _nextPage + ">" + res.Next + "</a>";
            if (_currentpage == 1)
            {
                btnPrevPage = "";
            }
            if (_currentpage == _maxpage)
            {
                btnNextPage = "";
            }
            var pageNumberList = "";
            if (_maxpage > 2 * _pagesize + 3)
            {
                pageNumberList = GetNumberPage(1);
                var start = 2;
                var end = _maxpage - 1;
                int added;

                if ((_currentpage - _pagesize) <= 2)
                {
                    added = 2 - (_currentpage - _pagesize);
                    end = _currentpage + _pagesize + added;
                }
                else if ((_currentpage + _pagesize) >= (_maxpage - 1))
                {
                    added = (_currentpage + _pagesize) - (_maxpage - 1);
                    start = _currentpage - _pagesize - added;
                }
                else
                {
                    start = _currentpage - _pagesize;
                    end = _currentpage + _pagesize;
                }

                if (start > 2)
                { pageNumberList = pageNumberList + "... "; }

                for (var i = start; i <= end; i++)
                {
                    pageNumberList += GetNumberPage(i);
                }

                if (end < (_maxpage - 1))
                { pageNumberList = pageNumberList + "... "; }

                pageNumberList += GetNumberPage(_maxpage);
            }
            else
            {
                for (var i = 1; i <= _maxpage; i++)
                {
                    pageNumberList += GetNumberPage(i);
                }
            }

            var html = btnPrevPage + pageNumberList + btnNextPage;
            return html;
        }

        public string GetNumberPage(int pPageno)
        {
            string t;
            var p = pPageno.ToString();
            if (pPageno == _currentpage)
            { t = "<b style='color:red'>" + p + "</b>&nbsp;"; }
            else
            {
                t = "<a  href='javascript:;' p=" + pPageno + ">" + p + "</a>&nbsp;";
            }
            return t;
        }
    }
}