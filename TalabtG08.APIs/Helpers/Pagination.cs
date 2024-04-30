using TalabtG08.APIs.Dtos;

namespace TalabtG08.APIs.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }  
        public int PageSize { get; set; }   
        public int Count { get; set; }  
        public IReadOnlyList<T> data { get; set; }

        public Pagination(int pageSize, int pageIndex, IReadOnlyList<T> mappProducts,int count)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            this.data = mappProducts;
            this.Count = count; 
        }


    }
}
