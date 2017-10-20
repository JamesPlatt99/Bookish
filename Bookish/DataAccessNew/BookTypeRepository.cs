using Dapper;
using DataAccessNew.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessNew
{
    public class BookTypeRepository
    {
        private readonly IDbConnection _db = new SqlConnection("Server = localhost; Database=Booksih;Trusted_Connection=True;");

        public List<BookTypes> GetBookTypes()
        {
            string sqlString = $"SELECT * FROM BookTypes;";
            List<BookTypes> bookTypes = (List<BookTypes>)_db.Query<BookTypes>(sqlString);
            bookTypes = GetAuthors(bookTypes);
            bookTypes = BookRepository.GetBooksOfType(bookTypes);
            bookTypes = GetAvailableCopies(bookTypes);
            return bookTypes;
        }

        public List<BookTypes> SearchBookTypes(string search, string type)
        {
            string sqlString = $"SELECT * FROM Author,BookTypes WHERE {type} LIKE '%{search}%' AND BookTypes.Author_Id = Author.Id;";
            List<BookTypes> bookTypes = (List<BookTypes>)_db.Query<BookTypes>(sqlString);
            bookTypes = GetAuthors(bookTypes);
            bookTypes = BookRepository.GetBooksOfType(bookTypes);
            bookTypes = GetAvailableCopies(bookTypes);
            return bookTypes;
        }

        private List<BookTypes> GetAvailableCopies(List<BookTypes> bookTypes)
        {
            foreach (BookTypes bookType in bookTypes)
            {
                foreach (Book book in bookType.Books)
                {
                    if (book.available)
                    {
                        bookType.availableCopies++;
                    }
                }
            }
            return bookTypes;
        }

        private List<BookTypes> GetAuthors(List<BookTypes> books)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            foreach (BookTypes book in books)
            {
                book.author = authorRepository.GetSingleAuthor(book.author_id);
            }
            return books;
        }

        public BookTypes GetSingleBookType(int bookTypeId)
        {
            string sqlString = $"SELECT * FROM BookTypes Where id = '{bookTypeId}';";
            BookTypes bookType = _db.Query<BookTypes>(sqlString).SingleOrDefault();
            AuthorRepository authorRepository = new AuthorRepository();
            bookType.author = authorRepository.GetSingleAuthor(bookType.author_id);
            return bookType;
        }

        public BookTypes GetSingleBookType(string title)
        {
            string sqlString = $"SELECT * FROM BookTypes Where title = '{title}';";
            BookTypes bookType = _db.Query<BookTypes>(sqlString).SingleOrDefault();
            return bookType;
        }

        public void InsertBookType(BookTypes bookType)
        {
            _db.Execute($"INSERT BookTypes(title, author_Id,genre,releaseDate,ISBN,coverImageURL) VALUES ('{bookType.title}','{bookType.author_id}','{bookType.genre}','{bookType.releaseDate:yyyy-MM-dd}','{bookType.ISBN}','{bookType.coverImageURL}')");
        }

        public bool DeleteBookType(int bookId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookType(BookTypes bookType)
        {
            _db.Execute($"UPDATE BookTypes SET title = '{bookType.title}', author_id = '{bookType.author_id}', genre= '{bookType.genre}', releaseDate = '{string.Format($"{bookType.releaseDate:yyyy-MM-dd}")}', ISBN = '{bookType.ISBN}', coverImageURL = '{bookType.coverImageURL}' WHERE id = {bookType.id}");
        }
    }
}