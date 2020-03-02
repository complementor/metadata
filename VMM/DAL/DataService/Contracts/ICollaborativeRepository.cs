using MongoDbAccessLayer.DataService.Dtos;

namespace MongoDbAccessLayer.DataService.Contracts
{
    public interface ICollaborativeRepository
    {
        CollaborationDto Get(string documentId);
    }
}