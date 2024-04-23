using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Invoice.Service.Domain.Abstract;

public class MongoDbEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; protected set; }
}
