using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Pagination<T>
    {
        public List<T> Items { get; }
        public int TotalRecords { get; }
        public int Page { get; }
        public int PageSize { get; }

        public Pagination(List<T> items, int totalRecords, int page, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            Page = page;
            PageSize = pageSize;
        }
    }
}
