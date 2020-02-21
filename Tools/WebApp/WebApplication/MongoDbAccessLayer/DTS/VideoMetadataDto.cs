using System;
using System.Collections.Generic;

namespace MongoDbAccessLayer.DTS
{
    public class VideoMetadataDto
    {
        public string Title { get; set; }
        public string YouTubeId { get; set; }
        public int Duration { get; set; }
        public List<GenericAttributes> Generic { get; set; }
        public List<Scenes> Scenes { get; set; }
        public string OCRAggregated { get; set; }
        public string SpeechAggregated { get; set; }
    }

    public class Scenes
    {
        public DateTime StartTime { get; set; }
        public int StartTimeSeconds { get; set; }
        public DateTime EndTime { get; set; }
        public int EndTimeSeconds { get; set; }
        public int FrameStart { get; set; }
        public int FrameEnd { get; set; }
        public int SceneNumber { get; set; }
        public string OCR { get; set; }
        public string Speech { get; set; }
        public Sentiment Sentiment { get; set; }
        public List<CommonObject> Objects { get; set; }
    }
    public class CommonObject
    {
        public string Name { get; set; }
        public string Confidence { get; set; }
    }
    public class Sentiment
    {
        public double Negative { get; set; }
        public double Positive { get; set; }
        public double Neutral { get; set; }
    }

    public class GenericAttributes
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}