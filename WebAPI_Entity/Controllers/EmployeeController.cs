using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WebAPI_Entity.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly GeneralContext _context;

        public EmployeeController(GeneralContext context, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}", Name = "GetAll")]
        public IActionResult GetById(int id)
        {
            var item = _context.Employee.FirstOrDefault(t => t.EmployeeId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            _logger.LogInformation("Index page says hello", new object[0]);
            return _context.Employee.ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Employee.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAll", new { id = item.EmployeeId }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee item)
        {
            if (item == null || item.EmployeeId != id)
            {
                return BadRequest();
            }

            var todo = _context.Employee.FirstOrDefault(t => t.EmployeeId == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.FirstName = item.FirstName;
            todo.LastName = item.LastName;
            todo.EmpCode = item.EmpCode;
            todo.Position = item.Position;
            todo.Office = item.Office;

            _context.Employee.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Employee.FirstOrDefault(t => t.EmployeeId == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}