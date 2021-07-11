using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace appathon_component
{
    public class ConnectionString
    {
        static string account = CloudConfigurationManager.GetSetting("StorageAccountName");
        static string key = CloudConfigurationManager.GetSetting("StorageAccountKey");
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account, key);
            return CloudStorageAccount.Parse(connectionString);
        }
    }

    public class ApiCall
    {
        public static string apiCall(string apiUrl, string method, object data)
        {
            try
            {
                string responseStr;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseaddress"].ToString());
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", ConfigurationManager.AppSettings["R_UserName"].ToString(), ConfigurationManager.AppSettings["R_Password"].ToString()))));
                    if (method == "POST")
                    {
                        var response = client.PostAsJsonAsync(apiUrl, data).Result;
                        responseStr = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        responseStr = client.GetStringAsync(apiUrl).Result;
                    }
                    return responseStr;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }

}