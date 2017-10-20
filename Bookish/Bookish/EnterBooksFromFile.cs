using DataAccessNew;
using DataAccessNew.Tables;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bookish
{
    public static class EnterBooksFromFile
    {
        public static void EnterFiles()
        {
            string path = "books.csv";
            List<BookTypes> books = new List<BookTypes>();
            StreamReader file = new StreamReader(path);
            string curLine = file.ReadLine();
            while ((curLine = file.ReadLine()) != null)
            {
                books.Add(GetBook(curLine.Split(',')));
            }

            foreach (BookTypes book in books)
            {
                BookRepository bookRepository = new BookRepository();
                BookTypeRepository bookTypeRepository = new BookTypeRepository();
                if (CheckBookTitleIsFree(book.title))
                {
                    bookTypeRepository.InsertBookType(book);
                }
                Book copy = new Book()
                {
                    BookTypes_id = bookTypeRepository.GetSingleBookType(book.title).id
                };
                for (int i = 0; i < book.availableCopies;i++)
                {
                    bookRepository.InsertBook(copy);
                }
            }
        }

        public static Boolean CheckBookTitleIsFree(string title)
        {
            BookTypeRepository bookTypeRepository = new BookTypeRepository();

            List<BookTypes> bookTypes = bookTypeRepository.GetBookTypes();
            foreach (BookTypes bookType in bookTypes)
            {
                if (bookType.title == title)
                {
                    return false;
                }
            }
            return true;
        }

        private static BookTypes GetBook(string[] line)
        {
            DateTime releaseDate = DateTime.MinValue;
            DateTime.TryParse(line[3], out releaseDate);
            BookTypes bookType = new BookTypes()
            {
                title = line[0],
                author_id = GetAuthorId(line[1]),
                genre = line[2],
                releaseDate =  releaseDate,
                ISBN = line[4],
                coverImageURL = line[5],
                availableCopies = int.Parse(line[6])
            };
            return bookType;
        }

        private static int GetAuthorId(string author)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            return authorRepository.GetAuthorId(author);
        }
    }
}