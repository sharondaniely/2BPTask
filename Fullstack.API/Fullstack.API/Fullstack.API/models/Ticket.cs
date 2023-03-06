
namespace Fullstack.API.models
{
    public interface Ticket
    {
        public string? Id { get; set; }
        public string Data { get; set; }
        public DateTime CreationDate { get; set; } 
        //employee id
        public string AssignTo { get; set; }
        //employee id
        public string creator { get; set; }
    }
}
