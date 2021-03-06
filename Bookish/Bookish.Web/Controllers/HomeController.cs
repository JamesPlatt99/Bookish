﻿using Bookish.Web.Models;
using Bookish.Web.ViewModels;
using DataAccessNew;
using System.Web.Mvc;
using DataAccessNew.Tables;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsableIndex()
        {
            return View();
        }

        public ActionResult PublicLibrary(LibraryParameters libraryParameters)
        {
            return View(libraryParameters);
        }

        public bool ValidateUser(UserData user)
        {
            try
            {
                LogIn login = new LogIn(user.User.userName);
                return login.ValidatePassword(user.User.passwordHash);
            }
            catch
            {
                return false;
            }
        }

        public ActionResult VerifyLogin(LoginRegisterData loginRegisterData)
        {
            LoginRegister loginRegister = new LoginRegister();
            if (loginRegisterData.UserName != null && loginRegisterData.Password != null && loginRegister.Validate(loginRegisterData))
            {
                UserData userData = new UserData();
                userData.GetUserData(loginRegisterData);
                return View("Home", userData);
            }
            LoginRegisterData error = new LoginRegisterData() { Error = "Invalid credentials." };
            return View("Index", error);
        }

        public ActionResult ReturnBook(int rentalId)
        {
            LoanRepository loanRepository = new LoanRepository();
            Loans loan = loanRepository.GetLoan(rentalId);
            loanRepository.ReturnBook(loan);
            return View("Home");
        }

        public ActionResult Home(UserData userData)
        {
            return View(userData);
        }

        public ActionResult Library(LibraryParameters libraryParameters)
        {
            return View(libraryParameters);
        }

        public ActionResult ViewStatus(int id)
        {
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            BookTypes bookTypes = bookTypeRepository.GetSingleBookType(id);
            return View(bookTypes);
        }

        public ActionResult TakeOutBook(int id)
        {
            UserRepository userRepository = new UserRepository();
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            BookRental bookRental = new BookRental()
            {
                User = userRepository.GetSingleUser(Session["userName"].ToString()),
                BookType = bookTypeRepository.GetSingleBookType(id)
            };
            return View(bookRental);
        }

        public ActionResult ConfirmRental(int typeId, int duration)
        {
            Rentals rentals = new Rentals();
            UserRepository userRepository = new UserRepository();
            int id = userRepository.GetSingleUser(Session["userName"].ToString()).id;
            rentals.CreateRental(typeId, duration, id);
            return View("Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AddBookResult(BookData book)
        {
            if (!InsertBook.CheckNewBookTitle(book.title))
            {
                book = new BookData()
                {
                    Error = "Title already exists in library."
                };
                return View("AddBook", book);
            }
            BookRepository bookRepository = new BookRepository();
            BookTypes bookType = InsertBook.CreateBookType(book);
            InsertBook.CreateBooks(bookType,book.quantity);
            
            return View("Library", new LibraryParameters());
        }

        public ActionResult EditBookResult(BookTypes bookType)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            bookType.author_id = authorRepository.GetAuthorId(bookType.author.name);
            bookTypeRepository.UpdateBookType(bookType);
                return View("Library", new LibraryParameters());
        }

        public ActionResult AddBook()
        {
            return View();
        }

        public ActionResult RentalHistory()
        {
            return View();
        }

        public ActionResult EditBook(int id)
        {
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            BookTypes bookType = bookTypeRepository.GetSingleBookType(id);
            return View("EditBook", bookType);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}