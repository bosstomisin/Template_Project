
using Newtonsoft.Json;
using System.Text;

namespace Template_Project.Utility
{
    public class HttpClientRepository
    {
        public static Tuple<string, StringContent> BuildReqContent<T>(HttpClient client, string _baseURL, T model, string PartOfUrl)
        {
            client.BaseAddress = new Uri(_baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            var serializedModel = JsonConvert.SerializeObject(model);
            var currentUrl = Path.Combine(client.BaseAddress.ToString(), PartOfUrl);
            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
            return new Tuple<string, StringContent>(currentUrl, content);
        }

        public static async Task<Tuple<string, HttpResponseMessage>> PostContentAsync<T>(string _baseURL, T model, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                response = await client.PostAsync(contents.Item1, contents.Item2);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }

        public static async Task<Tuple<string, HttpResponseMessage>> GetContentAsync<T>(string _baseURL, T model, string PartOfUrl)
        {
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var contents = BuildReqContent<T>(client, _baseURL, model, PartOfUrl);
                response = await client.GetAsync(contents.Item1);
            }
            var responseObj = await response.Content.ReadAsStringAsync();
            return new Tuple<string, HttpResponseMessage>(responseObj, response);
        }

    }
}
