using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using movieBucketData;

namespace movieBucketWebApi.Controllers
{
    public class ActorController : ApiController
    {

        [HttpGet]
        public IEnumerable<Actor> GetAllActor()
        {
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.Actors.ToList();
            }
        }

        [HttpGet]
        public HttpResponseMessage GetActor(string actorId)
        {
            Guid actorIdGuid = new Guid(actorId);
            movieBucketEntities entity = new movieBucketEntities();
            var actor = entity.Actors.Where(m => m.actor_Id == actorIdGuid).FirstOrDefault();
            if (actor != null)
                return Request.CreateResponse(HttpStatusCode.OK, actor);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Actor detail Not Found");
        }

        [HttpPut]
        public HttpResponseMessage PutActor([FromBody]Actor actor)
        {
            Guid actorId = Guid.NewGuid();
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    actor.actor_Id = actorId;
                    var res = entity.Actors.Add(actor);
                    if (res == null)
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No Data Updated");
                    entity.SaveChanges();
                }
                catch (Exception e)
                {
                    string exceptionMessage = e.Message;
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exceptionMessage);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateActor([FromUri]string actorId, [FromBody]Actor actor)
        {
            string exceptionString = null;
            Guid actorIdGuid = new Guid(actorId);
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    Actor actorData = entity.Actors.Where(m => m.actor_Id == actorIdGuid).FirstOrDefault();
                    if (actorData == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Actor Detail Not found in Database, Please Add detail!!!");
                    }
                    actorData.bio = actor.bio;
                    actorData.date_of_birth = actor.date_of_birth;
                    actorData.name = actor.name;
                    actorData.sex = actor.sex;
                    entity.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, actor);
                }
                catch(Exception e)
                {
                    exceptionString = e.Message;
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exceptionString);
                }    
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteActor(string actorId)
        {
            Guid actorIdGuid = new Guid(actorId);
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    entity.Actors.Remove(entity.Actors.Where(m => m.actor_Id == actorIdGuid).FirstOrDefault());
                }
                catch(Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            
        }
    }
}
