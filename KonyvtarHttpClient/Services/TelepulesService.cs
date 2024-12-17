using KonyvtarHttpClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace KonyvtarHttpClient.Services
{
    internal class TelepulesService
    {
        public static async Task GetAll(HttpClient httpClient, List<Telepulesek> feltoltendo)
        {
            var lista = await httpClient.GetFromJsonAsync<List<Telepulesek>>("Telepulesek");
            feltoltendo.Clear();
            lista.ForEach(feltoltendo.Add);
        }

        public static async Task GetByIrsz(HttpClient httpClient, Telepulesek keresett, int irsz)
        {
            Telepulesek result = await httpClient.GetFromJsonAsync<Telepulesek>($"Telepulesek/GetByIrsz?irsz={irsz}");
            Task.Delay(1000).Wait();
            if (result is not null)
            {
                keresett.Id = result.Id;
                keresett.Irsz = result.Irsz;
                keresett.TelepNev = result.TelepNev;
                keresett.MegyeId = result.MegyeId;
                keresett.Megye = result.Megye;
            }
        }
    }
}
