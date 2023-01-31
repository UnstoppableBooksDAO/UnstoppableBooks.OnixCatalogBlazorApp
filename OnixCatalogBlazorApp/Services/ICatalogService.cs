using System.Collections.Generic;

using OnixCatalogBlazorApp.Models;

namespace OnixCatalogBlazorApp.Services
{
    public interface ICatalogService
    {
        List<BookItem> GetBooks();
        BookItem GetBook(long ean);
        BookItem GetBook(string title);
        List<BookItem> Add(string onixContent);
        List<BookItem> Add(BookItem bookItem);
        List<BookItem> Delete(long ean);
        List<BookItem> Delete(string title);
    }
}
