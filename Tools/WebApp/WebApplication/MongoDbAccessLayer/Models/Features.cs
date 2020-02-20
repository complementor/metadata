using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbAccessLayer.Models
{
    public class Features
    {
        public class Link
        {
            public ObjectId _id { get; set; }
            public Hub hub { get; set; }
        }

        public class Hub
        {
            public string date { get; set; }
            public List<Satellite> satellite { get; set; }
        }

        public class Satellite
        {
            public List<Optical_Character_Recognition> optical_character_recognition { get; set; }
            public List<Speech_Recognition> speech_recognition { get; set; }
            public List<Sentiment_Analysis> sentiment_analysis { get; set; }
            public List<Object> objects { get; set; }
        }

        public class Optical_Character_Recognition
        {
            public int scene { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public int frameStart { get; set; }
            public int frameEnd { get; set; }
            public string value { get; set; }
        }

        public class Speech_Recognition
        {
            public int scene { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public int frameStart { get; set; }
            public int frameEnd { get; set; }
            public string value { get; set; }
        }

        public class Sentiment_Analysis
        {
            public int scene { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public int frameStart { get; set; }
            public int frameEnd { get; set; }
            public Sentiment_AnalysisValue value { get; set; }
        }

        public class Sentiment_AnalysisValue
        {
            public double neg { get; set; }
            public double neu { get; set; }
            public double pos { get; set; }
            public double compound { get; set; }
        }

        public class Object
        {
            public int scene { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public int frameStart { get; set; }
            public int frameEnd { get; set; }
            public List<ObjectValue> value { get; set; }
        }

        public class ObjectValue
        {
            public string label { get; set; }
            public float confidence { get; set; }
        }

    }
}
