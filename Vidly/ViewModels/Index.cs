using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class Index
    {
        public IEnumerable<Book> Latest { get; set; }
        public List<Book> Category1 { get; set; }
        public List<Book> Category2 { get; set; }
        public List<Book> Category3 { get; set; }
        public List<Book> Category4 { get; set; }

    }
}