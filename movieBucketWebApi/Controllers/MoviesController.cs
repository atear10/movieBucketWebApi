using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using movieBucketData;
using movieBucketWebApi.Models;

namespace movieBucketWebApi.Controllers
{
    public class MoviesController : ApiController
    {
        // GET: api/Movies
        //[HttpGet]
        //public HttpResponseMessage GetAllMovies()
        //{
        //    using (movieBucketEntities entity = new movieBucketEntities())
        //    {
        //        try
        //        {
        //            var movies = entity.Movies.ToList();
        //            //movies[0].producer = null;
        //            return Request.CreateResponse(HttpStatusCode.OK, movies);
        //        }
        //        catch(Exception e)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
        //        }
        //    }
        //}

        [HttpGet]
        public IEnumerable<Movie> GetAllMovies()
        {
            movieBucketEntities entity = new movieBucketEntities();
                return entity.Movies.AsEnumerable();
        }

        // POST: api/Movies
        [HttpPost]
        public HttpResponseMessage UpdateMovie([FromUri]Guid movieId, [FromBody]Movie mov)
        {
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                var data = entity.Movies.Where(m => m.movie_id == movieId).FirstOrDefault();
                if (data == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movie does not Exist in Database");
                }
                data.movie_name = mov.movie_name;
                data.plot = mov.plot;
                data.poster = mov.poster;
                data.producer = mov.producer;
                data.producer_id = mov.producer_id;
                data.year_of_release = mov.year_of_release;
                //data.Actors = mov.Actors;2
                foreach (var act in data.Actors)
                {
                    var actorDetail = entity.Actors.Where(m => m.actor_Id == act.actor_Id).FirstOrDefault();
                    actorDetail.Movies.Add(mov);
                }
                entity.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteMovie(string movieId)
        {
            Guid movieIdGuid = new Guid(movieId);
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    var data = entity.Movies.Where(m => m.movie_id == movieIdGuid).FirstOrDefault();
                    entity.Movies.Remove(data);
                    entity.SaveChanges();
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPut]
        public HttpResponseMessage addMovie(Movie movie)
        {
            Movie mov = new Movie();
            Guid movieId = Guid.NewGuid();
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                mov.movie_id = movieId;
                mov.movie_name = movie.movie_name;
                mov.plot = movie.plot;
                mov.producer_id = movie.producer_id;
                mov.year_of_release = movie.year_of_release;
                //mov.Actors.Add(movie.Actors.First());
                mov.poster = movie.poster;
                try
                {
                    entity.Movies.Add(mov);
                    foreach (var act in movie.Actors)
                    {
                        var actorDetail = entity.Actors.Where(m => m.actor_Id == act.actor_Id).FirstOrDefault();
                        actorDetail.Movies.Add(mov);
                    }
                    //entity.Movies.Attach(mov);
                    entity.SaveChanges();
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
        }
    }
}