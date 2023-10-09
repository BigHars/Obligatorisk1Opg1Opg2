using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLib
{
    public interface IBooksRepository
    {
        #region Methods
        Book AddBook(Book book);
        IEnumerable<Book> GetAllBooks(int? idAfter = null, string? titleIncludes = null, string? authorIncludes = null, double? macPrice = null, string? orderBy = null);
        Book? GetBookById(int id);
        Book? RemoveBook(int id);
        Book? UpdateBook(int id, Book book);
        #endregion
    }
}
