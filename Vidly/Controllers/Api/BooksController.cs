using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        //Ahmer
        //public IEnumerable<MovieDto> GetMoviesByID(string query = null)
        //{
        //    var moviesQuery = _context.Movies
        //        .Where(m => m.NumberAvailable > 0);


        //    if (!String.IsNullOrWhiteSpace(query))
        //    {
        //        var num = Convert.ToInt32(query);
        //        moviesQuery = moviesQuery.Where(m => m.Id == num);
        //    }
                

        //    return moviesQuery
        //        .ToList()
        //        .Select(Mapper.Map<Book, MovieDto>);
        //}

        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                bool isIntString = query.All(char.IsDigit);
                if(isIntString)
                {
                    var id = Convert.ToInt32(query);
                    moviesQuery = moviesQuery.Where(m => m.Id == id);
                }
                else
                {
                    moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
                }
            }
                

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Book, MovieDto>);
        }


        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Book, MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Book>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
