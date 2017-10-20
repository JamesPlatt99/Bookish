using System;

namespace DataAccessNew.Tables
{
    public class Loans
    {
        public int id { get; set; }

        public int book_id { get; set; }

        public DateTime dateTakenOut { get; set; }

        public int user_id { get; set; }

        public int duration { get; set; }

        public bool returned { get; set; }

        public Users user { get; set; }

        public Book Book { get; set; }

        public DateTime dateReturned { get; set; }
    }
}