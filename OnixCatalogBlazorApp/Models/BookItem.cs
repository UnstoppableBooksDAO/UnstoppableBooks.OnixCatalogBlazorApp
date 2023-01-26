using System.ComponentModel.DataAnnotations;

namespace OnixCatalogBlazorApp.Models
{
    public class BookItem
    {
        public long Ean { get; set; }

        [Required]
        [StringLength(512, ErrorMessage = "Title too long (512 character limit).")]
        public string? Title { get; set; }

        public string? AuthorName { get; set; }

        public string? AuthorEthereumId { get; set; }

        [Range(1, 100000, ErrorMessage = "Accommodation invalid (1-100000).")]
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
