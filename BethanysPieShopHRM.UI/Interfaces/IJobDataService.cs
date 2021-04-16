using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using System.Collections.Generic;

namespace BethanysPieShopHRM.UI.Interfaces
{
    public interface IJobDataService
    {
        Task AddJob(Job newJob);
        Task DeleteJob(int jobId);
        Task<IEnumerable<Job>> GetAllJobs();
        Task<Job> GetJobById(int jobId);
        Task UpdateJob(Job job);
    }
}