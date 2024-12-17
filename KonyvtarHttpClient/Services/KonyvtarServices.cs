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
            //var konyvtarLista = await httpClient.GetFromJsonAsync<List<KonyvtarakDTO>>(
            //    "Konyvtarak/GetDTO");
            //feltoltendo.Clear();
            //konyvtarLista.ForEach(feltoltendo.Add);
        }

        public static async Task Delete(HttpClient httpClient, int id)
        {
            try
            {
                string uri = $"{httpClient.BaseAddress}Konyvtarak?id={id}";                
                var response = await httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sikeres törlés");
                }
                else
                {
                    MessageBox.Show("Hiba");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task Post(HttpClient httpClient, Konyvtarak ujKonyvtar)
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
                    MessageBox.Show($"Sikeres mentés. {content}");
                }
                else
                {
                    MessageBox.Show($"Hiba: {response.StatusCode}\n {response.Content.Headers}\n{content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task Put(HttpClient httpClient, Konyvtarak ujKonyvtar)
        {
            try
            {
                string uj = JsonSerializer.Serialize(ujKonyvtar, JsonSerializerOptions.Default);
                string url = $"{httpClient.BaseAddress}Konyvtarak";
                var request = new StringContent(uj, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url, request);
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Sikeres mentés. {content}");
                }
                else
                {
                    MessageBox.Show($"Hiba: {response.StatusCode}\n {response.Content.Headers}\n{content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
