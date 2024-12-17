using KonyvtarHttpClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace KonyvtarHttpClient.Services
{
    public class MegyeService
    {
        public static async Task MegyeById(HttpClient httpClient, int id, Megyek megye)
        {
            string url = $"Megyek/GetById?id={id}";
            Megyek result = await httpClient.GetFromJsonAsync<Megyek>(url);
            Task.Delay(1000).Wait();
            if (result is not null)
            { 
                megye.Id = result.Id;
                megye.MegyeNev = result.MegyeNev;
            }
        }
    }
}
