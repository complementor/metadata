using MongoDbAccessLayer.DataService.Dtos;

namespace MongoDbAccessLayer.DataService.Contracts
{
    public interface IProvenanceRepository
    {
        ProvenanceDto Get(string documentId);
    }
}