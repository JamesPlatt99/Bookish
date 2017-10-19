using System;

namespace Bookish.Web.ViewModels
{
    public class BookData
    {
        public string title { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public DateTime releaseDate { get; set; }
        public string ISBN { get; set; }
        public string coverImageURL { get; set; }
        public int quantity { get; set; }
        public string Error { get; set; }
    }
}