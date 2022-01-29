using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;
        public int[] genreCount;
        public string[] genreName;

        public BooksController()
        {
            _context = new ApplicationDbContext();

            genreName = _context.Genres.Select(x => x.Name).ToList().ToArray();   //For Name of Categories

            genreCount = new int[genreName.Length]; //For Count of Categories
            for (int i = 1, j = 0; i <= genreName.Length; i++, j++)
            {
                genreCount[j] = _context.Movies.Count(x => x.GenreId == i && x.SetNo == 1);
            }

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /*[AllowAnonymous]
        public ActionResult getAuthors()
        {
            var Authors = _context.Movies.DistinctBy(x => x.Author).ToList();
            return Json(Authors.Select(x => new
            {
                DepartmentID = x.Id,
                DepartmentName = x.Author
            }).ToList(), JsonRequestBehavior.AllowGet);
        }*/

        [AllowAnonymous]
        public ActionResult getTopics()
        {
            return Json(_context.Genres.Select(x => new
            {
                DepartmentID = x.Id,
                DepartmentName = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult getAllBooks()
        {
            var Books = _context.Movies.DistinctBy(x => x.Name).ToList();
            return Json(Books.Select(x => new
            {
                DepartmentID = x.Id,
                DepartmentName = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult getAuthors(string q)
        {
            var list = new List<Book>();
            //var Books = _context.Movies.DistinctBy(x => x.Name).ToList();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                list = _context.Movies.DistinctBy(x => x.Author).Where(x => x.SetNo == 1 && x.Author.ToLower().Contains(q.ToLower())).ToList();
            }
            
            
            return Json(new { items = list.Select(x => new
            {
                id = x.Author,
                text = x.Author
            }).ToList()
        }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult getBooks(string q)
        {
            var list = new List<Book>();
            if(!string.IsNullOrEmpty(q))
            {
                var books = _context.Movies.Where(x => x.SetNo == 1).ToList();
                foreach (var book in books)
                {
                    if (RemoveDiacritics(book.Name.ToLower()).Contains(RemoveDiacritics(q.ToLower())))
                    {
                        list.Add(book);
                    }
                }
            }
            /*
             if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                list = _context.Movies.Where(x => x.SetNo == 1 && RemoveDiacritics(x.Name.ToLower()).Contains(RemoveDiacritics(q.ToLower()))).ToList();
            }
             */

            return Json(new
            {
                items = list.Select(x => new
                {
                    id = x.Id,
                    text = x.Name
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult getPublishers(string q)
        {
            var list = new List<Book>();
            //var Books = _context.Movies.DistinctBy(x => x.Name).ToList();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                list = _context.Movies.DistinctBy(x => x.Publisher).Where(x => x.SetNo == 1 && x.Publisher.ToLower().Contains(q.ToLower())).ToList();
            }


            return Json(new
            {
                items = list.Select(x => new
                {
                    id = x.Publisher,
                    text = x.Publisher
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }


        //public List<Book> GetBooks(string listType, string currentList)
        //{
        //    var books = new List<Book>();
        //    if (listType == "All" || currentList == "All")
        //    {
        //        books = _context.Movies.Include(b => b.Genre).ToList();  //Only those books that are available
        //        ViewBag.display = "تمام  کتابوں  کی  فہرست";
        //        ViewBag.listType = "All";

        //    }
        //    else
        //    {
        //        books = _context.Movies.Where(b => b.NumberAvailable > 0).Include(b => b.Genre).ToList();  //Only those books that are available
        //        ViewBag.display = "موجودہ  کتابوں  کی  فہرست";
        //        ViewBag.listType = "Available";

        //    }
        //    return books;
        //}
        [AllowAnonymous]
        public ViewResult Index()
        {
            var books = new Index
            {
                Latest = _context.Movies.Include(b => b.Genre).OrderByDescending(x => x.Id).Where( x => x.SetNo == 1).Take(8).ToList(),
                Category1 = _context.Movies.Include(b => b.Genre).Where(x => x.GenreId == 3 && x.SetNo == 1).Take(8).ToList(),
                Category2 = _context.Movies.Include(b => b.Genre).Where(x => x.GenreId == 14 && x.SetNo == 1).Take(8).ToList(),
                Category3 = _context.Movies.Include(b => b.Genre).Where(x => x.GenreId == 6 && x.SetNo == 1).Take(8).ToList(),
                Category4 = _context.Movies.Include(b => b.Genre).Where(x => x.GenreId == 10).Take(8).ToList()
            };
            /*To Enter Jild Number in all books
             * foreach (var book in books.Latest)
            {
                var number = _context.Movies.Where(m => m.Name == book.Name).ToList();
                foreach (var jild in number)
                {
                    if (jild.SetNo != 1)
                    {
                        jild.Set = book.Set;
                        _context.SaveChanges();
                    }
                }
            }
            foreach (var book in books.Latest)
            {
                var number = _context.Movies.Count(m => m.Name == book.Name);
                var single = _context.Movies.Where(x => x.Id == book.Id).SingleOrDefault();
                single.Set = (byte)number;
                _context.SaveChanges();
            }*/

            return View("Books",books);
        }
        /*
        [AllowAnonymous]
        public ViewResult List(int? page)
        {
            var books = _context.Movies.Include(b => b.Genre).OrderByDescending(x => x.Id).Where(x => x.SetNo == 1).ToList();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));

        }*/

        [AllowAnonymous]
        [HttpGet]
        public ViewResult List(string option, string search, int? pageNumber, string author, int? book, string publisher)
        {
            //if a user choose the radio button option as Subject  
            /*if (!string.IsNullOrEmpty(search))
            {
                search = RemoveDiacritics(search);
            }*/
            var ResultList = _context.Movies.Include(b => b.Genre).OrderByDescending(x => x.Id).Where(x => x.SetNo == 1).ToList();

            if (author != "Select Author" && author != null)
            { 
                ResultList = ResultList.Where(x => x.Author == author).ToList();
            }
            if(book.HasValue)
            {
                ResultList = ResultList.Where(x => x.Id == book).ToList();
            }
            if (publisher != "Select Publisher" && publisher != null)
            {
                ResultList = ResultList.Where(x => x.Publisher == publisher).ToList();
            }
            
            return View(ResultList.ToPagedList(pageNumber ?? 1, 9));
             
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        //[AllowAnonymous]
        //public ViewResult Index(string sortOrder, string currentBook, string currentAuthor, string currentTopic, string currentList, int? page, string bookName, string bookAuthor, string bookTopic, string listType, int? bookId)
        //{

        //    //var books = GetBooks(listType, currentList);  //Not giving option for the moment
        //    //var books = new List<Movie>();
        //    var books = _context.Movies.Include(b => b.Genre).ToList();  //All Books
        //    if (bookId.HasValue)
        //    {
        //        books = _context.Movies.Where(s => s.Id == bookId).ToList();
        //    }
        //    ViewBag.display = "موجودہ  کتابوں  کی  فہرست";
        //    ViewBag.listType = "Available";

        //    ViewBag.CurrentSort = sortOrder;
        //    if (bookName == "-1")
        //        bookName = null;
        //    if (bookAuthor == "-1")
        //        bookAuthor = null;
        //    if (bookTopic == "-1")
        //        bookTopic = null;

        //    if (bookName != null || bookAuthor != null || bookTopic != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        bookName = currentBook;
        //        bookAuthor = currentAuthor;
        //        bookTopic = currentTopic;
        //    }

        //    ViewBag.currentBook = bookName;
        //    ViewBag.currentAuthor = bookAuthor;
        //    ViewBag.currentTopic = bookTopic;

        //    var num = 0;

        //    if (!String.IsNullOrEmpty(bookTopic))
        //    {
        //        num = Convert.ToInt32(bookTopic);
        //    }

        //    if (!String.IsNullOrEmpty(bookName) && bookName.All(char.IsDigit))
        //    {
        //        var id = Convert.ToInt32(bookName);
        //        books = _context.Movies.Where(s => s.Id == id).ToList();
        //        bookName = string.Empty;
        //    }


        //    if (!String.IsNullOrEmpty(bookName) || !String.IsNullOrEmpty(bookAuthor) || !String.IsNullOrEmpty(bookTopic))
        //    {

        //        if (!String.IsNullOrEmpty(bookName) && !String.IsNullOrEmpty(bookAuthor) && !String.IsNullOrEmpty(bookTopic))
        //        {
        //            books = books.Where(s => s.Name.Contains(bookName)).Where(s => s.Author.Contains(bookAuthor)).Where(s => s.GenreId == num).ToList();
        //        }
        //        else if (!String.IsNullOrEmpty(bookName) && !String.IsNullOrEmpty(bookAuthor))
        //        {
        //            books = books.Where(s => s.Name.Contains(bookName)).Where(s => s.Author.Contains(bookAuthor)).ToList();
        //        }
        //        else if (!String.IsNullOrEmpty(bookAuthor) && !String.IsNullOrEmpty(bookTopic))
        //        {
        //            books = books.Where(s => s.Author.Contains(bookAuthor)).Where(s => s.GenreId == num).ToList();
        //        }
        //        else if (!String.IsNullOrEmpty(bookName) && !String.IsNullOrEmpty(bookTopic))
        //        {
        //            books = books.Where(s => s.Name.Contains(bookName)).Where(s => s.GenreId == num).ToList();
        //        }
        //        else if (!String.IsNullOrEmpty(bookName))
        //        {
        //            books = books.Where(s => s.Name.Contains(bookName)).ToList();
        //        }
        //        else if (!String.IsNullOrEmpty(bookAuthor))
        //        {
        //            books = books.Where(s => s.Author.Contains(bookAuthor)).ToList();
        //        }
        //        else if (!String.IsNullOrEmpty(bookTopic))
        //        {
        //            books = books.Where(s => s.GenreId == num).ToList();
        //        }
        //    }
        //    int pageSize = 6;
        //    int pageNumber = (page ?? 1);
        //    return View("Books", books.ToPagedList(pageNumber, pageSize));  //List


        //}


        [Authorize(Roles = RoleName.CanManageMovies)]
        
        public ViewResult New()
        {
            var genreTypes = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel()
            {
                Genres = genreTypes
            };
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            viewModel.Book = movie;
            return View("MovieForm", viewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int Id, int set)
        {
            var myBook = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            //var myBook = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Name == bookName.Name && m.SetNo == set);
            

            
            if (myBook == null)
                return HttpNotFound();


            var details = new Details
            {
                book = myBook,
                genreName = genreName,
                genreCount = genreCount
            };

            return View(details);

        }


        // GET: Movies/Random
       
        public ActionResult Random()
        {
            var movie = new Book() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        // GET: Movies/Random
       
        public ActionResult Borrowed()
        {
            if (!User.IsInRole(RoleName.CanManageMovies))
            {
                var rentals = _context.Rentals.Where(r => r.User.UserName == User.Identity.Name).OrderByDescending(s => s.Id).Include(c => c.User).Include(c => c.Movie).Where(c => c.DateReturned == DateTime.MinValue).ToList();
                return View(rentals);
            }
            else
            {
                var rentals = _context.Rentals.Include(c => c.User).Include(c => c.Movie).Where(c => c.DateReturned == DateTime.MinValue).OrderByDescending(s => s.Id).ToList();
                return View(rentals);
            }
        }

        public ActionResult History(string id)
        {
            var rentals = new List<Rental>();
            if(id.IsNullOrWhiteSpace())
            {
                rentals = _context.Rentals.Where(r => r.User.UserName == User.Identity.Name).OrderByDescending(s => s.Id).Include(c => c.User).Include(c => c.Movie).ToList();
            }
            else
            {
                rentals = _context.Rentals.Where(r => r.User.Id == id).OrderByDescending(s => s.Id).Include(c => c.User).Include(c => c.Movie).ToList();
            }
            return View(rentals);
        }

        
        public ActionResult Requests(int? page)
        {
            if (!User.IsInRole(RoleName.CanManageMovies))
            {
                var customer = _context.Users.Single(
                c => c.UserName == User.Identity.Name);
                var requests = _context.Requests.Where(r => r.User.UserName == customer.UserName).OrderByDescending(s => s.Id).Include(c => c.User).Include(c => c.Book).Include(c => c.Book.Genre).ToList();
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                return View(requests.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var requests = _context.Requests.Include(c => c.User).Include(c => c.Book).Include(c => c.Book.Genre).OrderByDescending(s => s.Id).ToList();
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                return View(requests.ToPagedList(pageNumber, pageSize));
            }
        }

        
        public ActionResult Accept(int requestId)
        {
            var requestInDb = _context.Requests.SingleOrDefault(c => c.Id == requestId);
            if (requestInDb == null)
                return View("Error");

            var movies = _context.Movies.Where(
                    m => m.Id == requestInDb.BookId && m.NumberAvailable > 0).SingleOrDefault();
            if (movies == null)
            {
                ViewBag.Unavailable = "Unavailable";

                IEnumerable<Request> requests= _context.Requests.Include(c => c.User).Include(c => c.Book).ToList();
                return View("Requests", requests);
            }
            var rental = new Rental
            {
                UserId = requestInDb.UserId,
                Movie = movies,
                DateRented = DateTime.Now
            };
            movies.NumberAvailable--;
            _context.Rentals.Add(rental);
            _context.Requests.Remove(requestInDb);
            
            var notification = new Notification { DateTime = DateTime.Now, Type = 1, Book = rental.Movie.Name };
            _context.Notifications.Add(notification);
            
            var userNotification = new UserNotification { NotificationId = notification.Id, UserId = rental.UserId };
            _context.UserNotifications.Add(userNotification);

            _context.SaveChanges();

            return RedirectToAction("Borrowed", "Books");
        }

       
        public ActionResult Decline(int requestId)
        {
            var requestInDb = _context.Requests.Include(c => c.Book).SingleOrDefault(c => c.Id == requestId);
            if (requestInDb == null)
                return View("Error");

            var userId = requestInDb.UserId;
            var bookName = requestInDb.Book.Name;
            _context.Requests.Remove(requestInDb);

            var notification = new Notification { DateTime = DateTime.Now, Type = 2, Book = bookName };
            _context.Notifications.Add(notification);

            var userNotification = new UserNotification { NotificationId = notification.Id, UserId = userId };
            _context.UserNotifications.Add(userNotification);

            _context.SaveChanges();

            return RedirectToAction("Requests", "Books");
        }

        [AllowAnonymous]
        public ActionResult Category(int id)
        {
            var categories = _context.Movies.Where(m => m.GenreId == id).ToList();

            if (categories == null)
                return HttpNotFound();

            return View(categories);

        }

        [AllowAnonymous]
        public ActionResult Topic(int id, int? page)
        {
            var books = _context.Movies.Where(m => m.Genre.Id == id).Include(m => m.Genre).ToList();
            if (books == null)
                return HttpNotFound();
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View("Books", books.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(MovieFormViewModel movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie.Book)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                //movie.DateAdded = DateTime.Now;

                if(!movie.Book.NumberInStock.HasValue)
                {
                    movie.Book.NumberInStock = 1;
                }
                movie.Book.NumberAvailable = movie.Book.NumberInStock;
                movie.Book.DateAdded = DateTime.Now;

                if(movie.Book.Set.HasValue && movie.Book.Set > 1)
                {
                    var sets = movie.Book.Set;
                    var temp = movie.Book.Name;
                    for(var i = 1; i <= sets; i++)
                    {
                        movie.Book.Name = temp + "  جلد  "  +  i;
                        _context.Movies.Add(movie.Book);
                        _context.SaveChanges();

                    }
                    return RedirectToAction("Index", "Books");
                }
                else
                {
                    _context.Movies.Add(movie.Book);
                    return RedirectToAction("Index", "Books");
                }
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Book.Name;
                movieInDb.Author = movie.Book.Author;
                movieInDb.Edition = movie.Book.Edition;
                movieInDb.GenreId = movie.Book.GenreId;
                
                movieInDb.Price = movie.Book.Price;
                movieInDb.Publisher = movie.Book.Publisher;
                movieInDb.Set = movie.Book.Set;
                movieInDb.Translator = movie.Book.Translator;

                
                //movieInDb.ReleaseDate = movie.ReleaseDate;
                if (movieInDb.NumberInStock == movieInDb.NumberAvailable)
                {
                    movieInDb.NumberAvailable = movie.Book.NumberInStock;
                }
                else
                {
                    int? borrowed = movieInDb.NumberInStock - movieInDb.NumberAvailable;
                    movieInDb.NumberAvailable = Convert.ToByte(movie.NumberInStock - borrowed);
                }
                movieInDb.NumberInStock = movie.Book.NumberInStock;  //New Stock cannot be less than number available
            }

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = movie.Id });
        }
    }
}