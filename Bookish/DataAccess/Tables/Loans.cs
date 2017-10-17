using System;

namespace Bookish.DataConnection.Tables
{
    public class Loans
    {
        public int id { get; set; }
        public DateTime dateTakenOut { get; set; }
        public int user { get; set; }
        public int duration { get; set; }
        public bool returned { get; set; }
    }
}