using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace Fullstack.API.models
{
    public class Employee : Person
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public string  Position { get; set; } = null!;

        public string ManagerId { get; set; } = null!;
        public string ManagerName { get; set; } = null!;

        public List<WorkTask> TaskList { get; set; } = new List<WorkTask>();
        
        public List<Subordinate> Subordinates { get; set; } = new List<Subordinate>();

        public List<Report> ReportList { get; set; } = new List<Report>();


    }
}
