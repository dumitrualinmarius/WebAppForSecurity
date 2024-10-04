using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using WebAppForSecurity.Authorization;
using WebAppForSecurity.DTO;

namespace WebAppForSecurity.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
    public class HRManagerModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO> weatherForecast { get; set; } = new List<WeatherForecastDTO>();

        public HRManagerModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task OnGetAsync()
        {
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");

            var res = await httpClient.PostAsJsonAsync("auth", new Credential { UserName = "admin", Password = "password" });
            res.EnsureSuccessStatusCode();
            string strJwt = await res.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtToken>(strJwt);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken ?? string.Empty);

            weatherForecast = 
                await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")
                ?? new List<WeatherForecastDTO>();
        }
    }

}
