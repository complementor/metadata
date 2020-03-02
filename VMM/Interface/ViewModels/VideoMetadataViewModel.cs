using Interface.Helpers;
using MongoDbAccessLayer.DataService.Dtos;
using System.Collections.Generic;

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
