using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservationFlight.Model.Catalog.Statistics;
using ReservationFlight.Utility;

namespace ReservationFlight.ApiIntegration
{
    public class StatisticApiClient : IStatisticApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public StatisticApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<StatisticViewModel> GetStatisticByMonth(int month)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var response = await client.GetAsync(
                string.Format(
                    Constants.API_STATISTIC,
                    nameof(GetStatisticByMonth)) + month);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<StatisticViewModel>(body);

            return data!;
        }
    }
}
