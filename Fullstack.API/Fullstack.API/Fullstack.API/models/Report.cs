using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace Fullstack.API.models
{
    public class Report : Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Data { get; set; } = null!;
        public DateTime CreationDate { get; set; } =DateTime.Now;
        //manager_id
        public string AssignTo { get; set; } = null;
        //employee_id
        public string creator { get; set; } = null!;

    }
}
