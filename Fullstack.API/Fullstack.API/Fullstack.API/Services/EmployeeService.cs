using Fullstack.API.models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fullstack.API.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> employeeCollection;
        private readonly IMongoDatabase mongoDatabase;

        public EmployeeService(
            IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);

            mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);

            employeeCollection = mongoDatabase.GetCollection<Employee>("Employee");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return mongoDatabase.GetCollection<T>(name);
        }

        public async Task<List<Employee>> GetAsync() =>
            await employeeCollection.Find(_ => true).ToListAsync();

        public async Task<Employee?> GetAsync(string id) {
            
            Employee currEmployee= await employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (currEmployee == null) { return currEmployee; }

            if (currEmployee.ManagerId != null)
            {
                Employee manager = await employeeCollection.Find(x => x.Id == currEmployee.ManagerId).FirstOrDefaultAsync();
                currEmployee.ManagerName = manager.FirstName + " " + manager.LastName;
            }
            else
                currEmployee.ManagerName = "";


            return currEmployee;
            }


        public async Task AddReportAsync(Report newReport)
        { 
            Employee employeeToUpdate = await employeeCollection.Find(x => x.Id == newReport.AssignTo).FirstOrDefaultAsync();
            if (employeeToUpdate != null)
            {
                employeeToUpdate.ReportList.Add(newReport);
            }
            await employeeCollection.ReplaceOneAsync(x => x.Id == newReport.AssignTo, employeeToUpdate);
        }
        public async Task AssignTaskToEmployee(WorkTask newWorkTask)
        {
            Employee employeeToUpdate = await employeeCollection.Find(x => x.Id == newWorkTask.AssignTo).FirstOrDefaultAsync();
            employeeToUpdate.TaskList.Add(newWorkTask);
            await employeeCollection.ReplaceOneAsync(x => x.Id == newWorkTask.AssignTo, employeeToUpdate);
        }



        public async Task CreateAsync(Employee newEmployee)
        {  
            await employeeCollection.InsertOneAsync(newEmployee);
        }

        public async Task UpdateAsync(string id, Employee updatedEmployee) =>
            await employeeCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

        public async Task RemoveAsync(string id) => await employeeCollection.DeleteOneAsync(x => x.Id == id);


    }
}
