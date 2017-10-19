using System;
using System.Collections.Generic;
using static System.String;

namespace DataAccessNew.Tables
{
    public class BookTypes
    {
        public int id { get; set; }
        public string title { get; set; }
        public int author_id { get; set; }
        public Author author { get; set; }
        public string genre { get; set; }
        public DateTime releaseDate { get; set; }
        public string ISBN { get; set; }
        public string coverImageURL { get; set; }
        public int availableCopies { get; set; }
        public List<Book> Books { get; set; }

        public static List<BookTypes> SortBooks(string sortby, List<BookTypes> books)
        {
            switch (sortby)
            {
                case "title":
                    {
                        books.Sort((x, y) => Compare(x.title, y.title, StringComparison.Ordinal));
                        break;
                    }
                case "author":
                    {
                        books.Sort((x, y) => Compare(x.author.name, y.author.name, StringComparison.Ordinal));
                        break;
                    }
                case "releaseDate":
                    {
                        books.Sort((x, y) => x.releaseDate.CompareTo(y.releaseDate));
                        break;
                    }
                case "genre":
                    {
                        books.Sort((x, y) => Compare(x.genre, y.genre, StringComparison.Ordinal));
                        break;
                    }
                case "ISBN":
                    {
                        books.Sort((x, y) => string.Compare(x.ISBN, y.ISBN, StringComparison.Ordinal));
                        break;
                    }
                case "availableCopies":
                    {
                        books.Sort((x, y) => x.availableCopies.CompareTo(y.availableCopies));
                        break;
                    }
                default:
                    {
                        books.Sort((x, y) => string.Compare(x.title, y.title, StringComparison.Ordinal));
                        break;
                    }
            }
            return books;
        }
    }
}