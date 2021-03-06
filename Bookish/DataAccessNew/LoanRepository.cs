﻿using System;
using Dapper;
using DataAccessNew.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessNew
{
    public class LoanRepository
    {
        private readonly IDbConnection _db = new SqlConnection("Server = localhost; Database=Booksih;Trusted_Connection=True;");

        public void CreateNewLoan(Loans loan)
        {
            _db.Execute($"INSERT Loans(book_id,user_id,dateTakenOut,duration,returned) VALUES ({loan.book_id},{loan.user_id},'{loan.dateTakenOut:yyyy-MM-dd}',{loan.duration},'false')");
        }

        public Loans GetLoan(int loanId)
        {
            BookRepository bookRepository = new BookRepository();
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            UserRepository userRepository = new UserRepository();
            string sqlString = $"SELECT * FROM Loans Where id = '{loanId}';";
            Loans loan = _db.Query<Loans>(sqlString).SingleOrDefault();
            loan.Book = bookRepository.GetBookOfId(loan.book_id);
            loan.Book.BookType = bookTypeRepository.GetSingleBookType(loan.Book.BookTypes_id);
            loan.user = userRepository.GetSingleUser(loan.user_id);
            return loan;
        }

        public Loans GetLoanFromBookId(int bookId)
        {
            BookRepository bookRepository = new BookRepository();
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            UserRepository userRepository = new UserRepository();
            string sqlString = $"SELECT * FROM Loans Where book_id = '{bookId}' AND returned = 'false';";
            Loans loan = _db.Query<Loans>(sqlString).SingleOrDefault();
            loan.Book = bookRepository.GetBookOfId(loan.book_id);
            loan.Book.BookType = bookTypeRepository.GetSingleBookType(loan.Book.BookTypes_id);
            loan.user = userRepository.GetSingleUser(loan.user_id);
            return loan;
        }

        public List<Loans> GetActiveLoans(int userId)
        {
            string sqlString = $"SELECT * FROM Loans WHERE User_Id = {userId} AND returned = 'false';";
            List<Loans> loans = (List<Loans>)_db.Query<Loans>(sqlString);
            loans = GetBooks(loans);
            loans = GetUsers(loans);
            return loans;
        }

        public List<Loans> GetPastLoans(int userId)
        {
            string sqlString = $"SELECT * FROM Loans WHERE User_Id = {userId} AND returned = 'true' ORDER BY dateReturned DESC;";
            List<Loans> loans = (List<Loans>)_db.Query<Loans>(sqlString);
            loans = GetBooks(loans);
            loans = GetUsers(loans);
            return loans;
        }

        private List<Loans> GetUsers(List<Loans> loans)
        {
            UserRepository userRepository = new UserRepository();
            foreach (Loans loan in loans)
            {
                loan.user = userRepository.GetSingleUser(loan.user_id);
            }
            return loans;
        }

        public void ReturnBook(Loans loan)
        {
            _db.Query($"UPDATE Loans Set returned = 'true', dateReturned = '{DateTime.Now:yyyy-MM-dd}' Where id = {loan.id}");
            _db.Query($"UPDATE Book SET available = 'true' Where id = {loan.Book.id}");
        }

        private List<Loans> GetBooks(List<Loans> loans)
        {
            BookRepository bookRepository = new BookRepository();
            BookTypeRepository bookTypeRepository = new BookTypeRepository();
            foreach (Loans loan in loans)
            {
                loan.Book = bookRepository.GetBookOfId(loan.book_id);
                loan.Book.BookType = bookTypeRepository.GetSingleBookType(loan.Book.BookTypes_id);
            }
            return loans;
        }
    }
}