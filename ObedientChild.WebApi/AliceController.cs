using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObedientChild.App.Alice;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObedientChild.WebApi
{
    [ApiController]
    [Route("api/v1/alice")]
    public class AliceController : ControllerBase
    {
        private readonly IAliceService _aliceService;

        public AliceController(IAliceService aliceService)
        {
            _aliceService = aliceService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AliceRequest request)
        {
            if (string.IsNullOrEmpty(request.Request?.Command) || request.Session?.New == true)
                return Ok(CreateResponse(request, "Привет! Это навык Послушный ребенок. У меня можно: нарушить правило, прибавить или отнять монетку, выполнить/провалить/пропустить/отменить привычку"));

            var result = await _aliceService.HandleAsync(request.Request.Command, request.Request.Nlu);

            return Ok(CreateResponse(request, result ?? "Я не смогла, попробуйте снова"));
        }

        private AliceResponse CreateResponse(AliceRequest request, string responseText)
        {
            var response = new AliceResponse
            {
                Response = new AliceResponseContent
                {
                    Text = responseText,
                    EndSession = false
                },
                Session = new AliceSession
                {
                    SessionId = request.Session.SessionId,
                    MessageId = request.Session.MessageId,
                    UserId = request.Session.UserId
                },
                Version = request.Version
            };

            return response;
        }
    }

    // Модель запроса от Алисы
    public class AliceRequest
    {
        [JsonProperty("meta")]
        public AliceMeta Meta { get; set; }

        [JsonProperty("request")]
        public AliceRequestContent Request { get; set; }

        [JsonProperty("session")]
        public AliceSession Session { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class AliceMeta
    {

    }

    public class AliceRequestContent
    {
        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("original_utterance")]
        public string OriginalUtterance { get; set; }

        [JsonProperty("nlu")]
        public AliceNaturalLanguageUnderstanding Nlu { get; set; }
    }

    public class AliceSession
    {
        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("new")]
        public bool New { get; set; }
    }

    // Модель ответа для Алисы
    public class AliceResponse
    {
        [JsonProperty("response")]
        public AliceResponseContent Response { get; set; }

        [JsonProperty("session")]
        public AliceSession Session { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class AliceResponseContent
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("end_session")]
        public bool EndSession { get; set; }
    }
}
