using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.Dtos;
using MongoDbAccessLayer.DTS;
using MongoDbAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = MongoDbAccessLayer.Models.Attribute;

namespace MongoDbAccessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        MongoVideoDbContext context;
        public BusinessLogic(IOptions<MongoSettings> mongoSettings)
        {
            context = new MongoVideoDbContext(mongoSettings);
        }

        public List<VideoInfoDto> Search(string searchQuery)
        {
            var mongoCollection = context.GetCollection<Features>("features");
            var builder = Builders<Features>.Filter;
            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var filter = builder.Text(searchQuery, textSearchOptions);
            var projection = Builders<Features>.Projection.MetaTextScore("score");
            var sort = Builders<Features>.Sort.MetaTextScore("score");
            var sortedResult = mongoCollection
                .Find(filter)
                .Project<Features>(projection)
                .Sort(sort)
                .ToList();
            var result = sortedResult.Select(x => new
            {
                score = x.score,
                _id = x._id,
                id = x.Id,
                date = x.hub.date,
            }
          );

            var genericCollection = context.GetCollection<Generic>("generic");
            var builder1 = Builders<Generic>.Filter;
            TextSearchOptions textSearchOptions1 = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var filter1 = builder1.Text(searchQuery, textSearchOptions);
            var projection1 = Builders<Generic>.Projection.MetaTextScore("score");
            var sort1 = Builders<Generic>.Sort.MetaTextScore("score");
            var sortedResult1 = genericCollection
                .Find(filter1)
                .Project<Generic>(projection1)
                .Sort(sort1)
                .ToList();

            var result1 = sortedResult1.Select(x => new
            {
                score = x.score,
                _id = x._id,
                date = x.hub?.Date,
                id = x.Id,
                title = FindValue(x?.hub?.Satellite?.Attributes, "title"),
                duration = FindValue(x?.hub?.Satellite?.Attributes, "duration"),
            }
            );

            var standardList = new List<string>() { "DC", "TVAnytime", "Mpeg7", "XMP" };

            return result.Join(result1,
                first => first.id,
                second => second.id,
                (first, second) => new { Feature = first, Generic = second }).Select(x => new VideoInfoDto
                {
                    VideoId = x.Feature.id,
                    Score = new List<double> { x.Feature.score, x.Generic.score }.Average(),
                    Standard = standardList[new Random().Next(standardList.Count)],
                    Duration = x.Generic.duration,
                    Title = x.Generic.title,
                }).ToList();
        }

        public VideoMetadataDto Get(string objectId)
        {
            var featureCollection = context.GetCollection<Features>("features");
            var genericCollection = context.GetCollection<Generic>("generic");

            var metadata = new VideoMetadataDto();

            FilterDefinition<Features> filter = Builders<Features>.Filter.Eq("Id", objectId);
            var feature = featureCollection.Find(filter).FirstOrDefaultAsync();
            if (feature != null)
            {
                metadata.IncludeFeature(feature);
            }

            FilterDefinition<Generic> filterGeneric = Builders<Generic>.Filter.Eq("Id", objectId);
            var generic = genericCollection.Find(filterGeneric).FirstOrDefaultAsync();
            if (generic != null)
            {
                metadata.IncludeGeneric(generic);
            }

            return metadata;
        }


        public string FindValue(List<Attribute> attributes, string attribute)
        {
            if (attributes == null) return null;
            return attributes.Where(x => x.Name == attribute).Select(x => x.Value).FirstOrDefault(); ;
        }
    }
}
