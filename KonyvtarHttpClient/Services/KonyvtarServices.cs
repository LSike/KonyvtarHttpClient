using KonyvtarHttpClient.DTOs;
using KonyvtarHttpClient.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace KonyvtarHttpClient.Services
{
    internal class KonyvtarServices
    {
        public static async Task<List<KonyvtarakDTO>> GetAll(HttpClient httpClient)
        {
            return await httpClient.GetFromJsonAsync<List<KonyvtarakDTO>>("Konyvtarak/GetDTO");
        }

        public static async Task<string> Delete(HttpClient httpClient, int id)
        {
            try
            {
                string uri = $"{httpClient.BaseAddress}Konyvtarak?id={id}";                
                var response = await httpClient.DeleteAsync(uri);
                var valasz =  await response.Content.ReadAsStringAsync();
                return valasz;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> Post(HttpClient httpClient, Konyvtarak ujKonyvtar)
        {
            try
            {
                string uj = JsonSerializer.Serialize(ujKonyvtar, JsonSerializerOptions.Default);
                string url = $"{httpClient.BaseAddress}Konyvtarak";
                var request = new StringContent(uj, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, request);
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return content;
                }
                else
                {
                    return $"Hiba: {response.StatusCode}\n {response.Content.Headers}\n{content}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> Put(HttpClient httpClient, Konyvtarak ujKonyvtar)
        {
            try
            {
                //A HTTP kérés részei a kódban
                //line --> Method: PUT, url, (HTTP verzió kezelve)
                //head --> Host (beépült a base url-be, azon keresztül az url-be)
                //head --> Content-Type application/json, UTF8
                //body --> serializált Json formátumban az ujKonyvtar

                //a kérés előkésszítése
                //url
                string url = $"{httpClient.BaseAddress}Konyvtarak";
                //body, mivel put kérésnél ez is kell
                string uj = JsonSerializer.Serialize(ujKonyvtar, JsonSerializerOptions.Default);
                var requestBody = new StringContent(uj, Encoding.UTF8, "application/json");
                //elküldjük a kérést, itt dől el, hogy PUT a metódus, a response a választ tartalmazza későbbi feldolgozásra
                var response = await httpClient.PutAsync(url, requestBody);
                //kiolvassuk a válasz adatait, ami a backend végpontnál beállított szöveges üzenet, és a StatusCode alapján kiíratunk (ez nem lenne szükséges, a válasz elég informatív így is)
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return content;
                }
                else
                {
                    return $"Hiba: {response.StatusCode}\n {response.Content.Headers}\n{content}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
