using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbAccessLayer.Context;
using MongoDbAccessLayer.DataService.Dtos;
using MongoDbAccessLayer.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = MongoDbAccessLayer.DomainModels.Attribute;

namespace MongoDbAccessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        private MongoVideoDbContext context;
        public BusinessLogic(IOptions<MongoSettings> mongoSettings)
        {
            context = new MongoVideoDbContext(mongoSettings);
        }

        public List<VideoInfoDto> Search(string searchQuery)
        {
            var descriptionCollection = context.GetCollection<DescriptionModel>("description");
            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var mongoCollection = context.GetCollection<IndexModel>("index");
            var builder = Builders<IndexModel>.Filter;
            var filter = builder.Text(searchQuery, textSearchOptions);
            var projection = Builders<IndexModel>.Projection.MetaTextScore("score");
            var sort = Builders<IndexModel>.Sort.MetaTextScore("score");
            var sortedResult = mongoCollection
                .Find(filter)
                .Project<IndexModel>(projection)
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
                        FilterDefinition<DescriptionModel> idFilter = Builders<DescriptionModel>.Filter.Eq("id", item.VideoId);
                        var descriptionInfo = descriptionCollection.Find(idFilter).FirstOrDefaultAsync();
                        item.Title = FindValue(descriptionInfo?.Result?.hub?.Satellite?.Attributes, "title");
                        item.Duration = FindValue(descriptionInfo?.Result?.hub?.Satellite?.Attributes, "duration");
                    }
                }
            }

            var builder1 = Builders<DescriptionModel>.Filter;
            TextSearchOptions textSearchOptions1 = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var filter1 = builder1.Text(searchQuery, textSearchOptions1);
            var projection1 = Builders<DescriptionModel>.Projection.MetaTextScore("score");
            var sort1 = Builders<DescriptionModel>.Sort.MetaTextScore("score");
            var sortedResult1 = descriptionCollection
                .Find(filter1)
                .Project<DescriptionModel>(projection1)
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
            var featureCollection = context.GetCollection<IndexModel>("index");
            var descriptionCollection = context.GetCollection<DescriptionModel>("description");

            var metadata = new VideoMetadataDto();

            FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("id", objectId);
            var feature = featureCollection.Find(filter).FirstOrDefault();
            if (feature != null)
            {
                metadata.MapFeatures(feature);
            }

            FilterDefinition<DescriptionModel> idFilder = Builders<DescriptionModel>.Filter.Eq("id", objectId);
            var description = descriptionCollection.Find(idFilder).FirstOrDefault();
            if (description != null)
            {
                metadata.MapGenericDescription(description);
            }

            return metadata;
        }

        public List<VideoInfoDto> GetAll()
        {
            var descriptionCollection = context.GetCollection<DescriptionModel>("description");
            var mongoCollection = context.GetCollection<IndexModel>("index");
            var builder = Builders<IndexModel>.Filter;
            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var sortedResult = mongoCollection
            .Find(Builders<IndexModel>.Filter.Empty)?
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
                    FilterDefinition<DescriptionModel> filterDescription = Builders<DescriptionModel>.Filter.Eq("id", item.VideoId);
                    var descriptionInfo = descriptionCollection.Find(filterDescription).FirstOrDefaultAsync();
                    item.Title = FindValue(descriptionInfo?.Result.hub?.Satellite?.Attributes, "title");
                    item.Duration = FindValue(descriptionInfo?.Result.hub?.Satellite?.Attributes, "duration");
                }
            }

            var builder1 = Builders<DescriptionModel>.Filter;
            TextSearchOptions textSearchOptions1 = new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" };
            var sortedResult1 = descriptionCollection
            .Find(Builders<DescriptionModel>.Filter.Empty)
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
