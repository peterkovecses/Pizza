namespace Pizza.Bll.Helpers
{
    public class ProductQueryParameters : QueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchTerm { get; set; }
    }
}
