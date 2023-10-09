using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BooksLib
{
    public class BooksRepository : IBooksRepository
    {
        #region InstanceField
        private int _nextid = 0;
        private readonly List<Book> _bookList = new();
        #endregion

        #region New List of Books
        public BooksRepository()
        {
            _bookList.Add(new Book() { Id = _nextid++, Title = "How to Change Your Mind", Author = "Michael Pollan", Price = 299 });
            _bookList.Add(new Book() { Id = _nextid++, Title = "This Is Your Mind on Plants", Author = "Michael Pollan", Price = 149 });
            _bookList.Add(new Book() { Id = _nextid++, Title = "The Agile Samurai", Author = "Jonathan Rasmusson", Price = 219 });
            _bookList.Add(new Book() { Id = _nextid++, Title = "The Botany of Desire", Author = "Michael Pollan", Price = 169 });
            _bookList.Add(new Book() { Id = _nextid++, Title = "American Psycho", Author = "Bret Easton Ellis", Price = 119 });
        }
        #endregion

        #region Methods
        /// <summary>
        /// AddBook method adds a book to the book list.
        /// </summary>
        /// <param name="book"> Consumes a book object as a parameter </param>
        /// <returns> Returns the added book object or null if the validation check fails </returns>
        public Book? AddBook(Book book)
        {
            // validation checks
            if (string.IsNullOrEmpty(book.Title) || book.Title.Length < 3 ||
                string.IsNullOrEmpty(book.Author) || book.Author.Length < 3 ||
                book.Price <= 0 || book.Price >= 1200)
            {
                return null; // return null if any of the validation checks fail
            }

            book.Id = _nextid++;
            _bookList.Add(book);
            return book;
        }

        /// <summary>
        /// Returns a filtered and sorted list of books.
        /// </summary>
        /// <param name="idAfter"> Filters books with an ID greater than the specified value </param>
        /// <param name="titleIncludes"> Filters books where the title includes a specified string </param>
        /// <param name="authorIncludes"> Filters books where the author's name includes a specified string</param>
        /// <param name="priceMax"> Filters books where the book's price is less than the specified price </param>
        /// <param name="orderBy"> Sorts the books based on the specified criteria (e.g. title, author's name or price) </param>
        /// <returns> An IEnumerable of book objects representing the filtered and sorted books </returns>
        /// <exception cref="ArgumentException"> Throws an Argument Excpetion if the orderBy value does not exist </exception>
        public IEnumerable<Book> GetAllBooks(int? idAfter = null, string? titleIncludes = null, string? authorIncludes = null, double? priceMax = null, string? orderBy = null)
        {
            IEnumerable<Book> filter = new List<Book>(_bookList);

            //filtering
            if (idAfter != null)
            {
                filter = filter.Where(b => b.Id > idAfter);
            }
            if (titleIncludes != null)
            {
                filter = filter.Where(b => b.Title.Contains(titleIncludes));
            }
            if (authorIncludes != null)
            {
                filter = filter.Where(b => b.Author.Contains(authorIncludes));
            }
            if (priceMax != null)
            {
                filter = filter.Where(b => b.Price <= priceMax);
            }

            //sorting
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title":
                    case "title_asc":
                        filter = filter.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        filter = filter.OrderByDescending(b => b.Title);
                        break;
                    case "author":
                    case "author_asc":
                        filter = filter.OrderBy(b => b.Author);
                        break;
                    case "author_desc":
                        filter = filter.OrderByDescending(b => b.Author);
                        break;
                    case "id":
                    case "id_asc":
                        filter = filter.OrderBy(b => b.Id);
                        break;
                    case "id_desc":
                        filter = filter.OrderByDescending(b => b.Id);
                        break;
                    case "price":
                    case "price_asc":
                        filter = filter.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        filter = filter.OrderByDescending(b => b.Price);
                        break;
                    default:
                        throw new ArgumentException("Unknown filter: " + orderBy);
                }
            }
            return filter;
        }

        /// <summary>
        /// A method to get a book by a specified ID
        /// </summary>
        /// <param name="id"> Consumes an ID representing the book you want to find </param>
        /// <returns> Returns the book with the specific ID or returns null, if the ID does not exist </returns>
        public Book? GetBookById(int id)
        {
            return _bookList.FirstOrDefault(b => b.Id == id);
        }

        /// <summary>
        /// A method to remove a specific book
        /// </summary>
        /// <param name="id"> Consumes an ID representing the book you want to remove </param>
        /// <returns> Returns the book you have removed or null, if the book you want to remove does not exist </returns>
        public Book? RemoveBook(int id)
        {
            Book? book = GetBookById(id);
            if (book == null)
            {
                return null;
            }
            _bookList.Remove(book);
            return book;
        }

        /// <summary>
        /// Updates a book with new data
        /// </summary>
        /// <param name="id"> The ID representing the book you want to update </param>
        /// <param name="book"> Takes in a new book object with the values you want to update </param>
        /// <returns> Returns the updated book or, if the book does not exist returns null </returns>
        public Book? UpdateBook(int id, Book book)
        {
            Book? existingBook = GetBookById(id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Price = book.Price;
            return existingBook;
        }
        #endregion
    }
}
