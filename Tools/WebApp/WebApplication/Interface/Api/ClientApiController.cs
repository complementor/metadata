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
        private readonly IBusinessLogic businessLogic;

        public ClientApiController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        private static readonly List<VideoInfoDto> list = new List<VideoInfoDto>
        {
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid().ToString(),
                Duration = "00:01:10.779",
                Standard = "XMP",
                Title = "Goldeneye"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid().ToString(),
                Duration = "00:05:20.122",
                Standard = "Dublin Core",
                Title = "Football"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid().ToString(),
                Duration = "00:02:00.433",
                Standard = "MPEG-7",
                Title = "Batman"
            }
        };

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string query)
        {
            //if (string.IsNullOrWhiteSpace(query) || query == null || query == "undefined")
            //{
            //    return Ok(businessLogic.GetAll());
            //}
            //return Ok(businessLogic.Search(query));

            return Ok(list);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(string guid)
        {
            //var metadata = businessLogic.Get(guid);

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
            //WebClient client = new WebClient();
            //client.Headers["Accept"] = "application/json";

            //string json = client.DownloadString(new Uri("https://escience.aip.de/prov/graphs/example.json"));

<<<<<<< HEAD
            var provenanceData = HardcodedData.GetProvenanceDto();
=======
            //string json = client.DownloadString(new Uri("https://escience.aip.de/prov/graphs/example.json"));

            var json = @"
            {""nodes"": [
                { ""type"": ""agent"", ""name"": ""Andrei"", ""value"": 0.2},
                { ""type"": ""entity"", ""name"": ""Document_v2"", ""value"": 0.2},
                { ""type"": ""activity"", ""name"": ""Tagging"", ""value"": 0.2},
                { ""type"": ""entity"", ""name"": ""Document_v3"", ""value"": 0.2},
                { ""type"": ""agent"", ""name"": ""SceneDetect"", ""value"": 0.2}, 
                { ""type"": ""activity"", ""name"": ""ExtractFeatures"", ""value"": 0.2},
                { ""type"": ""activity"", ""name"": ""ExtractGenericMetadata"", ""value"": 0.2},
                { ""type"": ""agent"", ""name"": ""ExifTool"", ""value"": 0.2},
                { ""type"": ""entity"", ""name"": ""Document_v1"", ""value"": 0.2}
                ],
            ""links"": [
                {""source"": 1, ""type"": ""wasGeneratedBy"", ""target"": 2, ""value"": 0.2 }, 
                {""source"": 2, ""type"": ""wasAssociatedWith"", ""target"": 0, ""value"": 0.2 }, 
                {""source"": 2, ""type"": ""used"", ""target"": 8, ""value"": 0.2}, 

                {""source"": 2, ""type"": ""wasAttributedTo"", ""target"": 7, ""value"": 0.2}, 
                {""source"": 3, ""type"": ""wasAssociatedWith"", ""target"": 4, ""value"": 0.2}, 
                {""source"": 5, ""type"": ""used"", ""target"": 1, ""value"": 0.2},
                {""source"": 3, ""type"": ""wasGeneratedBy"", ""target"": 5, ""value"": 0.2},
                {""source"": 3, ""type"": ""wasDerivedFrom"", ""target"": 1, ""value"": 0.2},

                {""source"": 6, ""type"": ""wasAttributedTo"", ""target"": 4, ""value"": 0.2}, 
                {""source"": 8, ""type"": ""wasAssociatedWith"", ""target"": 7, ""value"": 0.2}, 
                {""source"": 8, ""type"": ""wasGeneratedBy"", ""target"": 6, ""value"": 0.2},
                {""source"": 1, ""type"": ""wasDerivedFrom"", ""target"": 8, ""value"": 0.2}
            ]}
            ";

            //var json = @"
            //{""nodes"": [
            //{ ""type"": ""entity"", ""name"": ""Document_v1"", ""value"": 0.2},
            //{ ""type"": ""entity"", ""name"": ""Document_v2"", ""value"": 0.2}, 
            //{ ""type"": ""entity"", ""name"": ""Document_v3"", ""value"": 0.2} 
            //], 
            //""links"": [
            //    {""source"": 1, ""type"": ""wasDerivedFrom"", ""target"": 0, ""value"": 0.2},
            //    {""source"": 2, ""type"": ""wasDerivedFrom"", ""target"": 1, ""value"": 0.2}
            //]}
            //";
>>>>>>> 58402761ff14e94584da7c9dd8ca5369da2c4419

            return Ok(provenanceData);  
        }
    }
}