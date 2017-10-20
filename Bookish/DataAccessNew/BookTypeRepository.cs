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
        private readonly string[] _missingImageReplacements =
        {
            "http://cdn4.gurl.com/wp-content/gallery/cheating-excuses/confused-guy-shtrugging.jpg",
            "https://image.freepik.com/free-photo/guy-in-a-blue-jacket-thinking_1187-3006.jpg",
            "https://singleandnormal.files.wordpress.com/2011/02/confused-man.jpg",
            "https://lorilowe.files.wordpress.com/2010/12/hot-guy-thinking-nathalie-p.jpg",
            "https://thumbs.dreamstime.com/b/corporate-man-searching-something-mature-business-executive-looking-distance-43699714.jpg",
            "https://thumb1.shutterstock.com/display_pic_with_logo/776008/776008,1328802303,3/stock-photo-a-missing-person-notice-on-a-cartoon-carton-of-milk-94746055.jpg",
            "https://thumb9.shutterstock.com/display_pic_with_logo/1231094/125170985/stock-photo-wild-undressed-man-with-crazy-hair-and-beard-eating-a-banana-and-smiling-happily-studio-shot-125170985.jpg",
            "https://thumbs.dreamstime.com/z/wild-man-looking-confused-banana-28747200.jpg",
            "https://www.colourbox.com/preview/6490210-caveman-eating-a-banana.jpg",
            "https://www.colourbox.com/preview/6490323-wild-man-looking-confused-at-a-banana.jpg",
            "https://www.colourbox.com/preview/6490211-wild-guy-eating-a-banana.jpg"
        };
        private readonly IDbConnection _db = new SqlConnection("Server = localhost; Database=Booksih;Trusted_Connection=True;");

        public List<BookTypes> GetBookTypes()
        {
            string sqlString = $"SELECT * FROM BookTypes;";
            List<BookTypes> bookTypes = (List<BookTypes>)_db.Query<BookTypes>(sqlString);
            bookTypes = GetOtherData(bookTypes);
            return bookTypes;
        }

        public List<BookTypes> GetBookTypes(int numRows)
        {
            string sqlString = $"SELECT TOP {numRows} * FROM BookTypes;";
            List<BookTypes> bookTypes = (List<BookTypes>)_db.Query<BookTypes>(sqlString);
            bookTypes = GetOtherData(bookTypes);
            return bookTypes;
        }

        public List<BookTypes> SearchBookTypes(string search, string type, int numRows)
        {
            string sqlString = $"SELECT TOP {numRows} * FROM Author,BookTypes WHERE {type} LIKE '%{search}%' AND BookTypes.Author_Id = Author.Id;";
            List<BookTypes> bookTypes = (List<BookTypes>)_db.Query<BookTypes>(sqlString);
            bookTypes = GetOtherData(bookTypes);
            return bookTypes;
        }

        private List<BookTypes> GetOtherData(List<BookTypes> bookTypes)
        {
            bookTypes = GetAuthors(bookTypes);
            bookTypes = BookRepository.GetBooksOfType(bookTypes);
            bookTypes = GetAvailableCopies(bookTypes);
            bookTypes = FillMissingImages(bookTypes);
            return bookTypes;
        }

        private List<BookTypes> FillMissingImages(List<BookTypes> bookTypes)
        {
            
            Random random = new Random();
            foreach (BookTypes bookType in bookTypes)
            {
                if (bookType.coverImageURL != "NULL") continue;
                bookType.coverImageURL = _missingImageReplacements[random.Next(0, _missingImageReplacements.Length - 1)];
            }
            return bookTypes;
        }

        private BookTypes FillMissingImages(BookTypes bookType)
        {
                if (bookType.coverImageURL != "NULL") return bookType;
                Random random = new Random();
            bookType.coverImageURL = _missingImageReplacements[random.Next(0, _missingImageReplacements.Length - 1)];
            return bookType;
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
            return FillMissingImages(bookType);
        }

        public BookTypes GetSingleBookType(string title)
        {
            string sqlString = $"SELECT * FROM BookTypes Where title = '{title}';";
            BookTypes bookType = _db.Query<BookTypes>(sqlString).SingleOrDefault();
            return FillMissingImages(bookType);
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