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
        public IMongoCollection<BsonDocument> _documentCollection;

        public DocumentRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context ?? throw new ArgumentNullException(nameof(context));
            _documentCollection = _mongoContext.GetCollection<BsonDocument>("document");
        }

        public List<VideoInfoDto> GetAll()
        {
            //public async Task<IEnumerable<IndexModel>> GetAll()
            //{
            //    var all = await _dbCollection.FindAsync(Builders<IndexModel>.Filter.Empty);
            //    return await all.ToListAsync();
            //}

            var result = new VideoInfoDto();
            var filter = Builders<BsonDocument>.Filter.Empty;
            var project = Builders<BsonDocument>.Projection
                .Exclude("Description")
                .Exclude("Features");


            //MAPPING
            return _documentCollection.Find(filter).Project(project)
                .ToList()
                .Select(projection => new VideoInfoDto() {
                    VideoId = projection["_id"].ToString(),
                    Title = projection["Name"].ToString(),
                    //Duration = x.Attributes.Find(x => string.Equals(x.Name, "Duration", System.StringComparison.InvariantCultureIgnoreCase))?.Value,
                    //Standard = x.Attributes.Find(x => string.Equals(x.Name, "Standard", System.StringComparison.InvariantCultureIgnoreCase))?.Value,
                })
                .Take(100).ToList(); 
        }

        public VideoMetadataDto Get(string id)
        {
            //sample async
            //public async Task<IndexModel> GetById(string guid)
            //{
            //    //ex. guid
            //    FilterDefinition<IndexModel> filter = Builders<IndexModel>.Filter.Eq("Id", guid);

            //    _dbCollection = _mongoContext.GetCollection<IndexModel>(typeof(IndexModel).Name);

            //    return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
            //}

            //ex. ObjectId("5e514803fa0df9b9f548f02c);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));

            var bsonDoc =  _documentCollection.Find(filter).FirstOrDefault();
             
            var domainModel = MapDomainModel(bsonDoc);

            return MapDto(domainModel);
        }

        public List<VideoInfoDto> SearchByProperty(string propertyName, string text)
        {
            var result = new List<VideoInfoDto>();

            var documentCollection = _mongoContext.GetCollection<DocumentModel>("document");

            var filterOMRSatelliteExists = Builders<DocumentModel>.Filter.Eq("Description.0.Satellites.Name", "OMRSatellite");
            var filterPropertyName = Builders<DocumentModel>.Filter.Eq("Description.0.Satellites.Attributes.Name", propertyName);
            var filterTest = Builders<DocumentModel>.Filter.Eq("Description.0.Satellites.Attributes.Value", text);
            var filter = Builders<DocumentModel>.Filter.And(filterOMRSatelliteExists, filterPropertyName, filterTest);
            var project = Builders<DocumentModel>.Projection
                .Exclude("Features")
                .Exclude("Description");

            var bsonDocument = documentCollection.Find(filter).Project(project).ToList();

            result = bsonDocument.Select(b => new VideoInfoDto()
            {
                VideoId = b["_id"].ToString(),
                Title = b["Name"].ToString(),
            }).ToList();

            return result;
        }

        public GenericPropertiesDto GetExistentGenericProperties()
        { 
            var filterExists = Builders<BsonDocument>.Filter.Exists("Description.0.Satellites");
            var filterEq = Builders<BsonDocument>.Filter.Eq("Description.0.Satellites.Name", "OMRSatellite");
            var filter = Builders<BsonDocument>.Filter.And(filterExists, filterEq);
            var project = Builders<BsonDocument>.Projection
                .Exclude("_id")
                .Include("Description.Satellites.Attributes.Name");

            var result = _documentCollection.Find(filter).Project(project)
                 .ToList();

            var collector = new List<string>();
            foreach (var element in result)
            {
                var docDescription = element["Description"].AsBsonArray.First();
                var docSat = docDescription["Satellites"].AsBsonDocument;
                var attr = docSat["Attributes"].AsBsonArray.Select(attr => attr["Name"].ToString()).ToList();
                collector.AddRange(attr);

            }

            return new GenericPropertiesDto() {ListOfProperties = collector.Distinct().ToList() };
        }

        public List<VideoInfoDto> Search(string searchQuery)
        {
            //sample  async
            //public async Task<List<DescriptionModel>> SearchOntoDescriptionAsync(string searchQuery)
            //{
            //    return await _descriptionCollection
            //            .Find(Builders<DescriptionModel>.Filter.Text(searchQuery, new TextSearchOptions() { CaseSensitive = false, DiacriticSensitive = false, Language = "english" }))
            //            .Project<DescriptionModel>(Builders<DescriptionModel>.Projection.MetaTextScore("score"))
            //            .Sort(Builders<DescriptionModel>.Sort.MetaTextScore("score"))
            //            .ToListAsync();
            //}

            var result = new List<VideoInfoDto>();
             
            TextSearchOptions textSearchOptions = new TextSearchOptions() { CaseSensitive = false, 
                DiacriticSensitive = false, Language = "english" };
            var filter = Builders<BsonDocument>.Filter.Text(searchQuery, textSearchOptions);
            var projection = Builders<BsonDocument>.Projection
                .MetaTextScore("score")
                .Exclude("Features")
                .Exclude("Description");
            var sort = Builders<BsonDocument>.Sort.MetaTextScore("score");
            var sortedResult = _documentCollection
                .Find(filter)
                .Project<BsonDocument>(projection)
                .Sort(sort)
                .ToList();



            //map to dto
            result = sortedResult.Select(b => new VideoInfoDto()
            {
                VideoId = b["_id"].ToString(),
                Title = b["Name"].ToString(),
                Score = b["score"].AsDouble,
            }).ToList();


            return result;
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

            result._id = bsonDoc["_id"].AsObjectId;
            result.Source = bsonDoc["Source"]?.ToString();
            result.Name = bsonDoc["Name"]?.ToString();

            return result;
        }

        
    }
}