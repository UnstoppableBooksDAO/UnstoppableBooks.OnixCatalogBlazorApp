using Newtonsoft.Json;
using OnixData;
using OnixData.Version3;

using OnixCatalogBlazorApp.Models;

namespace OnixCatalogBlazorApp.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<BookItem> ReadFromFile(string fileContents)
        {
            var books = new List<BookItem>();

            using (OnixParser parser = new OnixParser(fileContents, false))
            {
                foreach (OnixProduct tempProduct in parser)
                {
                    var tempBook =
                        new BookItem() { Ean = tempProduct.EAN
                                         , Title = tempProduct.Title
                                         , Author = tempProduct.PrimaryAuthor.OnixKeyNames 
                                         , Price = tempProduct.USDRetailPrice?.PriceAmountNum
                                         , Publisher = tempProduct.PublisherName
                                         , IsPublished = !String.IsNullOrEmpty(tempProduct.PublisherName)
                                         , PrimaryBISAC = tempProduct.BisacCategoryCode.IsMainSubject() ? 
                                                            tempProduct.BisacCategoryCode.MainSubject : 
                                                            tempProduct.BisacCategoryCode.SubjectCode
                                       };

                    books.Add(tempBook);
                }
            }

            return books;
        }

        public void SaveToFile(string outputFilepath, List<BookItem> bookItems)
        {
            string jsonBookList = JsonConvert.SerializeObject(bookItems);
            
            System.IO.File.WriteAllText(outputFilepath, jsonBookList);
        }
    }
}
