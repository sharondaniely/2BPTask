using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace Fullstack.API.models
{
    public class Manager : Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public List<Employee> Subordinates { get; set; } = new List<Employee>();

        public List<Report> ReportList { get; set; } = new List<Report>();
    }
}
