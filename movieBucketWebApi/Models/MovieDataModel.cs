using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using movieBucketData;
using movieBucketWebApi;

namespace movieBucketWebApi.Models
{
    public class MovieDataModel
    {
        public Guid movieId { get; set; }
        public string movieName { get; set; }
        public string plot { get; set; }
        public byte[] poster { get; set; }
        public Guid producerId { get; set; }
        public string fileName { get; set; }
        public int yearOfRelease { get; set; }
        public List<Actor> actor { get; set; }
    }
}