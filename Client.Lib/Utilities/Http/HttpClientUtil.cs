using Client.Lib.Utilities.LocalStorage;
using Shared.Lib.DTOs;
using System.Net.Http.Headers;

namespace Client.Lib.Utilities.Http
{
    public class HttpClientUtil : IHttpClientUtil
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppLocalStorageService _appLocalStorageService;

        public HttpClientUtil(IHttpClientFactory httpClientFactory, IAppLocalStorageService appLocalStorageService)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _appLocalStorageService = appLocalStorageService ?? throw new ArgumentNullException(nameof(appLocalStorageService));
            
        }

        public async Task<HttpClient> CreateAuthenticatedHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.systemHttpClient);
            var stringToken = await _appLocalStorageService.GetItem(Constants.authKey);
            if (string.IsNullOrEmpty(stringToken)) return httpClient;

            try
            {
                var token = SerializationsUtil.DeserializeObj<UserSessionDto>(stringToken);
                if (token is null) return httpClient;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                return httpClient;
            }


            return httpClient;

        }

        public HttpClient CreatePublicHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient(Constants.systemHttpClient);
            httpClient.DefaultRequestHeaders.Remove(Constants.authHeader);

            return httpClient;
        }

    }
}
