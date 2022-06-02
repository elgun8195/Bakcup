using System.Collections.Generic;

namespace Fiorella_Webim.ViewModels
{
    public class Pagination<T>
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }
        public Pagination(int pagecount,int cuurentpage, List<T> items)
        {
            PageCount = pagecount;
            CurrentPage = cuurentpage;
            Items = items;
        }
    }
}
