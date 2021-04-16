using System.Net.Http;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using BethanysPieShopHRM.UI.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.UI.Services
{
    public class JobsDataService : IJobDataService
    {
        private readonly HttpClient _httpClient;

        public JobsDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await _httpClient.GetJsonAsync<IEnumerable<Job>>("jobs");
        }

        public async Task<Job> GetJobById(int jobId)
        {
            return await _httpClient.GetJsonAsync<Job>($"jobs/{jobId}");
        }

        public async Task AddJob(Job newJob)
        {
        }

        public async Task UpdateJob(Job updatedJob)
        {
        }

        public async Task DeleteJob(int jobId)
        {
        }
    }
}
