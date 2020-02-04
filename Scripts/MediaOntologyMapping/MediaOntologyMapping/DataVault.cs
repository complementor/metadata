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
        public DataVaultDocument CreateDocument(JObject original, MediaOntologyExifModel exif,
            MediaOntologyDublinCoreModel dc, MediaOntologyXmpModel xmp, MediaOntologyId3Model id3)
        {
            return null;
        }

        public void WriteDocument(DataVaultDocument dataVaultStructure)
        {
            var list = new List<DataVaultDocument>() { dataVaultStructure };
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(@"P:\src\Metadata\mo\28e78611-8dc7-4c86-8d48-3187a0c2a659_parsed.json", json);
        }

        internal DataVaultDocument CreateDocument(JObject original)
        {
            return new DataVaultDocument()
            {
                Id = Guid.NewGuid(),
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

        internal DataVaultDocument CreateDocument(JObject original, MediaOntologyModel mediaOntologyModel)
        {
            string newString = JsonConvert.SerializeObject(mediaOntologyModel.ListOfProperties);

            return new DataVaultDocument()
            {
                Id = Guid.NewGuid(),
                Hub = new Hub()
                {
                    Date = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"),
                    Satellite = new Satellite
                    {
                        Attributes = mediaOntologyModel.ListOfProperties,
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