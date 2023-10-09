using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLib
{
    public class Book
    {
        #region Constructors
        public Book(int id, string title, string author, double price)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
        }

        public Book()
        {
        }

        #endregion

        #region InstanceField
        private double _price;
        private string _title;
        private string _author;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= 3)
                {
                    _title = value;
                } else
                {
                    throw new ArgumentOutOfRangeException("Title is requiered and must be at least 3 characters long.");
                }
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= 3)
                {
                    _author = value;
                } else
                {
                    throw new ArgumentOutOfRangeException("The authors name is requiered and must be at least 3 characters long.");
                }
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                if (value > 0 && value <= 1200)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Price must be in the range of [1-1200]");
                }
            }
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"BookId: {Id} / BookTitle: {Title} / BookAuthor: {Author} / BookPrice: {Price}";
        }
        #endregion
    }
    public record Filter(double? Price, string? Authour, string? Title, int? Id);
}
