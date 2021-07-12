using System;
using System.Collections.Generic;
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
            //Searching records from list using LINQ query  
            var Name = (from N in ObjList
                        where N.Name.ToLower().Contains(Prefix.ToLower())
                        select new { N.Name });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }

        public class City
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}