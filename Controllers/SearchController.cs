using appathon_component.Models.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appathon_component.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            List<City> ObjList = new List<City>()
            {

                new City {Id=1,Name="Latur" },
                new City {Id=2,Name="Mumbai" },
                new City {Id=3,Name="Pune" },
                new City {Id=4,Name="Delhi" },
                new City {Id=5,Name="Dehradun" },
                new City {Id=6,Name="Noida" },
                new City {Id=7,Name="New Delhi" }

            };
            SearchModel searchModel = new SearchModel();
            Query qi = new Query();
            QueryString qstr = new QueryString();
            // Match match =. new Match();
            qstr.query = "*"+ Prefix+ "*";
            qstr.default_field = "title";
            qi.query_string = qstr;
            searchModel.query = qi;
            string url = string.Concat(ConfigurationManager.AppSettings["ApiBaseUrl"].ToString(), "/", ConfigurationManager.AppSettings["IndexName"].ToString(), "/", ConfigurationManager.AppSettings["search"].ToString(), "/");
            var responce = ApiCall.apiCall(url, "POST", searchModel);
            

            // Searching records from list using LINQ query
            var Name = (from N in ObjList
                        where N.Name.ToLower().Contains(Prefix.ToLower())
                        select new { N.Name });
            //string url = string.Concat(ConfigurationManager.AppSettings["ApiBaseUrl"].ToString(), "/", ConfigurationManager.AppSettings["All"].ToString(), "/", prod.ProductName.ToLower());
            //ApiCall.apiCall(url, "POST", request);

            return Json(Name, JsonRequestBehavior.AllowGet);
        }

        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}