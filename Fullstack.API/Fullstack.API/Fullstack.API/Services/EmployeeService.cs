using Fullstack.API.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fullstack.API.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(
            IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);

            _employeeCollection = mongoDatabase.GetCollection<Employee>("Employee");
        }

        public async Task<List<Employee>> GetAsync() =>
            await _employeeCollection.Find(_ => true).ToListAsync();

        public async Task<Employee?> GetAsync(string id) {
            
            Employee currEmployee= await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (currEmployee.Managerid != null)
            {
                Employee manager = await _employeeCollection.Find(x => x.Id == currEmployee.Managerid).FirstOrDefaultAsync();
                currEmployee.ManagerName = manager.FirstName + " " + manager.LastName;
            }
            else
                currEmployee.ManagerName = "";


            return currEmployee;
            }

        public async Task CreateAsync(Employee newEmployee) =>
            await _employeeCollection.InsertOneAsync(newEmployee);

        public async Task UpdateAsync(string id, Employee updatedEmployee) =>
            await _employeeCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

        public async Task RemoveAsync(string id) => await _employeeCollection.DeleteOneAsync(x => x.Id == id);


        public async Task AddReportAsync(Report newReport)
        { 
            Employee employeeToUpdate = await _employeeCollection.Find(x => x.Id == newReport.AssignTo).FirstOrDefaultAsync();
            if (employeeToUpdate != null)
            {
                employeeToUpdate.ReportList.Add(newReport);
            }
            await _employeeCollection.ReplaceOneAsync(x => x.Id == newReport.AssignTo, employeeToUpdate);
        }
        public async Task AssignTaskToEmployee(WorkTask newWorkTask)
        {
            Employee employeeToUpdate = await _employeeCollection.Find(x => x.Id == newWorkTask.AssignTo).FirstOrDefaultAsync();
            employeeToUpdate.TaskList.Add(newWorkTask);
            await _employeeCollection.ReplaceOneAsync(x => x.Id == newWorkTask.AssignTo, employeeToUpdate);
        }




    }
}
