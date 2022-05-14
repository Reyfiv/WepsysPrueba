using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppDesktop.Services
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public void InitializeClient()
        {

            string UrlBase = "https://localhost:44339/";

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(UrlBase);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
        }
    }
}
