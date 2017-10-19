using DataAccessNew.Tables;

namespace Bookish.Web.ViewModels
{
    public class BookRental
    {
        public BookTypes BookType { get; set; }
        public Users User { get; set; }
    }
}