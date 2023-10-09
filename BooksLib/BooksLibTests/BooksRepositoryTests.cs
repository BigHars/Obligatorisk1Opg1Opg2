using Microsoft.VisualStudio.TestTools.UnitTesting;
using BooksLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BooksLib.Tests
{
    [TestClass()]
    public class BooksRepositoryTests
    {
        #region BooksRepositoryTest
        [TestMethod()]
        public void BooksRepositoryTestOk()
        {
            // arrange
            var repository = new BooksRepository();

            // assert
            Assert.AreEqual(5, repository.GetAllBooks().Count());
        }
        #endregion

        #region AddBooksTests
        [TestMethod()]
        [DataRow("Shuggie Bain", "Douglas Stuart", 299)]
        [DataRow("Blood Meridian", "Cormac McCarthy", 119)]
        [DataRow("White Noise", "Don DeLillo", 119)]
        public void AddBookTestOk(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();
            var bookToAdd = new Book() { Title = title, Author = author, Price = price };

            // act
            var addedBook = repository.AddBook(bookToAdd);

            // assert
            Assert.IsNotNull(addedBook);
            Assert.AreEqual(bookToAdd.Title, addedBook.Title);
            Assert.AreEqual(bookToAdd.Author, addedBook.Author);
            Assert.AreEqual(bookToAdd.Price, addedBook.Price);

            var bookInList = repository.GetAllBooks().FirstOrDefault(b => b.Id == addedBook.Id);
            Assert.IsNotNull(bookInList);
            Assert.AreEqual(bookToAdd.Title, bookInList.Title);
            Assert.AreEqual(bookToAdd.Author, bookInList.Author);
            Assert.AreEqual(bookToAdd.Price, bookInList.Price);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(null, "Douglas Stuart", 299)]
        [DataRow("Blood Meridian", null, 119)]
        [DataRow("White Noise", "Don DeLillo", null)]
        public void AddBookTestInvalidBooksFail(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();
            var invalidBook = new Book() { Title = title, Author = author, Price = price };

            // act
            var result = repository.AddBook(invalidBook);

            // assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow("", "Douglas Stuart", 299)]
        [DataRow("B", "Cormac McCarthy", 119)]
        [DataRow("Wh", "Don DeLillo", 119)]
        public void AddBookTestTitleTooShort(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();
            var bookWithInvalidName = new Book() { Title = title, Author = author, Price = price };

            // assert
            repository.AddBook(bookWithInvalidName);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow("Shuggie Bain", "", 299)]
        [DataRow("Blood Meridian", "C", 119)]
        [DataRow("White Noise", "Do", 119)]
        public void AddBookTestNameTooShort(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();
            var bookWithInvalidTitle = new Book() { Title = title, Author = author, Price = price };

            // assert
            repository.AddBook(bookWithInvalidTitle);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow("Shuggie Bain", "Douglas Stuart", -1)]
        [DataRow("Blood Meridian", "Cormac McCarthy", 1201)]
        public void AddBookTestPriceOutOfRange(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();
            var bookWithInvalidPrice = new Book() { Title = title, Author = author, Price = price };

            // assert
            repository.AddBook(bookWithInvalidPrice);
        }
        #endregion

        #region GetAllBooksTests
        [TestMethod()]
        [DataRow("Shuggie Bain", "Douglas Stuart", 299)]
        [DataRow("Blood Meridian", "Cormac McCarthy", 119)]
        [DataRow("White Noise", "Don DeLillo", 119)]
        public void GetAllBooksTestOk(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();

            var books = new List<Book>
            {
                new Book() { Id = 1, Title = "Shuggie Bain", Author = "Douglas Stuart", Price = 299 },
                new Book() { Id = 2, Title = "Blood Meridian", Author = "Cormac McCarthy", Price = 119 },
                new Book() { Id = 3, Title = "White Noise", Author = "Don DeLillo", Price = 119 },
            };

            foreach (var book in books)
            {
                repository.AddBook(book);
            }

            // act
            var filteredBooks1 = repository.GetAllBooks(titleIncludes: title, authorIncludes: author, priceMax: price, orderBy: "title_asc");
            var filteredBooks2 = repository.GetAllBooks(titleIncludes: title, authorIncludes: author, priceMax: price, orderBy: "author_asc");
            var filteredBooks3 = repository.GetAllBooks(titleIncludes: title, authorIncludes: author, priceMax: price, orderBy: "price_asc");

            // assert
            Assert.IsNotNull(filteredBooks1);
            Assert.IsNotNull(filteredBooks2);
            Assert.IsNotNull(filteredBooks3);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllBooksTestFail()
        {
            var repository = new BooksRepository();

            var books = new List<Book>
            {
                new Book() { Id = 1, Title = "Shuggie Bain", Author = "Douglas Stuart", Price = 299 },
                new Book() { Id = 2, Title = "Blood Meridian", Author = "Cormac McCarthy", Price = 119 },
                new Book() { Id = 3, Title = "White Noise", Author = "Don DeLillo", Price = 119 },
            };

            foreach ( var book in books)
            {
                repository.AddBook(book);
            }

            repository.GetAllBooks(orderBy: "invalid_order");
        }
        #endregion

        #region GetBookByIdTests
        [TestMethod()]
        [DataRow("Shuggie Bain", "Douglas Stuart", 299)]
        [DataRow("Blood Meridian", "Cormac McCarthy", 119)]
        [DataRow("White Noise", "Don DeLillo", 119)]
        public void GetBookByIdTestOk(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();

            var expectedBook = new Book() { Title = title, Author = author, Price = price };
            repository.AddBook(expectedBook);

            // act
            var result = repository.GetBookById(expectedBook.Id);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBook.Id, result.Id);
            Assert.AreEqual(expectedBook.Title, result.Title);
            Assert.AreEqual(expectedBook.Author, result.Author);
            Assert.AreEqual(expectedBook.Price, result.Price);
        }

        [TestMethod()]
        [DataRow(999)]
        public void GetBookByIdTestFail(int id)
        {
            // arrange
            var repository = new BooksRepository();

            // act
            var result = repository.GetBookById(id);

            // assert
            Assert.IsNull(result);
        }
        #endregion

        #region RemoveBookTests
        [TestMethod()]
        [DataRow("Shuggie Bain", "Douglas Stuart", 299)]
        [DataRow("Blood Meridian", "Cormac McCarthy", 119)]
        [DataRow("White Noise", "Don DeLillo", 119)]
        public void RemoveBookTestOk(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();

            var expectedBook = new Book() { Title = title, Author = author, Price = price };
            repository.AddBook(expectedBook);

            // act
            var result = repository.RemoveBook(expectedBook.Id);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedBook.Id, result.Id);
            Assert.AreEqual(expectedBook.Title, result.Title);
            Assert.AreEqual(expectedBook.Author, result.Author);
            Assert.AreEqual(expectedBook.Price, result.Price);

            var removedBook = repository.GetBookById(expectedBook.Id);
            Assert.IsNull(removedBook);
        }

        [TestMethod()]
        public void RemoveBookTestInvalidId()
        {
            // arrange
            var repository = new BooksRepository();

            // arrange
            var result = repository.RemoveBook(999);

            // assert
            Assert.IsNull(result);
        }
        #endregion

        #region UpdateBookTests
        [TestMethod()]
        [DataRow("Shuggie Bain", "Douglas Stuart", 299)]
        [DataRow("Blood Meridian", "Cormac McCarthy", 119)]
        [DataRow("White Noise", "Don DeLillo", 119)]
        public void UpdateBookTestOk(string title, string author, double price)
        {
            // arrange
            var repository = new BooksRepository();

            var existingBook = new Book() { Title = "Oppenheimer", Author = "Cristopher Nolan", Price = 249 };
            repository.AddBook(existingBook);

            var updatedBook = new Book() { Title = title, Author = author, Price = price };

            // act
            var result = repository.UpdateBook(existingBook.Id, updatedBook);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(existingBook.Id, result.Id);
            Assert.AreEqual(existingBook.Title, result.Title);
            Assert.AreEqual(existingBook.Author, result.Author);
            Assert.AreEqual(existingBook.Price, result.Price);

            // check if the book has been updated
            var updatedBookInRepo = repository.GetBookById(existingBook.Id);
            Assert.IsNotNull(updatedBookInRepo);
            Assert.AreEqual(updatedBook.Title, updatedBookInRepo.Title);
            Assert.AreEqual(updatedBook.Author, updatedBookInRepo.Author);
            Assert.AreEqual(updatedBook.Price, updatedBookInRepo.Price);
        }

        [TestMethod()]
        public void UpdateBookTestInvalid()
        {
            // arrange
            var repository = new BooksRepository();

            var updatedBook = new Book() { Title = "Oppenheimer", Author = "Cristopher Nolan", Price = 249 };

            // act
            var result = repository.UpdateBook(999, updatedBook);

            // assert
            Assert.IsNull(result);
        }
        #endregion
    }
}