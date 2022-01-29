using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();    
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (!User.IsInRole(RoleName.CanManageMovies))
            {
                var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

                foreach (var movie in movies)
                {
                    if (movie.NumberAvailable == 0)
                        return BadRequest("Book is not available.");

                    //movie.NumberAvailable--;

                    var customer = _context.Users.Single(
                c => c.UserName == User.Identity.Name);
                    var request = new Request
                    {
                        UserId = customer.Id,
                        User = customer,
                        Book = movie,
                        BookId = movie.Id,
                        RequestDate = DateTime.Now
                    };

                    _context.Requests.Add(request);
                }
            }
            else
            {
                var customer = _context.Users.Single(
                c => c.Id == newRental.CustomerId);

                var movies = _context.Movies.Where(
                    m => newRental.MovieIds.Contains(m.Id)).ToList();

                foreach (var movie in movies)
                {
                    if (movie.NumberAvailable == 0)
                        return BadRequest("Movie is not available.");

                    movie.NumberAvailable--;

                    var rental = new Rental
                    {
                        UserId = customer.Id,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };

                    _context.Rentals.Add(rental);
                }
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
