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
    public class RolesService : IRolesService
    {
        public HttpClient Client { get; }
        IConfiguration _configuration;
        private readonly string rolesEndpoint = "/api/Role";

        public RolesService(HttpClient client, IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration["BaseUrl"]);
            Client = client;
        }

        public async Task<IEnumerable<Roles>> GetRoles()
        {
            var response = await Client.GetAsync(rolesEndpoint);
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<List<Roles>>(responseStream);
            return r;
        }

        public async Task<Roles> CreateRole(Roles emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(rolesEndpoint, data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Roles>(responseStream);
        }

        public async Task<Roles> UpdateRole(Roles emp)
        {
            var json = JsonConvert.SerializeObject(emp);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PatchAsync($"{rolesEndpoint}/{emp.RoleId}", data);

            var responseStream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Roles>(responseStream);
        }

        public async Task<bool> DeleteRole(int id)
        {
            var response = await Client.DeleteAsync($"{rolesEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<Roles> GetRoleById(int id)
        {
            var response = await Client.GetAsync($"{rolesEndpoint}/{id}");
            var responseStream = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<Roles>(responseStream);
            return r;
        }
    }

}
