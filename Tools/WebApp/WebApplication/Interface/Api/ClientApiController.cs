using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Interface.Helpers;
using Interface.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDbAccessLayer;
using MongoDbAccessLayer.Dtos;

namespace Interface.Api
{
    [Route("api/files")]
    public class ClientApiController : Controller
    {
        private readonly IBusinessLogic _businessLogic;

        public ClientApiController(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string query)
        {
            //if (string.IsNullOrWhiteSpace(query) || query == null || query == "undefined")
            //{
            //    return Ok(businessLogic.GetAll());
            //}
            //return Ok(businessLogic.Search(query));

            return Ok(HardcodedData.GetListOfVideos());
        }

        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            //var metadata = businessLogic.Get(guid);

            var list = HardcodedData.GetListOfVideos();
            var file = list
                .Where(x => x.VideoId == guid)
                .FirstOrDefault();
            var metadata = HardcodedData.GetVideoMetadataDto(file.Title);

            var wordCloud = WordCloud.Get(metadata.SpeechAggregated);

            var model = new VideoMetadataViewModel
            {
                VideoMetadataDto = metadata,
                Words = wordCloud
            };

            return Ok(model);
        }

        [HttpPost("video/search")]
        public IActionResult SearchVideoScenes([FromBody] SearchVideoScenesModel model)
        {
            model.SearchQuery = model.SearchQuery.ToLower();

            var filteredScenes = model.Scenes
                .Where(scene => scene?.OCR?.ToLower().Contains(model.SearchQuery) == true
                || scene?.Speech?.ToLower().Contains(model.SearchQuery) == true
                || scene?.Objects?.Any(obj => obj?.Name?.ToLower().Contains(model.SearchQuery) ?? false) == true
                || scene?.Sentiment?.Negative >= 0.8 && model.SearchQuery.Contains("neg")
                || scene?.Sentiment?.Neutral >= 0.8 && model.SearchQuery.Contains("neu")
                || scene?.Sentiment?.Positive >= 0.8 && model.SearchQuery.Contains("pos")
                ).Select(scene => scene);

            return Ok(filteredScenes);
        }

        [HttpGet("provenance")]
        public IActionResult FileProvenance()
        {
            var provenanceData = HardcodedData.GetProvenanceDto();

            return Ok(provenanceData);  
        }
    }
}