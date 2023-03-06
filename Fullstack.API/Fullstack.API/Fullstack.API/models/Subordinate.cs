using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace Fullstack.API.models
{
    public class Subordinate : Person
    {
        [BsonId]
       // [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastNAme { get; set; } = null!;
        public string  Position { get; set; } = null!;

    }
}
