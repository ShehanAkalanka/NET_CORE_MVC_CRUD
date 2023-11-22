using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET_CORE_MVC_CRUD.Data;
using NET_CORE_MVC_CRUD.Models;
using NET_CORE_MVC_CRUD.Models.Domain;

namespace NET_CORE_MVC_CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DataContext _DataContext;

        public EmployeesController(DataContext dataContext)
        {
            _DataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _DataContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel request)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Salary = request.Salary,
                DOB = request.DOB,
                Department = request.Department
            };

            await _DataContext.Employees.AddAsync(employee);
            await _DataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await _DataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DOB = employee.DOB,
                    Department = employee.Department
                };
                return await Task.Run(()=> View("View",viewModel));
            }
           

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel request)
        {
            var employee = await _DataContext.Employees.FindAsync(request.Id);
            if(employee != null)
            {
                employee.Name = request.Name;
                employee.Email = request.Email;
                employee.Salary = request.Salary;
                employee.DOB = request.DOB;
                employee.Department = request.Department;

                await _DataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel request)
        {
            var employee = await _DataContext.Employees.FindAsync(request.Id);
            if(employee != null)
            {
                _DataContext.Employees.Remove(employee);
                await _DataContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
