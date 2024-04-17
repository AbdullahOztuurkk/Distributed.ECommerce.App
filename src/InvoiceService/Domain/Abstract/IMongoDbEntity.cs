using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InvoiceWorkerService.Entity.Abstract;

public class MongoDbEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; protected set; }
}
