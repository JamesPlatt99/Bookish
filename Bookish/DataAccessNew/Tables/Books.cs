using System;

namespace DataAccessNew.Tables
{
    public class Books
    {
        public string name { get; set; }
        public int author { get; set; }
        public string genre { get; set; }
        public DateTime releaseDate { get; set; }
        public string ISBN { get; set; }
    }
}