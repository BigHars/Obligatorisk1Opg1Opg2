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
    public class BookTests
    {
        #region TestInitialize

        private Book _book;

        [TestInitialize]
        public void BeforeEachTest()
        {
            //arrange
            _book = new Book();
        }
        #endregion

        #region IdTest
        [TestMethod()]
        [DataRow(5)]
        [DataRow(15)]
        [DataRow(26)]
        public void IdTestOk(int id)
        {
            //act
            _book.Id = id;

            //assert
            Assert.AreEqual(id, _book.Id);
        }
        #endregion

        #region PriceTest
        [TestMethod()]
        [DataRow(1)]
        [DataRow(600)]
        [DataRow(1200)]
        public void PriceTestValid(double validPrice)
        {
            //act
            _book.Price = validPrice;

            //assert
            Assert.AreEqual(validPrice, _book.Price);
        }

        [TestMethod()]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(1201)]
        public void PriceTestInvalid(double invalidPrice)
        {
            //act & assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _book.Price = invalidPrice);
        }
        #endregion

        #region TitleTest
        [TestMethod()]
        [DataRow("Homers Odyssé")]
        [DataRow("The Tripitaka")]
        [DataRow("The Art of War")]
        public void TitleTestValid(string validTitle)
        {
            //act
            _book.Title = validTitle;

            //assert
            Assert.AreEqual(validTitle, _book.Title);
        }

        [TestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("A")]
        [DataRow("AB")]
        public void TitleTestInvalid(string invalidTitle)
        {
            //act & assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _book.Title = invalidTitle);
        }
        #endregion

        #region ToStringTest
        [TestMethod]
        [DataRow(1, "Who Wrote The Bible?", "Richard Elliot Friedman", 540)]
        [DataRow(2, "Homers Iliad", "Homer", 350)]
        [DataRow(3, "Moby Dick", "Herman Melville", 240)]
        public void ToStringTestOk(int validId, string validTitle, string validAuthor, double validPrice)
        {
            //arrange
            var book = new Book
            {
                Id = validId,
                Title = validTitle,
                Price = validPrice,
                Author = validAuthor
            };

            //act
            string result = book.ToString();

            //assert
            string expected = $"BookId: {validId} / BookTitle: {validTitle} / BookAuthor: {validAuthor} / BookPrice: {validPrice}";
            Assert.AreEqual(expected, result);
        }
        #endregion
    }
}