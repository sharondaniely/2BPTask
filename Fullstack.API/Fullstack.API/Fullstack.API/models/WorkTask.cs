using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace Fullstack.API.models
{
    public class WorkTask : Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Data { get; set; } = null!;
        public DateTime CreationDate { get; set; } =DateTime.Now;
        public DateTime DueDate { get; set; } =DateTime.Now.AddDays(7.0);
        //employee_id
        public string AssignTo { get; set; } = null;
        //manager_id
        public string creator { get; set; } = null!;

    }
}
