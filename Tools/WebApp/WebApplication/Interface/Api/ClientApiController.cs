using System.Linq;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDbAccessLayer;

namespace Interface.Api
{
    [Route("api/files")]
    public class ClientApiController : Controller
    {
        private readonly IBusinessLogic businessLogic;

        public ClientApiController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query == null || query == "undefined")
            {
                return Ok(businessLogic.GetAll());
            }
            return Ok(businessLogic.Search(query));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            return Ok(businessLogic.Get(guid));
        }

        [HttpPost("video/search")]
        public IActionResult SearchVideoScenes([FromBody] SearchVideoScenesModel model)
        {
            model.SearchQuery = model.SearchQuery.ToLower();

            var filteredScenes = model.Scenes
                .Where(scene => scene.OCR.ToLower().Contains(model.SearchQuery)
                || scene.Speech.ToLower().Contains(model.SearchQuery)
                || scene.Objects.Any(obj => obj.Name.ToLower().Contains(model.SearchQuery))
                || scene.Sentiment.Negative >= 0.8 && model.SearchQuery.Contains("neg")
                || scene.Sentiment.Neutral >= 0.8 && model.SearchQuery.Contains("neu")
                || scene.Sentiment.Positive >= 0.8 && model.SearchQuery.Contains("pos")
                ).Select(scene => scene);

            return Ok(filteredScenes);
        }
    }
}
