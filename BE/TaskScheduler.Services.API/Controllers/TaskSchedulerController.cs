using Microsoft.AspNetCore.Mvc;
using TaskScheduler.Services.Implementation;
using TaskScheduler.Services.Interfaces;

namespace TaskScheduler.Services.API.Controllers
{
    public class TaskSchedulerController : ControllerBase
    {
        private readonly ITaskSchedulerService taskScheduler;
        //public async Task<Common.Models.DBModels.Task> GetTaskAsync() => await Task.Run(() => 
    }
}
