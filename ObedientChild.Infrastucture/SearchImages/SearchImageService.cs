using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ObedientChild.Infrastructure.SearchImages
{
    public class SearchImageService : ISearchImageService
    {
        private readonly SerpapiOptions _options;

        public SearchImageService(SerpapiOptions options)
        {
            _options = options;
        }

        public async Task<SearchImagesResult> SearchAsync(string searchText)
        {
            var client = new HttpClient();
            var result = await client.GetFromJsonAsync<SearchImagesResult>(
                $"https://serpapi.com/search.json?q={searchText}&tbm=isch&ijn=0&api_key={_options.ApiKey}");

            return result;
        }
    }
}
