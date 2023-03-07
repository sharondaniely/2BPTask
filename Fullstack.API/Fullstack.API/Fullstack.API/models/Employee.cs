using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace Fullstack.API.models
{
    public class Employee : Person
    {
        [BsonId]
       // [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public string  Position { get; set; } = null!;

        public string Managerid { get; set; } = null!;
        public string ManagerName { get; set; } = null!;

        public List<WorkTask> TaskList { get; set; } = new List<WorkTask>();
        ///TODO create an manager object that extends and can be valid by service
        public List<Subordinate> Subordinates { get; set; } = new List<Subordinate>();

        public List<Report> ReportList { get; set; } = new List<Report>();


    }
}
