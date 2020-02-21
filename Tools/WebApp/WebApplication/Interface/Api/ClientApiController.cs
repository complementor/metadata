using System;
using System.Collections.Generic;
using System.Linq;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDbAccessLayer.Dtos;

namespace Interface.Api
{
    [Route("api/files")]
    public class ClientApiController : Controller
    {
        private static readonly List<VideoInfoDto> list = new List<VideoInfoDto>
        {
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid(),
                Duration = "00:01:10.779",
                Standard = "XMP",
                Title = "Goldeneye"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid(),
                Duration = "00:05:20.122",
                Standard = "Dublin Core",
                Title = "Football"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid(),
                Duration = "00:02:00.433",
                Standard = "MPEG-7",
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
            var metadata = GetVideoMetadata(guid);

            return Ok(metadata);
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

        private VideoMetadataDto GetVideoMetadata(Guid guid)
        {
            var file = list
                .Where(x => x.VideoId == guid)
                .FirstOrDefault();

            int sceneNumber = 1;

            var metadata = new VideoMetadataDto
            {
                Title = file.Title,
                Duration = 200,
                YouTubeId = "tgbNymZ7vqY",
                OCRAggregated = "Der er mange tilgængelige udgaver af Lorem Ipsum, men de fleste udgaver har gennemgået forandringer, når nogen har tilføjet humor eller tilfældige ord, som på ingen måde ser ægte ud. Hvis du skal bruge en udgave af Lorem Ipsum, skal du sikre dig, at der ikke indgår noget pinligt midt i teksten. Alle Lorem Ipsum-generatore på nettet har en tendens til kun at dublere små brudstykker af Lorem Ipsum efter behov, hvilket gør dette til den første ægte generator på internettet. Den bruger en ordbog på over 200 ord på latin kombineret med en håndfuld sætningsstrukturer til at generere sætninger, som ser pålidelige ud. Resultatet af Lorem Ipsum er derfor altid fri for gentagelser, tilføjet humor eller utroværdige ord osv.",
                SpeechAggregated = "Lorem Ipsum er ganske enkelt fyldtekst fra print- og typografiindustrien. Lorem Ipsum har været standard fyldtekst siden 1500-tallet, hvor en ukendt trykker sammensatte en tilfældig spalte for at trykke en bog til sammenligning af forskellige skrifttyper. Lorem Ipsum har ikke alene overlevet fem århundreder, men har også vundet indpas i elektronisk typografi uden væsentlige ændringer. Sætningen blev gjordt kendt i 1960'erne med lanceringen af Letraset-ark, som indeholdt afsnit med Lorem Ipsum, og senere med layoutprogrammer som Aldus PageMaker, som også indeholdt en udgave af Lorem Ipsum.",
                Generics = new List<GenericAttribute>
                {
                    new GenericAttribute
                    {
                        Name = "generic1",
                        Value = "value1"
                    },
                    new GenericAttribute
                    {
                        Name = "generic2",
                        Value = "value2"
                    }
                },
                Scenes = new List<Scene>
                {
                    new Scene
                    {
                        StartTime = DateTime.Now,
                        StartTimeSeconds = 200,
                        EndTime = DateTime.Now.AddSeconds(10),
                        EndTimeSeconds = 205,
                        FrameStart = 1,
                        FrameEnd = 1400,
                        SceneNumber = sceneNumber++,
                        OCR = "de fleste udgaver ",
                        Speech = "ikke alene overlevet fem århundreder,",
                        Sentiment = new Sentiment
                        {
                            Negative = 1.0,
                            Neutral = 0.0,
                            Positive = 0.0
                        },
                        Objects = new List<CommonObject>
                        {
                            new CommonObject
                            {
                                Name = "Car",
                                Confidence = "0.90101020202020"
                            },
                            new CommonObject
                            {
                                Name = "Tv",
                                Confidence = "0.60101020202020"
                            }
                        }
                    },
                    new Scene
                    {
                        StartTime = DateTime.Now,
                        StartTimeSeconds = 20,
                        EndTime = DateTime.Now.AddSeconds(10),
                        EndTimeSeconds = 25,
                        FrameStart = 1,
                        FrameEnd = 1400,
                        SceneNumber = sceneNumber++,
                        OCR = "tilgængelige udgaver af Lorem Ipsum,",
                        Speech = "fyldtekst fra print- og typografiindustrien.",
                        Sentiment = new Sentiment
                        {
                            Negative = 0.0,
                            Neutral = 1.0,
                            Positive = 0.0
                        },
                        Objects = new List<CommonObject>
                        {
                            new CommonObject
                            {
                                Name = "Dog",
                                Confidence = "0.90101020202020"
                            },
                            new CommonObject
                            {
                                Name = "Cat",
                                Confidence = "0.60101020202020"
                            }
                        }
                    },
                    new Scene
                    {
                        StartTime = DateTime.Now,
                        StartTimeSeconds = 250,
                        EndTime = DateTime.Now.AddSeconds(10),
                        EndTimeSeconds = 255,
                        FrameStart = 1,
                        FrameEnd = 1400,
                        SceneNumber = sceneNumber++,
                        OCR = "når nogen har tilføjet humor",
                        Speech = "Lorem Ipsum har været standard fyldtekst",
                        Sentiment = new Sentiment
                        {
                            Negative = 0.0,
                            Neutral = 0.0,
                            Positive = 1.0
                        },
                        Objects = new List<CommonObject>
                        {
                            new CommonObject
                            {
                                Name = "Cow",
                                Confidence = "0.90101020202020"
                            },
                            new CommonObject
                            {
                                Name = "Bus",
                                Confidence = "0.60101020202020"
                            }
                        }
                    },
                }
            };

            return metadata;
        }
    }
}
