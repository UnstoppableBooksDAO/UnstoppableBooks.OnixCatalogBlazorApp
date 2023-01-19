namespace OnixCatalogBlazorApp.Models
{
    public class BookItem
    {
        public long Ean { get; set; }

        public string? Title { get; set; }

        public string? AuthorName { get; set; }

        public string? AuthorEthereumId { get; set; }

        public decimal? Price { get; set; }

        public string? PrimaryBISAC { get; set; }

        public bool IsPublished { get; set; }

        public string? Publisher { get; set; }

        public DateTime DateCreated { get; set; }

        public BookItem()
        {
            Ean         = 0;
            IsPublished = false;
            DateCreated = DateTime.Now;
        }

        public BookItem(long id, bool isPublished, DateTime dateCreated)
        {
            Ean         = id;
            IsPublished = isPublished;
            DateCreated = dateCreated;
        }
    }
}
