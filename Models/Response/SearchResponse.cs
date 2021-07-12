using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appathon_component.Models.Response
{
    public class SearchResponse
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public Shards _shards { get; set; }
        public Hit hits { get; set; }
    }
    public class Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int skipped { get; set; }
        public int failed { get; set; }
    }

    public class Total
    {
        public int value { get; set; }
        public string relation { get; set; }
    }

    public class Source
    {
        public int id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public int rate { get; set; }
        public int ratedBy { get; set; }
        public DateTime datePublished { get; set; }
        public string publishedDate { get; set; }
    }

    public class Hit
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }
        public Source _source { get; set; }
        public Total total { get; set; }
        public double max_score { get; set; }
        public List<Hit> hits { get; set; }
    }

}