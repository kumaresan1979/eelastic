using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appathon_component.Models.Request
{
    //public class Match
    //{
    //    public string title { get; set; }
    //}

    //public class Query
    //{
    //    public Match match { get; set; }
    //}



    //public class SearchModel
    //{
    //    public Query query { get; set; }
    //}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class QueryString
    {
        public string default_field { get; set; }
        public string query { get; set; }
    }

    public class Query
    {
        public QueryString query_string { get; set; }
    }

    public class SearchModel
    {
        public Query query { get; set; }
    }


}
