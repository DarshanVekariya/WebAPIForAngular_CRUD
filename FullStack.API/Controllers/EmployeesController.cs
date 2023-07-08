using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext=fullStackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext.Employees.ToListAsync();

            return Ok(employees);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployees([FromBody]Employee employee)
        {
            employee.Id=Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(employee);

            _fullStackDbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateEmploye([FromRoute]Guid id ,Employee employee)
        {
            var emp = await _fullStackDbContext.Employees.FindAsync(id);
            if (emp==null)
            {
                return NotFound();
            }

            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Phone =employee.Phone;
            emp.Salary = employee.Salary;
            emp.Department = employee.Department;
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> deleteEmploye([FromRoute] Guid id)
        {
            var emp = await _fullStackDbContext.Employees.FindAsync(id);
            if (emp==null)
            {
                return NotFound();
            }

            _fullStackDbContext.Employees.Remove(emp);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
        }
    }
}
