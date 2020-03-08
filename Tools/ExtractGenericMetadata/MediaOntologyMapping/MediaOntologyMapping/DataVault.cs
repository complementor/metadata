﻿using MediaOntologyMapping.Models;
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

        public Link CreateLink(JObject original, List<Models.Attribute> mediaOntologyProperties)
        {
            return new Link()
            {
                _id = ObjectId.GenerateNewId(),
                Name = ((string)original["FileName"]).Split('.')[0],
                Source = original["SourceFile"].ToString(),
                Description = new List<Hub>() {
                    new Hub()
                    {
                        Date = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"),
                        Satellites = new Satellite
                        {
                            Name = "",
                            Attributes = mediaOntologyProperties,
                        }
                    },
                    new Hub()
                    {
                        Date = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"),
                        Satellites = new OriginalSatellite
                        {
                            Name = "",
                            Attributes = original
                        }
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