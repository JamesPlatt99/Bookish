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
                BookTypeRepository bookTypeRepository = new BookTypeRepository();
                bookTypeRepository.InsertBookType(book);
            }
        }

        private static BookTypes GetBook(string[] line)
        {
            BookTypes bookType = new BookTypes()
            {
                title = line[1],
                author_id = GetAuthorId(line[4]),
                genre = "Unknown",
                releaseDate = DateTime.Parse(line[6]),
                ISBN = line[9],
                coverImageURL = line[12]
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