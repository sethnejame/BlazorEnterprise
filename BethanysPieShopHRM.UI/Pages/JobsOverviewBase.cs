using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using BethanysPieShopHRM.UI.Interfaces;

namespace BethanysPieShopHRM.UI.Pages
{
    public class JobsOverviewBase: ComponentBase
    {
        [Inject]
        public IJobDataService JobService { get; set; }

        public List<Job> Jobs { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Jobs = (await JobService.GetAllJobs()).ToList();
        }
    }
}
