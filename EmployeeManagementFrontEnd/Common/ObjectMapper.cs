using System.Collections.Generic;
using System.Linq;
using EmployeeSalaryCalculationSystem.MVC.Entities;
using EmployeeSalaryCalculationSystem.MVC.ViewModels;

namespace EmployeeSalaryCalculationSystem.MVC.Common
{
    public class ObjectMapper : IObjectMapper
    {

        public EmployeeViewModel EmployeeToEmployeeViewModel(Employee employeeEntity)
        {
            return new EmployeeViewModel()
            {
                FirstName = employeeEntity.FirstName,
                AccessCode = employeeEntity.AccessCode,
                EmailAddress = employeeEntity.EmailAddress,
                EmployeeCode = employeeEntity.EmployeeCode,
                EmployeeId = employeeEntity.EmployeeId,
                MobileNo = employeeEntity.MobileNo,
                Surname = employeeEntity.Surname,
                MiddleName = employeeEntity.MiddleName,
                PhysicalAddress = employeeEntity.PhysicalAddress,
                RoleId = employeeEntity.RoleId
            };
        }

        public Employee EmployeeViewModelToEmployee(EmployeeViewModel employeeViewModel)
        {
            return new Employee()
            {
                FirstName = employeeViewModel.FirstName,
                AccessCode = employeeViewModel.AccessCode,
                EmailAddress = employeeViewModel.EmailAddress,
                EmployeeCode = employeeViewModel.EmployeeCode,
                EmployeeId = employeeViewModel.EmployeeId,
                MobileNo = employeeViewModel.MobileNo,
                Surname = employeeViewModel.Surname,
                MiddleName = employeeViewModel.MiddleName,
                PhysicalAddress = employeeViewModel.PhysicalAddress,
                RoleId = employeeViewModel.RoleId,
            };
        }

        public EmployeeTasksViewModel EmployeeTaskToEmployeeTasksViewModel(EmployeeTask employeeTaskEntity)
        {
            return new EmployeeTasksViewModel()
            {
                EmployeeTaskId = employeeTaskEntity.EmployeeTaskId,
                EmployeeId = employeeTaskEntity.EmployeeId,
                TaskId = employeeTaskEntity.TaskId,
                TotalNoOfHours = employeeTaskEntity.TotalNoOfHours,
                CurrentDate = employeeTaskEntity.CurrentDate,
                Priority = employeeTaskEntity.Priority,
                PayPerTask = employeeTaskEntity.PayPerTask,
                EmployeeName = employeeTaskEntity.Employee.FirstName + " " + employeeTaskEntity.Employee.Surname,
                TaskName = employeeTaskEntity.Task.Name,
            };
        }

        public EmployeeTask EmployeeTasksViewModelToEmployeeTasks(EmployeeTasksViewModel employeeTasksViewModel)
        {
            return new EmployeeTask()
            {
                EmployeeTaskId = employeeTasksViewModel.EmployeeTaskId,
                EmployeeId = employeeTasksViewModel.EmployeeId,
                TaskId = employeeTasksViewModel.TaskId,
                TotalNoOfHours = employeeTasksViewModel.TotalNoOfHours,
                CurrentDate = employeeTasksViewModel.CurrentDate,
                Priority = employeeTasksViewModel.Priority,
                PayPerTask = employeeTasksViewModel.PayPerTask,
            };
        }

        public IEnumerable<EmployeeTasksViewModel> EmployeeTasksDTOObjectsToViewModel(IEnumerable<EmployeeTask> employeeTasksCollection, IEnumerable<Employee> employeesCollection, IEnumerable<WorkItem> workItemCollection)
        {
            return employeeTasksCollection.Select(e => new EmployeeTasksViewModel()
            {
                EmployeeTaskId = e.EmployeeTaskId,
                TaskId = e.TaskId,
                EmployeeName = employeesCollection.Where(x => x.EmployeeId == e.EmployeeId).Select(x => x.FirstName + " " + x.Surname).FirstOrDefault().ToString(),
                TaskName = workItemCollection.Where(x => x.TaskId == e.TaskId).Select(x => x.Name).FirstOrDefault().ToString(),
                EmployeeId = e.EmployeeId,
                TotalNoOfHours = e.TotalNoOfHours,
                CurrentDate = e.CurrentDate,
                Priority = e.Priority,
                PayPerTask = e.PayPerTask
            });
        }


        public WorkItemViewModel WorkItemEnityToWorkItemViewModel(WorkItem workItemEnity)
        {
            return new WorkItemViewModel()
            {
                TaskId = workItemEnity.TaskId,
                Name = workItemEnity.Name,
                NoOfHours = workItemEnity.NoOfHours,
                IsCompleted = workItemEnity.IsCompleted,
                IsTaskCompleted = workItemEnity.IsCompleted == true ? "Completed" : "Not Completed",
            };
        }
        public WorkItem WorkItemViewModelToWorkItemEnity(WorkItemViewModel workItemViewModel)
        {
            return new WorkItem()
            {
                TaskId = workItemViewModel.TaskId,
                Name = workItemViewModel.Name,
                NoOfHours = workItemViewModel.NoOfHours,
                IsCompleted = workItemViewModel.IsCompleted,
            };
        }

        public RoleViewModel RoleEntityToRoleViewModel(Roles roleEntity)
        {
            return new RoleViewModel()
            {
                RoleId = roleEntity.RoleId,
                RoleName = roleEntity.RoleName,
                RoleDescription = roleEntity.RoleDescription,
                RatePerhour = roleEntity.RatePerhour,
            };
        }

        public Roles RoleViewModelToRoleEntity(RoleViewModel roleViewModel)
        {
            return new Roles()
            {
                RoleId = roleViewModel.RoleId,
                RoleName = roleViewModel.RoleName,
                RoleDescription = roleViewModel.RoleDescription,
                RatePerhour = roleViewModel.RatePerhour,
            };
        }
    }
}
