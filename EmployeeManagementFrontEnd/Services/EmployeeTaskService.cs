using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using EmployeeSalaryCalculationSystem.MVC.Entities;

namespace EmployeeSalaryCalculationSystem.MVC.Services
{
    public class EmployeeTaskService : IEmployeeTaskService
    {
        public HttpClient Client { get; }
        IConfiguration _configuration;
        private readonly string employeeTaskEndpoint = "/api/EmployeeTask";

        public EmployeeTaskService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration["BaseUrl"]);
            Client = client;
        }
        public async Task<IEnumerable<EmployeeTask>> GetEmployeeTasks()
        {
            var response = await Client.GetAsync(employeeTaskEndpoint);
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<List<EmployeeTask>>(responseStream);
            return r;
        }

        public async Task<EmployeeTask> CreateEmployeeTask(EmployeeTask empTask)
        {
            var json = JsonConvert.SerializeObject(empTask);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(employeeTaskEndpoint, data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeTask>(responseStream);
        }

        public async Task<EmployeeTask> UpdateEmployeeTask(EmployeeTask empTask)
        {
            var json = JsonConvert.SerializeObject(empTask);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PatchAsync($"{employeeTaskEndpoint}/{empTask.EmployeeTaskId}", data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EmployeeTask>(responseStream);
        }

        public async Task<bool> DeleteEmployeeTask(int id)
        {
            var response = await Client.DeleteAsync($"{employeeTaskEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<EmployeeTask> GetEmployeeTaskById(int id)
        {
            var response = await Client.GetAsync($"{employeeTaskEndpoint}/{id}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<EmployeeTask>(responseStream);
            return r;
        }
        public async Task<IEnumerable<EmployeeTask>> GetEmpHourCapacityOfTheDate(int id, DateTime startDate, DateTime? endDate)
        {

            string customRoute = employeeTaskEndpoint + "/search";
            var response = await Client.GetAsync($"{customRoute}/{id}?&startDate={startDate}&endDate={endDate}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<List<EmployeeTask>>(responseStream);

            return r;
        }

        public async Task<IEnumerable<EmployeeAndTaskList>> GetEmployeesAndWorkItems(String searchText)
        {
            string customRoute = "api/EmployeeTask/employees-tasks";

            var response = await Client.GetAsync($"{customRoute}?&searchText={searchText}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<List<EmployeeAndTaskList>>(responseStream);
            return r;
        }
    }
}
