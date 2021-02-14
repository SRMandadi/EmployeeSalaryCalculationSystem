using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EmployeeSalaryCalculationSystem.INFRA.Models;
using EmployeeSalaryCalculationSystem.INFRA.Repositories;

namespace EmployeeSalaryCalculationSystem.WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var employees = await _unitOfWork.EmployeeRepository.GetAll();
                return this.Ok(employees);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(String searchText)
        {
            var employees = await _unitOfWork.EmployeeRepository.Find(searchText);
            return this.Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        { 
            var employeeDetail = await _unitOfWork.EmployeeRepository.Get(id);
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return this.Ok(employeeDetail);
        }

        [HttpGet("accessCode")]
        public async Task<IActionResult> GetEmployeeDetailsByaccessCode(string accessCode)
        {
            var employeeDetail = await _unitOfWork.EmployeeRepository.GetEmployeeDetailsByaccessCode(accessCode);
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return this.Ok(employeeDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Add(employee);
            await this._unitOfWork.SaveChangesAsync();

            return this.Ok(employee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Employee employee)
        {
            var existingEmployeeDetail = await _unitOfWork.EmployeeRepository.Get(id);
            if (existingEmployeeDetail == null)
            {
                return NotFound();
            }

            MapEmployeeObjects(employee, existingEmployeeDetail);

            _unitOfWork.EmployeeRepository.Update(existingEmployeeDetail);
            await this._unitOfWork.SaveChangesAsync();

            return this.Ok(employee);
        } 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var existingEmployeeDetail = await _unitOfWork.EmployeeRepository.Get(id);
            if (existingEmployeeDetail == null)
            {
                return NotFound();
            }

            _unitOfWork.EmployeeRepository.Delete(existingEmployeeDetail);
            await this._unitOfWork.SaveChangesAsync();

            return this.Ok(true);
        }

        private static void MapEmployeeObjects(Employee employee, Employee existingEmployeeDetail)
        {
            existingEmployeeDetail.FirstName = employee.FirstName;
            existingEmployeeDetail.AccessCode = employee.AccessCode;
            existingEmployeeDetail.EmailAddress = employee.EmailAddress;
            existingEmployeeDetail.EmployeeCode = employee.EmployeeCode;
            existingEmployeeDetail.MobileNo = employee.MobileNo;
            existingEmployeeDetail.Surname = employee.Surname;
            existingEmployeeDetail.MiddleName = employee.MiddleName;
            existingEmployeeDetail.PhysicalAddress = employee.PhysicalAddress;
            existingEmployeeDetail.RoleId = employee.RoleId;
        }
    }
}
