using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;

namespace BethanysPieShopHRM.UI.Components
{
    public class TaskListBase : ComponentBase
    {
        [Parameter]
        public int Count { get; set; }
        public List<HRTask> Tasks { get; set; } = new List<HRTask>();

        [Inject]
        public ITaskDataService TaskService { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public ILogger<TaskListBase> Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Tasks = (await TaskService.GetAllTasks()).ToList();
            }
            catch(Exception e)
            {
                Logger.LogDebug(e, e.Message);
            }

            if (Count != 0) Tasks = Tasks.Take(Count).ToList();
        }

        public void AddTask()
        {
            NavManager.NavigateTo("taskedit");
        }
    }
}
