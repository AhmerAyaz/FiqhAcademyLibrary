using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult New(int? Id)
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View();
            }
            else
            {
                if(Id.HasValue)
                {
                    var book = _context.Movies.Where(m => m.Id == Id).FirstOrDefault();
                    return View("NewCustomer", book);
                }
                else
                {
                    var book = new Book();
                    return View("NewCustomer", book);
                }
            }
        }
        
        public ActionResult Return(int Id)
        {
            var rental = _context.Rentals.Include(c => c.User).Include(c => c.Movie).FirstOrDefault(c => c.Id == Id);
            rental.DateReturned = DateTime.Now;
            rental.Movie.NumberAvailable++;
            _context.SaveChanges();
            DateTime PaidDate = (DateTime)rental.DateReturned;
            TimeSpan difference = (PaidDate - rental.DateRented);
            int num = difference.Days;
            ViewBag.Difference = num;
            
            
            return View(rental);
        }
    }
}