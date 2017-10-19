namespace DataAccessNew.Tables
{
    public class Book
    {
        public int id { get; set; }
        public int BookTypes_id { get; set; }
        public bool available { get; set; }
        public BookTypes BookType { get; set; }
    }
}