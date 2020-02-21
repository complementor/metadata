using MongoDbAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbAccessLayer.DTS
{
    public class VideoMetadataDto
    {
        public string Title { get; set; }
        public string YouTubeId { get; set; }
        public int Duration { get; set; }
        public List<GenericAttributes> Generic { get; set; }
        public string OCRAggregated { get; set; }
        public string SpeechAggregated { get; set; }
        public List<Scene> Scenes { get; set; }

        internal void IncludeFeature(Task<Features> feature)
        {
            var satellites = feature.Result?.hub?.satellite.ToList();
            if (satellites == null) return;
            var ocrDictionary = new Dictionary<int, string>();
            var speechDictionary = new Dictionary<int, string>();
            var objectDictionary = new Dictionary<int, List<CommonObject>>();
            var sentimentDictionary = new Dictionary<int, Sentiment>();


            var speech = satellites.Where(x => x.speech_recognition != null).Select(x => x.speech_recognition).FirstOrDefault();
            if (speech != null)
            {
                SpeechAggregated = string.Join(" ", speech.Where(x => x.value != null).Select(x => x.value).ToList());

                foreach (var item in speech)
                {
                    speechDictionary.Add(item.scene, item.value);
                }
            }

            var ocrFromScenes = satellites.Where(x => x.optical_character_recognition != null).Select(x => x.optical_character_recognition).FirstOrDefault();
            if (ocrFromScenes != null)
            {
                OCRAggregated = string.Join(" ", ocrFromScenes.Where(x => x.value != null).Select(x => x.value).ToList());


                foreach (var item in ocrFromScenes)
                {
                    ocrDictionary.Add(item.scene, item.value);
                }
            }

            var objectsFromScenes = satellites.Where(x => x.objects != null).Select(x => x.objects).FirstOrDefault();
            if (objectsFromScenes != null)
            {

                foreach (var item in objectsFromScenes)
                {
                    if (item.value == null) continue;

                    var listOfObjectsInAScene = item.value
                    .Where(x => x.label != null)
                    .Select(x => new CommonObject
                    {
                        Confidence = x?.confidence.ToString(),
                        Name = x?.label,
                    }).ToList();

                    objectDictionary.Add(item.scene, listOfObjectsInAScene);
                }
            }

            var sentimetnsFromScenes = satellites.Where(x => x.sentiment_analysis != null).Select(x => x.sentiment_analysis).FirstOrDefault();
            if (sentimetnsFromScenes != null)
            {
                foreach (var item in sentimetnsFromScenes)
                {
                    if (item.value == null) continue;

                    var sentiment = new Sentiment
                    {
                        Negative = item.value.neg,
                        Neutral = item.value.neu,
                        Positive = item.value.pos
                    };

                    sentimentDictionary.Add(item.scene, sentiment);
                }
            }



            var scenes = satellites.Where(x => x.scenes != null).Select(x => x.scenes).FirstOrDefault();
            if (scenes != null)
            {
                Scenes = new List<Scene>();
                foreach (var item in scenes)
                {

                    var scene = new Scene()
                    {
                        SceneNumber = item.scene,
                        FrameStart = item.frameStart,
                        FrameEnd = item.frameEnd,
                        Objects = objectDictionary.FirstOrDefault(x => x.Key == item.scene).Value,
                        OCR = ocrDictionary.FirstOrDefault(x => x.Key == item.scene).Value,
                        Sentiment = sentimentDictionary.FirstOrDefault(x => x.Key == item.scene).Value,
                        Speech = speechDictionary.FirstOrDefault(x => x.Key == item.scene).Value
                    };

                    if (string.IsNullOrWhiteSpace(item.start) && item.start != null)
                    {
                        scene.StartTime = DateTime.ParseExact(item.start, "HH:mm:ss,fff", null);
                        scene.StartTimeSeconds = scene.StartTime.Second;

                    }
                    if (string.IsNullOrWhiteSpace(item.end) && item.end != null)
                    {
                        scene.EndTime = DateTime.ParseExact(item.end, "HH:mm:ss,fff", null);
                        scene.EndTimeSeconds = scene.EndTime.Second;
                    }

                    Scenes.Add(scene);
                }
            }
        }

        internal void IncludeGeneric(Task<Generic> generic)
        {
            var attributes = generic?.Result?.hub?.Satellite?.Attributes.ToList();
            if (attributes != null)
            {
                Generic = new List<GenericAttributes>();
                foreach (var item in attributes)
                {
                    Generic.Add(new GenericAttributes()
                    {
                        Name = item?.Name,
                        Value = item?.Value,
                        //Standard = item.Value,
                    });
                }
                Title = attributes.FirstOrDefault(x => x.Name.ToLowerInvariant() == "Title".ToLowerInvariant())?.Value;
                YouTubeId = attributes.FirstOrDefault(x => x.Name.ToLowerInvariant() == "YouTubeId".ToLowerInvariant())?.Value;
                Duration = Int32.Parse(attributes.FirstOrDefault(x => x.Name.ToLowerInvariant() == "Duration".ToLowerInvariant())?.Value);
            }
        }
    }
    public class GenericAttributes
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Scene
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


}