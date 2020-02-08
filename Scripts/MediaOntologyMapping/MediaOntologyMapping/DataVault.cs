using MediaOntologyMapping.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace MediaOntologyMapping
{
    public class DataVault : IDataVault
    {
        public void WriteJsonFile(List<Link> linkDocuments, string destinationFolder, string fileName)
        {
            string json = JsonConvert.SerializeObject(linkDocuments);
            File.WriteAllText(destinationFolder + fileName, json);
        }

        public Link CreateLink(JObject original, List<object> mediaOntologyProperties)
        {
            return new Link()
            {
                Id = Guid.NewGuid(),
                Hub = new Hub()
                {
                    Date = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"),
                    Satellite = new Satellite
                    {
                        Attributes = mediaOntologyProperties,
                    }
                },
                OriginalHub = new Hub()
                {
                    Date = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"),
                    Satellite = new OriginalSatellite
                    {
                        Attributes = original
                    }
                }
            };
        }
    }
}