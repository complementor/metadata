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
        public void WriteDocument(DataVaultDocument dataVaultStructure, string destinationFolder, string fileName)
        {
            var list = new List<DataVaultDocument>() { dataVaultStructure };
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(destinationFolder+fileName, json);
        }

        public DataVaultDocument CreateDocument(JObject original, MediaOntologyModel mediaOntologyModel)
        {
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