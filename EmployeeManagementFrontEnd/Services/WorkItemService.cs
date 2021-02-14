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
    public class WorkItemService : IWorkItemService
    {
        public HttpClient Client { get; }
        IConfiguration _configuration;
        private readonly string workItemEndpoint = "/api/WorkItem";

        public WorkItemService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration["BaseUrl"]);
            Client = client;
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItems()
        {
            var response = await Client.GetAsync(workItemEndpoint);
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<List<WorkItem>>(responseStream);
            return r;
        }

        public async Task<WorkItem> CreateWorkItem(WorkItem emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(workItemEndpoint, data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkItem>(responseStream);
        }

        public async Task<WorkItem> UpdateWorkItem(WorkItem emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PatchAsync($"{workItemEndpoint}/{emp.TaskId}", data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkItem>(responseStream);
        }

        public async Task<bool> DeleteWorkItem(int id)
        {
            string tetsBAse = Client.BaseAddress.ToString();
            var response = await Client.DeleteAsync($"{workItemEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<WorkItem> GetWorkItemById(int id)
        {
            var response = await Client.GetAsync($"{workItemEndpoint}/{id}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<WorkItem>(responseStream);
            return r;
        }
    }
}