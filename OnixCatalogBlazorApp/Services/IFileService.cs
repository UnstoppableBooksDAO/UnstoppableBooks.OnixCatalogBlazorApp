using OnixCatalogBlazorApp.Models;

namespace OnixCatalogBlazorApp.Services
{
    public interface IFileService
    {
        List<BookItem> ReadFromFile(string fileContents);

        void SaveToFile(string outputFilepath, List<BookItem> bookItems);
    }
}
