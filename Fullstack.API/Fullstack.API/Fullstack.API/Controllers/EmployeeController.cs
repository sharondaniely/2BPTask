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

        [HttpGet]
        public async Task<List<Employee>> Get() => await _employeeService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return employee;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee newEmployee)
        {
            await _employeeService.CreateAsync(newEmployee);

            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Employee updatedEmployee)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            updatedEmployee.Id = employee.Id;

            await _employeeService.UpdateAsync(id, updatedEmployee);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            await _employeeService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPost("~/report")]
        public async Task<IActionResult> Report([FromBody] Report newReport)
        {
            await _employeeService.AddReportAsync(newReport);

            return Ok();
        }

        [HttpPost("~/assign")]
        public async Task<IActionResult> AssignTask([FromBody] WorkTask task)
        {
            await _employeeService.AssignTaskToEmployee(task);

            return Ok();
        }



    }
}