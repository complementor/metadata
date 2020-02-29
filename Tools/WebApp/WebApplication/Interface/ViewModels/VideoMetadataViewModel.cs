using Interface.Helpers;
using MongoDbAccessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface.ViewModels
{
    public class VideoMetadataViewModel
    {
        public VideoMetadataDto VideoMetadataDto { get; set; }
        public List<Word> Words { get; set; }
        public CollaborationDto Collaboration { get; set; }
        public ProvenanceDto Provenance { get; set; }
        public bool ObjectsAdded { get; set; }
        public bool OcrAdded { get; set; }
        public bool SpeechAdded { get; set; }
        public bool ProvenanceAdded { get; set; }
        public bool CollaborationAdded { get; set; }
        public bool SentimentAdded { get; set; }
    }
}
