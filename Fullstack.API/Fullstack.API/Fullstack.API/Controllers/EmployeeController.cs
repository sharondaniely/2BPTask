using Fullstack.API.models;
using Fullstack.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Fullstack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService) => _employeeService = employeeService;

        // returns all the employee list
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployeesList() => Ok(await _employeeService.GetAsync());


        // returns an employee instance by id 
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            Employee employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // post a report to the Employee where id = newReport.AssignTo to its ReportList  
        [HttpPost("report")]
        public async Task<ActionResult> Report([FromBody] Report newReport)
        {
            await _employeeService.AddReportAsync(newReport);

            return Ok();
        }
        // post a workTask to the Employee where id = task.AssignTo to its TaskList  
        [HttpPost("assign")]
        public async Task<ActionResult> AssignTask([FromBody] WorkTask task)
        {
            await _employeeService.AssignTaskToEmployee(task);

            return Ok();
        }


       



    }
}