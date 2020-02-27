using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Interface.Helpers;
using Interface.Models;
using Interface.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDbAccessLayer;
using MongoDbAccessLayer.Context.Repository;
using MongoDbAccessLayer.Dtos;

namespace Interface.Api
{
    [Route("api/files")]
    public class ClientApiController : Controller
    {
        private readonly IBusinessLogic _businessLogic;
        private readonly IProvenanceRepository _provenanceRepository;

        public ClientApiController(IBusinessLogic businessLogic, IProvenanceRepository provenanceRepository)
        {
            _provenanceRepository = provenanceRepository;
            _businessLogic = businessLogic;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string property, [FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(property) || property == null || property == "undefined" || property == "null"
                || string.IsNullOrWhiteSpace(query) || query == null || query == "undefined" || query == "null")
            {
                return Ok(_businessLogic.GetAll());
                //return Ok(HardcodedData.GetListOfVideos());
            }

            return Ok(_businessLogic.Search(query));
            //return Ok(HardcodedData.GetListOfVideos());
        }

        [HttpGet("genericproperties")]
        public IActionResult GetExistentGenericProperties()
        {
            return Ok(HardcodedData.GetGenericPropertiesDto());
        }

        [HttpGet("{guid}")]
        public IActionResult GetAllMetadata(string guid)
        {
            var metadata = _businessLogic.Get(guid);

            //var metadata = HardcodedData.GetVideoMetadataDto(guid);

            var wordCloud = WordCloud.Get(metadata.SpeechAggregated);

            var collaboration = HardcodedData.GetCollaborationDto();

            //var provenance = HardcodedData.GetProvenanceDto();
            var provenance = _provenanceRepository.Get("5e5648f60042cf1df5214403");

            var model = new VideoMetadataViewModel
            {
                VideoMetadataDto = metadata,
                Words = wordCloud,
                Collaboration = collaboration,
                Provenance = provenance
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
    }
}