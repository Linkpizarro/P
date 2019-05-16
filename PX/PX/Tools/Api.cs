using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PX.Tools
{
    public class Api
    {
        public static String uriapi = "https://tiendaapi.azurewebsites.net/";
        public static MediaTypeWithQualityHeaderValue headerjson = new MediaTypeWithQualityHeaderValue("application/json");

        public static async Task<T> CallApi<T>(String peticion, String token)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(uriapi);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(headerjson);
                if (token != null)
                {
                    cliente.DefaultRequestHeaders.Add("Authorization", "bearer "
                        + token);
                }
                HttpResponseMessage response = await cliente.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    String content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                return default(T);
            }
        }
        public static async Task PostApi(Object obj, String peticion, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer "
                        + token);
                }
                String stringJson = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(peticion, content);
            }
        }
        public static async Task PutApi(Object obj, String peticion, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer "
                        + token);
                }
                String stringJson = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(stringJson, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PutAsync(peticion, content);
            }
        }
        public static async Task DeleteApi(String peticion, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer "
                        + token);
                }
                HttpResponseMessage responseMessage = await client.DeleteAsync(peticion);
            }
        }
        public static async Task<String> GetToken(String usuario, String password)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "login";
                client.BaseAddress = new Uri(uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type","password"),
                    new KeyValuePair<string, string>("username", usuario),
                    new KeyValuePair<string, string>("password", password)
                });
                HttpResponseMessage response = await client.PostAsync(peticion, content);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(contenido);
                    String token = json.GetValue("access_token").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
