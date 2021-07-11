﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appathon_component.Models.Request
{
    public class AddSearchRequest
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
}