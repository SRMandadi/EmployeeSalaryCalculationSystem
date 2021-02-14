using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using EmployeeSalaryCalculationSystem.MVC.Services;
using EmployeeSalaryCalculationSystem.MVC.ViewModels;
using EmployeeSalaryCalculationSystem.MVC.Common;


namespace EmployeeSalaryCalculationSystem.MVC.Controllers
{
    public class RoleController : Controller
    {
        private IRolesService rolesService;
        private readonly ILogger _logger;
        private IObjectMapper _objectMapper;

        public RoleController(IRolesService rolesService, ILogger<RoleController> logger, IObjectMapper objectMapper)
        {
            this.rolesService = rolesService;
            _objectMapper = objectMapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Get All Roles");
            IEnumerable<RoleViewModel> role = await LoadAllRoles();

            return View(role);
        }

        // GET: RoleController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // GET: RoleController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Loading Role Details to View");

            RoleViewModel roleViewModel = await GetRole(id);

            return View(roleViewModel);
        }

        // GET: RoleController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Loading Role Details to Edit");

            RoleViewModel roleViewModel = await GetRole(id);

            return View(roleViewModel);
        }


        // GET: RoleController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting Role");
            try
            {
                var isDeleted = await this.rolesService.DeleteRole(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Deleting Role {0}", ex.Message);
                return View(ex.InnerException.Message);
            }
        }

        // POST: RoleController/Create
        [HttpPost]
        public async Task<ActionResult> CreateAsync(RoleViewModel roleViewModel)
        {
            _logger.LogInformation("Creating A New Role");

            //checking model state
            if (ModelState.IsValid)
            {
                try
                {
                    var emp = await this.rolesService.CreateRole(_objectMapper.RoleViewModelToRoleEntity(roleViewModel));
                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    _logger.LogError("Error Creating a New Role {0}", ex.Message);
                    return View(ex.InnerException.Message);
                }
            }

            return View();
        }

        // POST: RoleController/Edit/5 
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel roleViewModel)
        {
            _logger.LogInformation("Updating Role Details");
            try
            {
                //checking model state
                if (ModelState.IsValid)
                {
                    var emp = await this.rolesService.UpdateRole(_objectMapper.RoleViewModelToRoleEntity(roleViewModel));
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Error Updating Role Details {0}", ex.Message);
                return View(ex.InnerException.Message);
            }

            return View(roleViewModel);
        }

        private async Task<IEnumerable<RoleViewModel>> LoadAllRoles()
        {
            var roles = await this.rolesService.GetRoles();

            var role = roles.Select(r => new RoleViewModel()
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                RoleDescription = r.RoleDescription,
                RatePerhour = r.RatePerhour,
            });
            return role;
        }

        private async Task<RoleViewModel> GetRole(int id)
        {
            var roleEntity = await this.rolesService.GetRoleById(id);

            RoleViewModel roleViewModel = _objectMapper.RoleEntityToRoleViewModel(roleEntity);

            return roleViewModel;
        }
    }
}
