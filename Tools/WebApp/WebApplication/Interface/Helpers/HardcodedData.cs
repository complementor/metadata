using MongoDbAccessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface.Helpers
{
    public static class HardcodedData
    {
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

        public static List<VideoInfoDto> GetListOfVideos()
        {
            return list;
        }

        public static ProvenanceDto GetProvenanceDto()
        {
            var provenance = new ProvenanceDto
            {
                Nodes = new List<Node>
                {
                    new Node
                    {
                        Name = "Andrei",
                        Type = "agent",
                    },
                    new Node
                    {
                        Name = "Document_v2",
                        Type = "entity",
                    },
                    new Node
                    {
                        Name = "Tagging",
                        Type = "activity",
                    },
                    new Node
                    {
                        Name = "Document_v3",
                        Type = "entity",
                    },
                    new Node
                    {
                        Name = "SceneDetect",
                        Type = "agent",
                    },
                    new Node
                    {
                        Name = "ExtractFeatures",
                        Type = "activity",
                    },
                    new Node
                    {
                        Name = "ExtractGenericMetadata",
                        Type = "activity",
                    },
                    new Node
                    {
                        Name = "ExifTool",
                        Type = "agent",
                    },
                    new Node
                    {
                        Name = "Document_v1",
                        Type = "entity",
                    },
                },
                Links = new List<Link>
                {
                    new Link
                    {
                        Source = 1,
                        Type = "wasGeneratedBy",
                        Target = 2,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 2,
                        Type = "wasAssociatedWith",
                        Target = 0,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 2,
                        Type = "used",
                        Target = 8,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 2,
                        Type = "wasAttributedTo",
                        Target = 7,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 3,
                        Type = "wasAssociatedWith",
                        Target = 4,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 5,
                        Type = "used",
                        Target = 1,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 3,
                        Type = "wasGeneratedBy",
                        Target = 5,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 3,
                        Type = "wasDerivedFrom",
                        Target = 1,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 6,
                        Type = "wasAttributedTo",
                        Target = 4,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 8,
                        Type = "wasAssociatedWith",
                        Target = 7,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 8,
                        Type = "wasGeneratedBy",
                        Target = 6,
                        Datetime = DateTime.Now
                    },
                    new Link
                    {
                        Source = 1,
                        Type = "wasDerivedFrom",
                        Target = 8,
                        Datetime = DateTime.Now
                    }
                }
            };

            return provenance;
        }

        public static VideoMetadataDto GetVideoMetadataDto(string fileName)
        {
            int sceneNumber = 1;

            var metadata = new VideoMetadataDto
            {
                Title = fileName,
                Duration = "200",
                YouTubeId = "tgbNymZ7vqY",
                OCRAggregated = "Der er mange tilgængelige udgaver af Lorem Ipsum, men de fleste udgaver har gennemgået forandringer, når nogen har tilføjet humor eller tilfældige ord, som på ingen måde ser ægte ud. Hvis du skal bruge en udgave af Lorem Ipsum, skal du sikre dig, at der ikke indgår noget pinligt midt i teksten. Alle Lorem Ipsum-generatore på nettet har en tendens til kun at dublere små brudstykker af Lorem Ipsum efter behov, hvilket gør dette til den første ægte generator på internettet. Den bruger en ordbog på over 200 ord på latin kombineret med en håndfuld sætningsstrukturer til at generere sætninger, som ser pålidelige ud. Resultatet af Lorem Ipsum er derfor altid fri for gentagelser, tilføjet humor eller utroværdige ord osv.",
                SpeechAggregated = "Lorem Ipsum er ganske enkelt fyldtekst fra print- og typografiindustrien. Lorem Ipsum har været standard fyldtekst siden 1500-tallet, hvor en ukendt trykker sammensatte en tilfældig spalte for at trykke en bog til sammenligning af forskellige skrifttyper. Lorem Ipsum har ikke alene overlevet fem århundreder, men har også vundet indpas i elektronisk typografi uden væsentlige ændringer. Sætningen blev gjordt kendt i 1960'erne med lanceringen af Letraset-ark, som indeholdt afsnit med Lorem Ipsum, og senere med layoutprogrammer som Aldus PageMaker, som også indeholdt en udgave af Lorem Ipsum.",
                Generic = new List<GenericAttribute>
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
