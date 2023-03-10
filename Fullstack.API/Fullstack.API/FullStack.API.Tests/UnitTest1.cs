using Amazon.Util.Internal.PlatformServices;
using Fullstack.API.Controllers;
using Fullstack.API.Services;
using FullStack.API;
using MongoDB.Driver;
using Xunit;
using Moq;
using Fullstack.API.models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FullStack.API.Tests
{

    public class UnitTest1
    {
        private Mock<IOptions<DatabaseSettings>> _mockDBSettings;
        private Mock<IMongoClient> mongoClient;
        private Mock<IMongoDatabase> mongodb;
        private Mock<IMongoCollection<Employee>> EmployeeMockCollection;


        private Report report = new Report()
        {
            Id = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss"),
            CreationDate = DateTime.Now,
            Data = "Random data",
            AssignTo = "111",
            creator = "1234"

        };


        private WorkTask workTask = new WorkTask()
        {
            Id = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss"),
            CreationDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(7),
            Data = "Random data",
            AssignTo = "111",
            creator = "1234"

        };
        private Employee testEmployee= new Employee()
        {
            
            Id = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss"),
            FirstName = "Harry",
            LastName = "Potter",
            PictureUrl = "",
            Position = "Magician",
            ManagerId = null,
            ManagerName = "",
            TaskList = new List<WorkTask>(),
            Subordinates = new List<Subordinate>(),
            ReportList = new List<Report>()
        };

        private Employee returnedEmployeeVaild = new Employee()
        {

            Id = "1234",
            FirstName = "Harry",
            LastName = "Potter",
            PictureUrl = "",
            Position = "Magician",
            ManagerId = null,
            ManagerName = "",
            TaskList = new List<WorkTask>(),
            Subordinates = new List<Subordinate>(),
            ReportList = new List<Report>()
        };


        private Employee[] MockEmployeeList = new Employee[]
            { new Employee() {
                Id = "1",
                FirstName = "Clark",
                LastName = "Kent",
                PictureUrl = "",
                Position = "Superman",
                ManagerId = null,
                ManagerName = "",
                TaskList = new List<WorkTask>(),
                Subordinates = new List<Subordinate>()
                {
                    new Subordinate()
                    {
                        Id = "2",
                        FirstName = "Forrest",
                        LastName = "Gump",
                        Position = "Runner"
                    },
                    new Subordinate()
                    {
                        Id = "3",
                        FirstName = "Peter",
                        LastName = "Parker",
                        Position = "Spiderman"
                    }

                },
                ReportList = new List<Report>()
                },
                new Employee() {
                    Id = "2",
                    FirstName = "Forrest",
                    LastName = "Gump",
                    PictureUrl = "",
                    Position = "Runner",
                    ManagerId = "1",
                    ManagerName = "",
                    TaskList = new List<WorkTask>(),
                    Subordinates = new List<Subordinate>()
                    {
                        new Subordinate()
                        {
                            Id = "4",
                            FirstName = "Harry",
                            LastName = "Potter",
                            Position = "Magician"
                        }


                    },
                    ReportList = new List<Report>()
                },
                new Employee() {
                    Id = "3",
                    FirstName = "Peter",
                    LastName = "Parker",
                    PictureUrl = "",
                    Position = "Spiderman",
                    ManagerId = "1",
                    ManagerName = "",
                    TaskList = new List<WorkTask>(),
                    Subordinates = new List<Subordinate>(),
                    ReportList = new List<Report>()
                },
                new Employee() {
                    Id = "4",
                    FirstName = "Harry",
                    LastName = "Potter",
                    PictureUrl = "",
                    Position = "Magician",
                    ManagerId = "2",
                    ManagerName = "",
                    TaskList = new List<WorkTask>(),
                    Subordinates = new List<Subordinate>(),
                    ReportList = new List<Report>()
                }
            };
        private string insertedEmployeeId="";
        public UnitTest1()
        {
            this.mongoClient = new Mock<IMongoClient>();
            this.EmployeeMockCollection = new Mock<IMongoCollection<Employee>>();
            this.mongodb = new Mock<IMongoDatabase>();
            this._mockDBSettings = new Mock<IOptions<DatabaseSettings>>();
        }



        [Fact]
            public void a_MongoCollectionContractorSuccess()
            {
                var settings = new DatabaseSettings()
                {
                    ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                    DatabaseName = "TestDB"
                };

                _mockDBSettings.Setup(s => s.Value).Returns(settings);
                mongoClient.Setup(c => c
                .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                    .Returns(mongodb.Object);

                //Act 
                var service = new EmployeeService(_mockDBSettings.Object);

                //Assert 
                Assert.NotNull(service);

           
            }

        [Fact]
        public void b_GetMongoCollectionNameEmptyFailure()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            var myCollection = service.GetCollection<Employee>("");

            //Assert 
            Assert.Null(myCollection);


        }

        [Fact]
        public void c_GetMongoCollectionValidNameSuccess()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            var myCollection = service.GetCollection<Employee>("Employee");

            //Assert 
            Assert.NotNull(myCollection);


        }



        [Fact]
        public async Task e_InsertEmployeeListSuccess()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            

            int employeeListCountBeforeInsertion = service.GetAsync().Result.Count();
            await service.CreateAsync(testEmployee);
            int employeeListCountAfterInsertion = service.GetAsync().Result.Count();

            Assert.True(employeeListCountBeforeInsertion + 1 == employeeListCountAfterInsertion);

        }

        [Fact]
        public void f_GetEmployeeListSuccess()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            List<Employee> employees = service.GetAsync().Result;

            //Assert 
            Assert.NotEmpty(employees);


        }


        [Fact]
        public void g_GetEmployeeEmptyIDFailure()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            Employee? employee = service.GetAsync("").Result;

            //Assert 
            Assert.Null(employee);


        }

        [Fact]
        public void h_GetEmployeeValidIDSuccess()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            Employee? employee = service.GetAsync("1234").Result;

            var obj1Str = JsonConvert.SerializeObject(employee);
            var obj2Str = JsonConvert.SerializeObject(returnedEmployeeVaild);

            //Assert 
            Assert.Equal(obj1Str, obj2Str);

      

        }

        [Fact]
        public async Task i_InsertReportSuccess()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);
            
            int reportListCountBefore = service.GetAsync("111").Result.ReportList.Count;
            await service.AddReportAsync(report);
            int reportListCountAfter = service.GetAsync("111").Result.ReportList.Count;

            Assert.True(reportListCountBefore + 1 == reportListCountAfter);
        }


        [Fact]
        public async Task i_assignTaskSuccess()
        {
            var settings = new DatabaseSettings()
            {
                ConnectionString = "mongodb+srv://sharondaniely7:Portugal2023@sharoncluster.dt5dq8g.mongodb.net/test",
                DatabaseName = "TestDB"
            };

            _mockDBSettings.Setup(s => s.Value).Returns(settings);
            mongoClient.Setup(c => c
            .GetDatabase(_mockDBSettings.Object.Value.DatabaseName, null))
                .Returns(mongodb.Object);

            //Act 
            var service = new EmployeeService(_mockDBSettings.Object);

            int taskListCountBeforeAssignTask = service.GetAsync("111").Result.TaskList.Count;
            await service.AssignTaskToEmployee(workTask);
            int taskListCountAfterAssignTask = service.GetAsync("111").Result.TaskList.Count;

            Assert.True(taskListCountBeforeAssignTask + 1 == taskListCountAfterAssignTask);
        }

    }
    }
