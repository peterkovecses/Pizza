namespace Pizza.Bll.Helpers
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _pageSize;

        public int PageNumber { get; set; }
        public int PageSize { get => _pageSize; set => _pageSize = Math.Min(_maxSize, value); }

        public string SortBy { get; set; } = "Id";

        private string _sortOrder = "asc";
        public string SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                if (value.ToLower() == "desc")
                    _sortOrder = "desc";
            }
        }
    }
}
