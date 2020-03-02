using MongoDbAccessLayer.DataService.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid().ToString(),
                Duration = "00:05:10.779",
                Standard = "ID3",
                Title = "Breaking Bad"
            },
            new VideoInfoDto
            {
                VideoId = Guid.NewGuid().ToString(),
                Duration = "00:10:00.122",
                Standard = "Exif",
                Title = "Snowboarding"
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

        public static CollaborationDto GetCollaborationDto()
        {
            var collaboration = new CollaborationDto
            {
                Comments = new List<Comment>
                {
                   new Comment
                   {
                       User = "Peter",
                       UserIcon = "https://images.pexels.com/photos/220453/pexels-photo-220453.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=200",
                       Description = "I did a classification task on this video.",
                       DateTime = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                   },
                   new Comment
                   {
                       User = "Anne",
                       UserIcon = "https://www.joancanto.com/wp-content/uploads/2017/04/H10B0527.jpg",
                       Description = "Great job, Peter. Very useful for my work.",
                       DateTime = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                   },
                   new Comment
                   {
                       User = "Thomas",
                       UserIcon = "https://cdn.vuetifyjs.com/images/lists/2.jpg",
                       Description = "Nice findings! I added a few more tags.",
                       DateTime = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                   }
                },
                Tags = new List<Tag>
                {
                    new Tag
                    {
                        TagName = "Entertainment",
                        UserName = "Peter"
                    },
                    new Tag
                    {
                        TagName = "Nature",
                        UserName = "Anne"
                    },
                    new Tag
                    {
                        TagName = "New Zealand",
                        UserName = "Anne"
                    },
                    new Tag
                    {
                        TagName = "Travel",
                        UserName = "Anne"
                    },
                    new Tag
                    {
                        TagName = "Youtube",
                        UserName = "Thomas"
                    },
                    new Tag
                    {
                        TagName = "Auckland",
                        UserName = "Thomas"
                    }
                }
            };

            return collaboration;
        }

        public static VideoMetadataDto GetVideoMetadataDto(string guid)
        {
            var list = HardcodedData.GetListOfVideos();
            var file = list
                .Where(x => x.VideoId == guid)
                .FirstOrDefault();

            int sceneNumber = 1;

            var metadata = new VideoMetadataDto
            {
                Title = file.Title,
                Duration = "200",
                YouTubeId = "mIq9jFdEfZo",
                OCRAggregated = "Der er mange tilgængelige udgaver af Lorem Ipsum, men de fleste udgaver har gennemgået forandringer, når nogen har tilføjet humor eller tilfældige ord, som på ingen måde ser ægte ud. Hvis du skal bruge en udgave af Lorem Ipsum, skal du sikre dig, at der ikke indgår noget pinligt midt i teksten. Alle Lorem Ipsum-generatore på nettet har en tendens til kun at dublere små brudstykker af Lorem Ipsum efter behov, hvilket gør dette til den første ægte generator på internettet. Den bruger en ordbog på over 200 ord på latin kombineret med en håndfuld sætningsstrukturer til at generere sætninger, som ser pålidelige ud. Resultatet af Lorem Ipsum er derfor altid fri for gentagelser, tilføjet humor eller utroværdige ord osv.",
                SpeechAggregated = "Lorem Ipsum er ganske enkelt fyldtekst fra print- og typografiindustrien. Lorem Ipsum har været standard fyldtekst siden 1500-tallet, hvor en ukendt trykker sammensatte en tilfældig spalte for at trykke en bog til sammenligning af forskellige skrifttyper. Lorem Ipsum har ikke alene overlevet fem århundreder, men har også vundet indpas i elektronisk typografi uden væsentlige ændringer. Sætningen blev gjordt kendt i 1960'erne med lanceringen af Letraset-ark, som indeholdt afsnit med Lorem Ipsum, og senere med layoutprogrammer som Aldus PageMaker, som også indeholdt en udgave af Lorem Ipsum.",
                Generic = new List<GenericAttribute>
                {
                    new GenericAttribute
                    {
                        Name = "frameSizeWidth",
                        Value = "1920"
                    },
                    new GenericAttribute
                    {
                        Name = "frameSizeHeight",
                        Value = "1080"
                    },
                    new GenericAttribute
                    {
                        Name = "numTracks",
                        Value = "2"
                    },
                    new GenericAttribute
                    {
                        Name = "sampleRate",
                        Value = "48000"
                    },
                    new GenericAttribute
                    {
                        Name = "compression",
                        Value = "MKV"
                    },
                    new GenericAttribute
                    {
                        Name = "format",
                        Value = "video/x-matroska"
                    },
                    new GenericAttribute
                    {
                        Name = "frameRate",
                        Value = "23.976"
                    }
                },
                Scenes = new List<Scene>
                {
                    new Scene
                    {
                        StartTime = DateTime.Now,
                        StartTimeSeconds = 1,
                        EndTime = DateTime.Now.AddSeconds(10),
                        EndTimeSeconds = 60,
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
                        StartTimeSeconds = 60,
                        EndTime = DateTime.Now.AddSeconds(10),
                        EndTimeSeconds = 80,
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
                        StartTimeSeconds = 80,
                        EndTime = DateTime.Now.AddSeconds(10),
                        EndTimeSeconds = 85,
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

        public static GenericPropertiesDto GetGenericPropertiesDto()
        {
            var properties = new GenericPropertiesDto
            {
                ListOfProperties = new List<string>
                {
                    "GenericProperty01",
                    "GenericProperty02",
                    "GenericProperty03",
                    "GenericProperty04",
                    "GenericProperty05",
                    "GenericProperty06",
                    "GenericProperty07",
                    "GenericProperty08",
                    "GenericProperty09",
                    "GenericProperty10"
                }
            };

            return properties;
        }
    }
}
