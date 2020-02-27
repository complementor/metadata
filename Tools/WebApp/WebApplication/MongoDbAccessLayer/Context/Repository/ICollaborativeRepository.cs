using MongoDbAccessLayer.Dtos;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface ICollaborativeRepository
    {
        CollaborationDto Get(string documentId);
    }
}