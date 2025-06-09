namespace DermaKlinik.API.Core.Models
{
    public class PagingRequestModel
    {
        public string? LanguageCode { get; set; } = "tr";
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public string? Direction { get; set; } = "asc";
    }

    public class PagedList<T> : List<T>
    {
        public int Page { get; private set; }
        public int TotalPages { get; private set; }
        public int Take { get; private set; }
        public int TotalCount { get; private set; }
        public bool hasNext => TotalPages > Page;

        public PagedList(IQueryable<T> items, int page, int take)
        {
            AddRange(items);
            TotalCount = items.Count();
            Take = take;
            Page = page;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Take);
        }
        public PagedList(IQueryable<T> items, PagingRequestModel model)
        {
            AddRange(items);
            TotalCount = items.Count();
            Take = model.Take;
            Page = model.Page;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Take);
        }
        public PagedList(List<T> items, int page, int take)
        {
            AddRange(items);
            TotalCount = items.Count;
            Take = take;
            Page = page;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Take);
        }
        public PagedList(List<T> items, int count, int page, int take)
        {
            AddRange(items);
            TotalCount = count;
            Take = take;
            Page = page;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Take);
        }
        public PagedList(List<T> items, PagingRequestModel model)
        {
            AddRange(items);
            TotalCount = items.Count;
            Take = model.Take;
            Page = model.Page;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Take);
        }
        public PagedList(List<T> items, int count, PagingRequestModel model)
        {
            AddRange(items);
            TotalCount = count;
            Take = model.Take;
            Page = model.Page;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Take);
        }
        public PagedList()
        {

        }
    }
}
