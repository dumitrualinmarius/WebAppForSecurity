using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
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
            weatherForecast = 
                await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast")
                ?? new List<WeatherForecastDTO>();
        }
    }

}
