//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace movieBucketData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movie()
        {
            this.Actors = new HashSet<Actor>();
        }
    
        public System.Guid movie_id { get; set; }
        public string movie_name { get; set; }
        public decimal year_of_release { get; set; }
        public string plot { get; set; }
        public byte[] poster { get; set; }
        public System.Guid producer_id { get; set; }
    
        public virtual producer producer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Actor> Actors { get; set; }
    }
}