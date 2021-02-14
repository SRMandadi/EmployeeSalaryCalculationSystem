using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using EmployeeSalaryCalculationSystem.MVC.Services;
using EmployeeSalaryCalculationSystem.MVC.ViewModels;
using EmployeeSalaryCalculationSystem.MVC.Common;

namespace EmployeeSalaryCalculationSystem.MVC.Controllers
{

    public class WorkItemController : Controller
    {
        private IWorkItemService workItemService;
        private IObjectMapper _objectMapper;
        private readonly ILogger _logger;

        public WorkItemController(IWorkItemService workItemService, IObjectMapper objectMapper, ILogger<WorkItemController> logger)
        {
            this.workItemService = workItemService;
            _objectMapper = objectMapper;
            _logger = logger;
        }


        // GET: TaskController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Get All Tasks/WorkItems");
            IEnumerable<WorkItemViewModel> workItems = await GetAllWorkItems();

            return View(workItems);
        }

        // GET: TaskController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // GET: TaskController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Loading WorkItem/Task Details to View");

            var workItemEnity = await this.workItemService.GetWorkItemById(id);
            WorkItemViewModel workItemViewModel = _objectMapper.WorkItemEnityToWorkItemViewModel(workItemEnity);

            return View(workItemViewModel);
        }

        // GET: TaskController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Loading WorkItem/Task Details to Edit");

            var workItemEnity = await this.workItemService.GetWorkItemById(id);
            WorkItemViewModel workItemViewModel = _objectMapper.WorkItemEnityToWorkItemViewModel(workItemEnity);

            return View(workItemViewModel);
        }

        // GET: TaskController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting WorkItem/Task");
            try
            {
                var isDeleted = await this.workItemService.DeleteWorkItem(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Deleting WorkItem {0}", ex.Message);
                return View(ex.InnerException.Message);
            }
        }

        // POST: TaskController/Create
        [HttpPost]
        public async Task<ActionResult> CreateAsync(WorkItemViewModel workItemViewModel)
        {
            _logger.LogInformation("Creating New WorkItem/Task");

            //checking model state
            if (ModelState.IsValid)
            {
                try
                {
                    await this.workItemService.CreateWorkItem(_objectMapper.WorkItemViewModelToWorkItemEnity(workItemViewModel));
                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    _logger.LogError("Error Creating a New WorkItem/Task {0}", ex.Message);
                    return View(ex.InnerException.Message);
                }
            }

            return View();
        }

        // POST: TaskController/Edit/5
        public async Task<ActionResult> EditAsync(WorkItemViewModel workItemViewModel)
        {
            _logger.LogInformation("Updating WorkItem/Task Details");
            try
            {
                //checking model state
                if (ModelState.IsValid)
                {
                    await this.workItemService.UpdateWorkItem(_objectMapper.WorkItemViewModelToWorkItemEnity(workItemViewModel));
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Error Updating WorkItem/Task Details {0}", ex.Message);
                return View(ex.InnerException.Message);
            }

            return View(workItemViewModel);
        }


        private async Task<IEnumerable<WorkItemViewModel>> GetAllWorkItems()
        {
            var workItem = await this.workItemService.GetWorkItems();

            var workItems = workItem.Select(w => new WorkItemViewModel()
            {
                TaskId = w.TaskId,
                Name = w.Name,
                NoOfHours = w.NoOfHours,
                IsTaskCompleted = w.IsCompleted == true ? "Completed" : "Not Completed",
            });
            return workItems;
        }
    }
}