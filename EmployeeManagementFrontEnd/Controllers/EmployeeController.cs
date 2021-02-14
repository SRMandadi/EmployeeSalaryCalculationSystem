using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeSalaryCalculationSystem.MVC.Common;
using EmployeeSalaryCalculationSystem.MVC.Services;
using EmployeeSalaryCalculationSystem.MVC.ViewModels;
using System.Collections.Generic;

namespace EmployeeManagement.MVC.Controllers
{
    /// <summary>
    ///  Create and Edit Casual Employees 
    /// </summary>
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private IRolesService _rolesService;
        private IObjectMapper _objectMapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, IRolesService rolesService, IObjectMapper objectMapper, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _objectMapper = objectMapper;
            _rolesService = rolesService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Get All Employees");
            IEnumerable<EmployeeViewModel> employee = await GetAllEmployees();

            return View(employee);
        }

        // GET: EmployeeController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.Role= await LoadEmployeeRoles();

            return View(employeeViewModel);
        }

        // GET: Employee/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Loading Employee Details to View");

            var employee = await _employeeService.GetEmployeeById(id);
            EmployeeViewModel employeeViewModel = _objectMapper.EmployeeToEmployeeViewModel(employee);

            return View(employeeViewModel);
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Loading Employee Details to Edit");

            EmployeeViewModel employeeViewModel = await LoadEmployeeDetails(id);

            return View(employeeViewModel);
        }

        // GET: Employee/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting Employee");
            try
            {
                var isDeleted = await _employeeService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Deleting Employee {0}", ex.Message);
                return View(ex.InnerException.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> CreateAsync(EmployeeViewModel employeeViewModel)
        {
            _logger.LogInformation("Creating A New Employee");

            //checking model state
            if (ModelState.IsValid)
            {
                try
                {
                    var emp = await _employeeService.CreateEmployee(_objectMapper.EmployeeViewModelToEmployee(employeeViewModel));
                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    _logger.LogError("Error Creating a New Employee {0}", ex.Message);
                    return View(ex.InnerException.Message);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(EmployeeViewModel employeeViewModel)
        {
            _logger.LogInformation("Updating Employee Details");
            try
            {
                //checking model state
                if (ModelState.IsValid)
                {
                    var emp = await _employeeService.UpdateEmployee(_objectMapper.EmployeeViewModelToEmployee(employeeViewModel));
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Error Updating Employee Details {0}", ex.Message);
                return View(ex.InnerException.Message);
            }

            return View(employeeViewModel);
        }

        private async Task<IEnumerable<EmployeeViewModel>> GetAllEmployees()
        {
            var employees = await _employeeService.GetEmployees();

            var employee = employees.Select(e => new EmployeeViewModel()
            {
                EmployeeId = e.EmployeeId,
                FullName = e.FirstName + " " + e.Surname,
                MobileNo = e.MobileNo,
                EmailAddress = e.EmailAddress,
                EmployeeRoleName = e.Role.RoleName,
            });
            return employee;
        }

        private async Task<EmployeeViewModel> LoadEmployeeDetails(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            EmployeeViewModel employeeViewModel = _objectMapper.EmployeeToEmployeeViewModel(employee);

            var employeeRole = await _rolesService.GetRoleById(employeeViewModel.RoleId);

            employeeViewModel.EmployeeRoleName = employeeRole.RoleName;
            employeeViewModel.RoleId = employeeRole.RoleId;

            employeeViewModel.Role = await LoadEmployeeRoles();


           // await LoadEmployeeRoles(employeeViewModel);
            return employeeViewModel;
        }

        private async Task<List<RoleViewModel>> LoadEmployeeRoles()
        {
            var rolesList = await _rolesService.GetRoles();
            var roleViewModel = rolesList.Select(p => new RoleViewModel
            {
                RoleId = p.RoleId,
                RoleName = p.RoleName
            }).ToList();
            return roleViewModel;
            // employeeViewModel.Role = roleViewModel;
        }
    }
}