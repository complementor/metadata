using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDbAccessLayer;

namespace Interface.Api
{
    [Route("api/files")]
    public class ClientApiController : Controller
    {
        private readonly ILogger<ClientApiController> _logger;

        public ClientApiController(ILogger<ClientApiController> logger)
        {
            _logger = logger;
        }

        private static readonly List<VideoInfoDto> list = new List<VideoInfoDto>
        {
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid(),
                Duration = "00:01:10.779",
                Stadnard = "XMP",
                Title = "Goldeneye"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid(),
                Duration = "00:05:20.122",
                Stadnard = "Dublin Core",
                Title = "Football"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid(),
                Duration = "00:02:00.433",
                Stadnard = "MPEG-7",
                Title = "Batman"
            }
        };

        [HttpGet("search")]
        public IActionResult Search()
        {
            return Ok(list);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var file = list
                .Where(x => x.VideoId == guid)
                .FirstOrDefault();

            return Ok(file);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
