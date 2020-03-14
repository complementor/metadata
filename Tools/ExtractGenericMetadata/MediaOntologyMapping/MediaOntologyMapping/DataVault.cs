using MediaOntologyMapping.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MediaOntologyMapping
{
    public class DataVault : IDataVault
    {
        public void WriteJsonFile(List<Link> linkDocuments, string destination)
        {
            string json = JsonConvert.SerializeObject(linkDocuments);
            File.WriteAllText(destination, json);
        }

        public Link CreateLink(JObject original, List<OMRAttribute> mediaOntologyProperties)
        {
            return new Link()
            {
                Name = ((string)original["FileName"]).Split('.')[0],
                Source = original["SourceFile"].ToString(),
                Description = new List<Hub>() {
                    new Hub()
                    {
                        Date = BsonDateTime.Create(DateTime.Now),
                        Satellites = new Satellite
                        {
                            Name = "OMRSatellite",
                            Attributes = mediaOntologyProperties,
                        }
                    },
                    new Hub()
                    {
                        Date = BsonDateTime.Create(DateTime.Now),
                        Satellites = new OriginalSatellite
                        {
                            Name = "ORGSatellite",
                            Attributes = original
                        }
                    }
                },
                Features = new List<Hub>
                {
                    new Hub()
                    {
                        Date = BsonDateTime.Create(DateTime.Now),
                        Satellites = new List<FeatureSatellite>()
                        {
                            //new FeatureSatellite
                            //{
                            //    Name = "Scenes",
                            //    Attributes = mediaOntologyProperties ,
                            //},
                            //new FeatureSatellite
                            //{
                            //    Name = "OCR",
                            //    Attributes = mediaOntologyProperties,
                            //},
                            //new FeatureSatellite
                            //{
                            //    Name = "OCR",
                            //    Attributes = mediaOntologyProperties,
                            //},
                        }
                    },
                }
            };
        }
    }
}