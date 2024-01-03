namespace Talabat.Core.Specification
{
    public class ProductSpecParams
    {

        /// <summary>
      
        ///  There 2prop to pagination => the pagination is how meny prduct you wont to get it in the page   
       
        private int pagesize = 5;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value > 10 ? 10 : value; }
        }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

        public int PageIndex { get; set; } = 1;

        /// </summary>
        public string? Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
    }
}
