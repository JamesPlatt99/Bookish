using DataAccessNew;
using DataAccessNew.Tables;
using System;

namespace Bookish.Web.Controllers
{
    public class Rentals
    {
        public void CreateRental(int typeId, int loanDuration, int userId)
        {
            BookRepository bookRepository = new BookRepository();
            Book book = bookRepository.GetAvailableBookOfTypeId(typeId);
            bookRepository.SetBookAvailability(book.id, false);
            Loans loan = new Loans()
            {
                book_id = book.id,
                dateTakenOut = DateTime.Now,
                duration = loanDuration,
                user_id = userId
            };
            LoanRepository loanRepository = new LoanRepository();
            loanRepository.CreateNewLoan(loan);
        }
    }
}