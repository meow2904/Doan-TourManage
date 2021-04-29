using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourManagement.Models
{
    public class PageList<TEntity> : List<TEntity>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int CountItem => listItems.Count();
        public int TotalPages => (int)Math.Ceiling(CountItem / (double)PageSize);
        public List<TEntity> listItems { get; set; }

    }

}
