using Dapper;
using DataAccessNew.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessNew
{
    public class AuthorRepository
    {
        private readonly IDbConnection _db = new SqlConnection("Server = localhost; Database=Booksih;Trusted_Connection=True;");

        public List<Author> GetAuthors()
        {
            string sqlString = "SELECT * FROM Author;";
            List<Author> authors = (List<Author>)_db.Query<Author>(sqlString);
            return authors;
        }

        public Author GetSingleAuthor(int authorId)
        {
            string sqlString = $"SELECT * FROM Author Where id = '{authorId}';";
            Author author = _db.Query<Author>(sqlString).SingleOrDefault();
            return author;
        }

        public int GetAuthorId(string authName)
        {
            string sqlString = $"SELECT * FROM Author Where name = '{authName}';";
            Author author = _db.Query<Author>(sqlString).SingleOrDefault();
            if (author != null) return author.id;
            author = new Author()
            {
                name = authName
            };
            InsertAuthor(author);
            return GetAuthorId(authName);
        }

        public void InsertAuthor(Author author)
        {
            _db.Execute($"INSERT Author(name) VALUES ('{author.name}')");
        }

        public bool DeleteBook(int authorId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBook(Author author)
        {
            throw new NotImplementedException();
        }
    }
}