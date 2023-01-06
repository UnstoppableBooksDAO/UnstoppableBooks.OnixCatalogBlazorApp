using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using OnixCatalogBlazorApp.Models;

namespace OnixCatalogBlazorApp.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IFileService _fileService;
        private List<BookItem>        _bookItems;

        public CatalogService(IFileService fileService)
        {
            _fileService = fileService;
            _bookItems   = new List<BookItem>();
        }

        public List<BookItem> GetBooks()
        {
            return _bookItems;
        }

        public BookItem GetBook(long ean)
        {
            return _bookItems.First(x => x.Ean == ean);
        }

        public BookItem GetBook(string title)
        {
            return _bookItems.First(x => x.Title == title);
        }

        public List<BookItem> Add(string onixContent)
        {
            var newBookItems = _fileService.ReadFromFile(onixContent);

            _bookItems.AddRange(newBookItems);

            return _bookItems;
        }

        public List<BookItem> Add(BookItem bookItem)
        {
            _bookItems.Add(bookItem);

            return _bookItems;
        }

        public List<BookItem> Delete(long ean)
        {
            var bookItemToRemove = GetBook(ean);

            if (bookItemToRemove != null)
            {
                _bookItems.Remove(bookItemToRemove);                
            }

            return _bookItems;
        }

        public List<BookItem> Delete(string title)
        {
            var bookItemToRemove = GetBook(title);

            return Delete(bookItemToRemove?.Ean ?? 0);
        }
    }
}

