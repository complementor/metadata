using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DataService.Contracts;
using MongoDbAccessLayer.DataService.Dtos;
using MongoDbAccessLayer.DomainModels;

namespace MongoDbAccessLayer.Context.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public readonly IMongoVideoDbContext _mongoContext;
        public IMongoCollection<BsonDocument> _documentModel;

        public DocumentRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context ?? throw new ArgumentNullException(nameof(context));
            _documentModel = _mongoContext.GetCollection<BsonDocument>("document");
        }

        public List<VideoInfoDto> GetAll()
        {
            var result = new VideoInfoDto();
            var filter = Builders<BsonDocument>.Filter.Empty;
            var project = Builders<BsonDocument>.Projection
                .Exclude("Description")
                .Exclude("Features");

            return _documentModel.Find(filter).Project(project)
                .ToList()
                .Select(projection => new VideoInfoDto() {
                    VideoId = projection["_id"].ToString(),
                    Title = projection["Name"].ToString(),
                })
                .Take(100).ToList(); 
        }

        public VideoMetadataDto Get(string id)
        {
            //ex. ObjectId("5e514803fa0df9b9f548f02c);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var bsonDoc =  _documentModel.Find(filter).FirstOrDefault();
             
            var domainModel = MapDomainModel(bsonDoc);

            return MapDto(domainModel);
        }


        private VideoMetadataDto MapDto(DocumentModel domainModel)
        {
            var result = new VideoMetadataDto();

            result.Title = domainModel.Name;
            result.YouTubeId = domainModel.Name;

            return result;
        }

        private DocumentModel MapDomainModel(BsonDocument bsonDoc)
        {
            var result = new DocumentModel();

            result._id = bsonDoc["_id"]?.ToString();
            result.Source = bsonDoc["Source"]?.ToString();
            result.Name = bsonDoc["Name"]?.ToString();



            return result;
        }
    }
}