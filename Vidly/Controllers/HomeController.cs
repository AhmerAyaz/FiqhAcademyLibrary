using System;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                var numberOfBooks = dbContext.Movies.Count();
                var booksRented = dbContext.Rentals.Where(r => r.DateReturned == DateTime.MinValue).Count();
                var numberOfRequests = dbContext.Requests.Count();
                AdminViewModel adminViewModel = new AdminViewModel()
                {
                    Books = numberOfBooks,
                    Requests = numberOfRequests,
                    Rentals = booksRented
                };
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Index","Books");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}