using System.Linq;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;

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

        protected override async Task OnInitializedAsync()
        {
            Tasks = (await TaskService.GetAllTasks()).ToList();
            if (Count != 0) Tasks = Tasks.Take(Count).ToList();
        }

        public void AddTask()
        {
            NavManager.NavigateTo("taskedit");
        }
    }
}
