using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ObedientChild.WebApi
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TasksController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetExternalData()
        {
            try
            {
                var url = "https://www.dropbox.com/scl/fi/q9dqqu1u37hvgbhub3bgm/tasks.opml?rlkey=5pnianllu0eoz5ex2y0uqi3mo&dl=1";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error fetching data from Dropbox.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/xml");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
