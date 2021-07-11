using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appathon_component.Models
{
    public class ImageBlob
    {
        public Uri Uri { get; set; }
        public StorageUri StorageUri { get; set; }
        public CloudBlobDirectory Parent { get; set; }
        public CloudBlobContainer Container { get; set; }
 
    }

}