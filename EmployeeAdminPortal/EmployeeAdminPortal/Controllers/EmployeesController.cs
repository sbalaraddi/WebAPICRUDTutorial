using EmployeeAdminPortal.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var emmployeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            dbContext.Employees.Add(emmployeeEntity);
            dbContext.SaveChanges();

            return Ok(emmployeeEntity);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound("Employee Not Found");
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateemployee )
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Employee Not Found, So Cannot Update");
            }

            employee.Email = updateemployee.Email;
            employee.Name = updateemployee.Name;
            employee.Salary = updateemployee.Salary;
            employee.Phone = updateemployee.Phone;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployeeById(Guid id) 
        {
            var employee = dbContext.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Employee Not Found, So Cannot Update");
            }
            dbContext.Remove(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }
    }
}
