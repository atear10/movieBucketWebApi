using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using movieBucketData;

namespace movieBucketWebApi.Controllers
{
    public class ProducerController : ApiController
    {

        [HttpGet]
        public IEnumerable<producer> GetAllProducer()
        {
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                entity.Configuration.ProxyCreationEnabled = false;
                return entity.producers.ToList();
            }
        }

        // GET: api/Producer
        [HttpGet]
        public HttpResponseMessage GetProducer(string producerId)
        {
            producer prod = null;
            Guid producerIdGuid = new Guid(producerId);
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    prod = entity.producers.Where(m => m.producer_id == producerIdGuid).FirstOrDefault();
                    if(prod == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Producer data not available");
                    }
                }
                catch(Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                return Request.CreateResponse(HttpStatusCode.OK, prod);
            }
        }

        // POST: api/Producer
        [HttpPost]
        public HttpResponseMessage UpdateProducer([FromUri]string producerId, [FromBody]producer pr)
        {
            producer prod = null;
            Guid producerIdGuid = new Guid(producerId);
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    prod = entity.producers.Where(m => m.producer_id == producerIdGuid).FirstOrDefault();
                    if (prod == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Producer Detail not found In Database, Please Add new Detail !!!");
                    }
                    prod.bio = pr.bio;
                    prod.date_of_birth = pr.date_of_birth;
                    prod.name = pr.name;
                    prod.sex = pr.sex;
                    prod.Movies = pr.Movies;
                    entity.SaveChanges();
                }
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                return Request.CreateResponse(HttpStatusCode.OK, prod);
            }
        }

        // PUT: api/Producer/5
        [HttpPut]
        public HttpResponseMessage PutProducer([FromBody]producer prod)
        {
            prod.producer_id = Guid.NewGuid();
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    entity.producers.Add(prod);
                    entity.SaveChanges();
                }
                catch(Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
                }
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
        }

        // DELETE: api/Producer/5
        [HttpDelete]
        public HttpResponseMessage DeleteProducer([FromUri]string producerId)
        {
            Guid producerIdGuid = new Guid(producerId);
            using (movieBucketEntities entity = new movieBucketEntities())
            {
                try
                {
                    var producer = entity.producers.Where(m => m.producer_id == producerIdGuid).FirstOrDefault();
                    entity.producers.Remove(producer);
                    entity.SaveChanges();
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
