using appathon_component.Models.Request;
using appathon_component.Models.Response;
using Newtonsoft.Json;
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
        public JsonResult Text(string term)
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
            qstr.query = "*" + term + "*";
            qstr.default_field = "title";
            qi.query_string = qstr;
            searchModel.query = qi;
            string url = string.Concat(ConfigurationManager.AppSettings["ApiBaseUrl"].ToString(), "/", ConfigurationManager.AppSettings["IndexName"].ToString(), "/", ConfigurationManager.AppSettings["search"].ToString(), "/");
            var response = ApiCall.apiCall(url, "POST", searchModel);

            var result = JsonConvert.DeserializeObject<SearchResponse>(response);
            if (result == null)
            {
                // no result
                return Json("", JsonRequestBehavior.AllowGet);
            }

            if (result.hits != null)
            {
                if (result.hits.hits.Count() == 0)
                {
                    // no result
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                 var name = result.hits.hits.Select(x => new { Imagepath = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png", Name = x._source.title }).ToList();

                //var name = result.hits.hits.Select(x => new
                //{
                //    id = x._source.title,
                //    CountryName = x._source.title,
                //    Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png"
                //    // label = $"< a href = \"javascript:void(0);\" >< img src = \"\" width = \"50\" height = \"50\" />< span > " + x._source.title + "</ span ></ a > "
                //}).ToList();
                return Json(name, JsonRequestBehavior.AllowGet);

            }
            // Searching records from list using LINQ query
            //var Name = (from N in ObjList
            //            where N.Name.ToLower().Contains(Prefix.ToLower())
            //            select new { N.Name });
            //return Json(Name, JsonRequestBehavior.AllowGet);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}