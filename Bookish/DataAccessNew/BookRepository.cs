using Dapper;
using DataAccessNew.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessNew
{
    public class BookRepository
    {
        private static readonly IDbConnection Db = new SqlConnection("Server = localhost; Database=Booksih;Trusted_Connection=True;");

        public static List<BookTypes> GetBooksOfType(List<BookTypes> bookTypes)
        {
            foreach (BookTypes bookType in bookTypes)
            {
                string sqlString = $"SELECT * FROM Book WHERE Book.BookTypes_Id = {bookType.id};";
                bookType.Books = (List<Book>)Db.Query<Book>(sqlString);
            }
            return bookTypes;
        }

        public static BookTypes GetBooksOfType(BookTypes bookType)
        {
            string sqlString = $"SELECT * FROM Book WHERE Book.BookTypes_Id = {bookType.id};";
            bookType.Books = (List<Book>)Db.Query<Book>(sqlString);
            return bookType;
        }

        public Book GetAvailableBookOfTypeId(int bookTypesId)
        {
            string sqlString = $"SELECT TOP 1 * FROM Book Where BookTypes_id = '{bookTypesId}' AND available = 'true';";
            Book book = Db.Query<Book>(sqlString).SingleOrDefault();
            return book;
        }

        public Book GetBookOfId(int id)
        {
            string sqlString = $"SELECT * FROM Book Where id = '{id}';";
            Book book = Db.Query<Book>(sqlString).SingleOrDefault();
            return book;
        }

        public void SetBookAvailability(int id, bool available)
        {
            string sqlString = $"UPDATE Book SET available = '{available}' Where id = '{id}';";
            Book book = Db.Query<Book>(sqlString).SingleOrDefault();
        }
    }
}