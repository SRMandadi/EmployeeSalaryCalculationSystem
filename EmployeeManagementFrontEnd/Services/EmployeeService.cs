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
    public class EmployeeService : IEmployeeService
    {
        public HttpClient Client { get; }
        IConfiguration _configuration;

        private readonly string employeeEndpoint = "/api/employee";

        public EmployeeService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration["BaseUrl"]);
            Client = client;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var response = await Client.GetAsync(employeeEndpoint);
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<List<Employee>>(responseStream);
            return r;
        }

        public async Task<Employee> CreateEmployee(Employee emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(employeeEndpoint, data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Employee>(responseStream);
        }

        public async Task<Employee> UpdateEmployee(Employee emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PatchAsync($"{employeeEndpoint}/{emp.EmployeeId}", data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Employee>(responseStream);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var response = await Client.DeleteAsync($"{employeeEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var response = await Client.GetAsync($"{employeeEndpoint}/{id}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<Employee>(responseStream);
            return r;
        }

        public async Task<Employee> GetEmployeeDetailsByaccessCode(string accessCode)
        {
            string customRoute = employeeEndpoint + "/accessCode";
            var response = await Client.GetAsync($"{customRoute}?&accessCode={accessCode}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<Employee>(responseStream);
            return r;
        }
    }
}
