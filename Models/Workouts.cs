using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace NetMongoDb.Models;

[BsonIgnoreExtraElements]
public class Workouts
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("reps")]
    public int Reps { get; set; }
    [BsonElement("load")]
    public int Load { get; set; }


}
