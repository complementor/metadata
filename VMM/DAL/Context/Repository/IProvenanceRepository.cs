using MongoDbAccessLayer.Dtos;

namespace MongoDbAccessLayer.Context.Repository
{
    public interface IProvenanceRepository
    {
        ProvenanceDto Get(string documentId);
    }
}