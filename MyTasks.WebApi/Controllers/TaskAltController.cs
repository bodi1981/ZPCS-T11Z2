using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApi.Extensions;
using MyTasks.WebApi.Models;
using MyTasks.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAltController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskAltController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("get-tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var userId = User.GetUserId();

            var tasks = await _unitOfWork.Task.GetTasksAsync(userId);

            return Ok(tasks);
        }


        [HttpGet("get-task/{taskId}")]
        public async Task<IActionResult> GetTask(int taskId)
        {
            var userId = User.GetUserId();

            var task = await _unitOfWork.Task.GetTaskAsync(taskId, userId);

            return Ok(task);
        }


        [HttpPost("add-task")]
        public async Task<IActionResult> AddTask([FromBody] TaskDomain task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            await _unitOfWork.Task.AddTaskAsync(task);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpPost("edit-task")]
        public async Task<IActionResult> EditTask([FromBody] TaskDomain task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            await _unitOfWork.Task.EditTaskAsync(task);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpDelete("remove-task/{taskId}")]
        public async Task<IActionResult> RemoveTask(int taskId)
        {
            var userId = User.GetUserId();
            await _unitOfWork.Task.DeleteAsync(taskId, userId);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpPatch("finish-task/{taskId}")]
        public async Task<IActionResult> FinishTask(int taskId)
        {
            var userId = User.GetUserId();
            await _unitOfWork.Task.FinishAsync(taskId, userId);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
