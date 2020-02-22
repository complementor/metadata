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
            var genericCollection = context.GetCollection<Generic>("generic");
            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var mongoCollection = context.GetCollection<Features>("features");
            var builder = Builders<Features>.Filter;
            var filter = builder.Text(searchQuery, textSearchOptions);
            var projection = Builders<Features>.Projection.MetaTextScore("score");
            var sort = Builders<Features>.Sort.MetaTextScore("score");
            var sortedResult = mongoCollection
                .Find(filter)
                .Project<Features>(projection)
                .Sort(sort)
                .ToListAsync();

            var result = new List<VideoInfoDto>();
            if (sortedResult != null)
            {
                result = sortedResult.Result.Select(x => new VideoInfoDto
                {
                    Score = x.score,
                    VideoId = x.Id,
                }).ToList();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        FilterDefinition<Generic> filterGeneric = Builders<Generic>.Filter.Eq("id", item.VideoId);
                        var genericInfo = genericCollection.Find(filterGeneric).FirstOrDefaultAsync();
                        item.Title = FindValue(genericInfo?.Result.hub?.Satellite?.Attributes, "title");
                        item.Duration = FindValue(genericInfo?.Result.hub?.Satellite?.Attributes, "duration");
                    }
                }
            }

            var builder1 = Builders<Generic>.Filter;
            TextSearchOptions textSearchOptions1 = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var filter1 = builder1.Text(searchQuery, textSearchOptions1);
            var projection1 = Builders<Generic>.Projection.MetaTextScore("score");
            var sort1 = Builders<Generic>.Sort.MetaTextScore("score");
            var sortedResult1 = genericCollection
                .Find(filter1)
                .Project<Generic>(projection1)
                .Sort(sort1)
                .ToList();

            var standardList = new List<string>() { "DC", "TVAnytime", "Mpeg7", "XMP" };
            Random r = new Random();

            var result1 = new List<VideoInfoDto>();
            if (sortedResult1 != null)
            {
                result1 = sortedResult1.Select(x => new VideoInfoDto
                {
                    Score = x.score,
                    VideoId = x.id,
                    Title = FindValue(x?.hub?.Satellite?.Attributes, "title"),
                    Duration = FindValue(x?.hub?.Satellite?.Attributes, "duration"),
                    Standard = standardList[r.Next(standardList.Count)],
                }
            ).ToList();

            }


            if (result != null && result1 != null)
            {
                result = result.Union(result1).ToList()
                          .GroupBy(x => x.VideoId)
                          .Select(x => x.FirstOrDefault())
                          .ToList();
            }
            else if (result == null)
            {
                result = result1;

                if (result == null)
                {
                    return null;
                }
            }

            return result;
        }

        public VideoMetadataDto Get(string objectId)
        {
            var featureCollection = context.GetCollection<Features>("features");
            var genericCollection = context.GetCollection<Generic>("generic");

            var metadata = new VideoMetadataDto();

            FilterDefinition<Features> filter = Builders<Features>.Filter.Eq("id", objectId);
            var feature = featureCollection.Find(filter).FirstOrDefaultAsync();
            if (feature != null)
            {
                metadata.IncludeFeature(feature);
            }

            FilterDefinition<Generic> filterGeneric = Builders<Generic>.Filter.Eq("id", objectId);
            var generic = genericCollection.Find(filterGeneric).FirstOrDefaultAsync();
            if (generic != null)
            {
                metadata.IncludeGeneric(generic);
            }

            return metadata;
        }

        public List<VideoInfoDto> GetAll()
        {
            var genericCollection = context.GetCollection<Generic>("generic");
            var mongoCollection = context.GetCollection<Features>("features");
            var builder = Builders<Features>.Filter;
            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var sortedResult = mongoCollection
            .Find(Builders<Features>.Filter.Empty)?
            .ToList();

            var result = sortedResult?.Select(x => new VideoInfoDto
            {
                Score = x.score,
                VideoId = x.Id,
            }
            ).ToList();

            if (result != null)
            {
                foreach (var item in result)
                {
                    FilterDefinition<Generic> filterGeneric = Builders<Generic>.Filter.Eq("id", item.VideoId);
                    var genericInfo = genericCollection.Find(filterGeneric).FirstOrDefaultAsync();
                    item.Title = FindValue(genericInfo?.Result.hub?.Satellite?.Attributes, "title");
                    item.Duration = FindValue(genericInfo?.Result.hub?.Satellite?.Attributes, "duration");
                }
            }

            var builder1 = Builders<Generic>.Filter;
            TextSearchOptions textSearchOptions1 = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var sortedResult1 = genericCollection
            .Find(Builders<Generic>.Filter.Empty)
            .ToList();

            var standardList = new List<string>() { "DC", "TVAnytime", "Mpeg7", "XMP" };
            Random r = new Random();
            var result1 = sortedResult1?.Select(x => new VideoInfoDto
            {
                Score = x.score,
                VideoId = x.id,
                Title = FindValue(x?.hub?.Satellite?.Attributes, "title"),
                Duration = FindValue(x?.hub?.Satellite?.Attributes, "duration"),
                Standard = standardList[r.Next(standardList.Count)],
            }
            ).ToList();

            if (result != null && result1 != null)
            {
                result = result.Union(result1).ToList()
                          .GroupBy(x => x.VideoId)
                          .Select(x => x.FirstOrDefault())
                          .ToList();
            }
            else if (result == null)
            {
                result = result1;

                if (result == null)
                {
                    return null;
                }
            }

            return result;
        }

        public string FindValue(List<Attribute> attributes, string attribute)
        {
            if (attributes == null) return null;
            return attributes.Where(x => x.Name == attribute).Select(x => x.Value).FirstOrDefault(); ;
        }
    }
}
