using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appathon_component.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using FFImageLoading;
using Elasticsearch.Net;
using Nest.Specification;

namespace appathon_component.Controllers
{
    public class HomeController : Controller
    {
        ImageService imageService = new ImageService();
        //CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=myteststorage2021;AccountKey=4MLvpT3UvxrjeG6I7NYjaKGlYHqpPbfdmPpkgyXKGR6nQ8qgd4k9XMC5Q+aY51l1m4Ja2S4tatlrgzY7TBvyQw==;EndpointSuffix=core.windows.net");

        public ActionResult Index()
        {
            Product product = Product.GetProduct();
            return View(product);
        }

       
        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo, Product prod)
        {
            var imageUrl = await imageService.UploadImageAsync(photo);
            prod.ProductImage = imageUrl;
            return RedirectToAction("Upload");

        }

        public ActionResult Upload(Product pro)
        {
            List<appathon_component.Models.ImageBlob> blobs = new List<appathon_component.Models.ImageBlob>();
            blobs= imageService.ListAll();
            ViewData["blobs"] = blobs;
            return View();
        }

        [HttpPost]
        public string DownloadMe(string Name)
        {
            imageService.AzureFileDownload(Name);
            return "File Downloaded";
        }

        [HttpPost]
        public string Delete(string Name)
        {
            string FileName = System.IO.Path.GetFileName(Name);
            imageService.DeleteFile(Name);
            return "File Deleted";
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      

         
    }
}