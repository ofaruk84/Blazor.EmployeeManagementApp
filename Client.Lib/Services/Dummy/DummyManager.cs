using Client.Lib.Utilities.Http;
using Shared.Lib.Entities.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib.Services.Dummy
{
    //Will Be removed
    public class DummyManager : IDummyService
    {
        private readonly IHttpClientUtil _httpClientUtil;
        private readonly string url = "api/weatherforecast";


        private  readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public DummyManager(IHttpClientUtil httpClientUtil)
        {
            _httpClientUtil = httpClientUtil;
        }

        public async Task<WeatherForecast[]?> GetWeatherForecasts()
        {
            
            var httpClient = await _httpClientUtil.CreateAuthenticatedHttpClient();
            var result =  await httpClient.GetFromJsonAsync<WeatherForecast[]>(url);

            return result;
            
        }
    }
}
