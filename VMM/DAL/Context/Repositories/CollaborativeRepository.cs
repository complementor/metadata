using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbAccessLayer.DomainModels;
using MongoDbAccessLayer.DataService.Contracts;
using MongoDbAccessLayer.DataService.Dtos;
using System;
using System.Linq;

namespace MongoDbAccessLayer.Context.Repositories
{
    public class CollaborativeRepository : ICollaborativeRepository
    {
        private static IMongoVideoDbContext _mongoContext;
        private static IMongoCollection<CollaborationModel> _collaborativeCollection;

        public CollaborativeRepository(IMongoVideoDbContext context)
        {
            _mongoContext = context ?? throw new ArgumentNullException(nameof(context));
            _collaborativeCollection = _mongoContext.GetCollection<CollaborationModel>("collaborative");
        }

        public CollaborationDto Get(string documentId)
        {
            var result = (from e in _collaborativeCollection.AsQueryable<CollaborationModel>()
                          where e.DocumentId == ObjectId.Parse(documentId)
                          select new
                          {
                              e.Sat_Collaborative_Tag,
                              e.Sat_Collaborative_Description
                          })?.First();

            return new CollaborationDto()
            {
                Comments = result.Sat_Collaborative_Description,
                Tags = result.Sat_Collaborative_Tag
            };
        }
    }
}