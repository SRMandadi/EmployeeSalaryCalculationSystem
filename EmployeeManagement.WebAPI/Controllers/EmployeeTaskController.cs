using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using EmployeeSalaryCalculationSystem.INFRA.Repositories;
using EmployeeSalaryCalculationSystem.INFRA.Models;
using EmployeeSalaryCalculationSystem.WEBAPI.Models;

namespace EmployeeTaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeTaskController : ControllerBase
    {

        private readonly ILogger<EmployeeTaskController> _logger;
        private IUnitOfWork _unitOfWork;

        public EmployeeTaskController(ILogger<EmployeeTaskController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _unitOfWork.EmployeeTaskRepository.GetAll();
            return this.Ok(employees);
        }

        [HttpGet("search/{empId}")]
        public async Task<IActionResult> GetEmpHourCapacityOfTheDate(int empId, DateTime startDate, DateTime? endDate)
        {
            IEnumerable<EmployeeTask> empTasks = new List<EmployeeTask>();

            if (endDate == null)
            {
                empTasks = await _unitOfWork.EmployeeTaskRepository.FindAsync(empTask => empTask.EmployeeId == empId && empTask.CurrentDate == startDate);
                return this.Ok(empTasks);
            }

            else
            {
                empTasks = await _unitOfWork.EmployeeTaskRepository.FindAsync(empTask => empTask.EmployeeId == empId && (empTask.CurrentDate >= startDate && empTask.CurrentDate <= endDate));
            }

            return this.Ok(empTasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var employeeDetail = await _unitOfWork.EmployeeTaskRepository.Get(id);
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return this.Ok(employeeDetail);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(String searchText)
        {
            var employeeTasks = await _unitOfWork.EmployeeTaskRepository.Find(searchText);
            return this.Ok(employeeTasks);
        }

        [HttpGet("employees-tasks")]
        public async Task<IActionResult> GetEmployeesAndWorkItems(string searchText)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAll();
            var tasks = await _unitOfWork.TaskRepository.GetAll();
            var employeesAndTasks = new EmployeeAndTaskList()
            {
                Employees = employees,
                WorkItems = tasks,
            };
            return this.Ok(employeesAndTasks);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeTask employee)
        {
            _unitOfWork.EmployeeTaskRepository.Add(employee);
            await this._unitOfWork.SaveChangesAsync();

            return this.Ok(employee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, EmployeeTask employeeTask)
        {
            var existingEmployeeTaskDetail = await _unitOfWork.EmployeeTaskRepository.Get(id);
            if (existingEmployeeTaskDetail == null)
            {
                return NotFound();
            }

            MapEmployeeTaskDetails(employeeTask, existingEmployeeTaskDetail);

            _unitOfWork.EmployeeTaskRepository.Update(existingEmployeeTaskDetail);
            await this._unitOfWork.SaveChangesAsync();

            return this.Ok(existingEmployeeTaskDetail);
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var existingEmployeeTaskDetail = await _unitOfWork.EmployeeTaskRepository.Get(id);
            if (existingEmployeeTaskDetail == null)
            {
                return NotFound();
            }

            _unitOfWork.EmployeeTaskRepository.Delete(existingEmployeeTaskDetail);
            await this._unitOfWork.SaveChangesAsync();

            return this.Ok(true);
        }

        private static void MapEmployeeTaskDetails(EmployeeTask employeeTask, EmployeeTask existingEmployeeTaskDetail)
        {
            existingEmployeeTaskDetail.TaskId = employeeTask.TaskId;
            existingEmployeeTaskDetail.EmployeeId = employeeTask.EmployeeId;
            existingEmployeeTaskDetail.TotalNoOfHours = employeeTask.TotalNoOfHours;
            existingEmployeeTaskDetail.CurrentDate = employeeTask.CurrentDate;
            existingEmployeeTaskDetail.Priority = employeeTask.Priority;
            existingEmployeeTaskDetail.PayPerTask = employeeTask.PayPerTask;
        }
    }
}
