using Bookish.Web.ViewModels;
using DataAccessNew;
using DataAccessNew.Tables;

namespace Bookish.Web.Controllers
{
    public class InsertBook
    {
        public static BookTypes CreateBookType(BookData book)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            BookTypes bookType = new BookTypes()
            {
                author_id = authorRepository.GetAuthorId(book.author),
                title = book.title,
                coverImageURL = book.coverImageURL,
                genre = book.genre,
                ISBN = book.ISBN,
                releaseDate = book.releaseDate
            };
            bookTypeRepository.InsertBookType(bookType);
            return bookTypeRepository.GetSingleBookType(bookType.title);
        }

        public static bool CheckNewBookTitle(string title)
        {
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            return (bookTypeRepository.GetSingleBookType(title) == null);
        }

        public static void CreateBooks(BookTypes bookType, int quantity)
        {
            BookRepository bookRepository = new BookRepository();
            for (int i = 0; i < quantity; i++)
            {
                Book newBook = new Book()
                {
                    available = true,
                    BookTypes_id = bookType.id
                };
                bookRepository.InsertBook(newBook);
                
            }
        }
    }
}