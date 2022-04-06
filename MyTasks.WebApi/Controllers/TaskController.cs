using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApi.Extensions;
using MyTasks.WebApi.Models;
using MyTasks.WebApi.Models.Domains;
using MyTasks.WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("get-tasks")]
        public async Task<DataResponse<IEnumerable<TaskDomain>>> GetTasks()
        {
            var userId = User.GetUserId();
            var response = new DataResponse<IEnumerable<TaskDomain>>();

            try
            {
                response.Data = await _unitOfWork.Task.GetTasksAsync(userId);
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpGet("get-task/{taskId}")]
        public async Task<DataResponse<TaskDomain>> GetTask(int taskId)
        {
            var userId = User.GetUserId();
            var response = new DataResponse<TaskDomain>();

            try
            {
                response.Data = await _unitOfWork.Task.GetTaskAsync(taskId, userId);
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpPost("add-task")]
        public async Task<Response> AddTask([FromBody] TaskDomain task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            var response = new Response();

            try
            {
                await _unitOfWork.Task.AddTaskAsync(task);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpPut("edit-task")]
        public async Task<Response> EditTask([FromBody] TaskDomain task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            var response = new Response();

            try
            {
                await _unitOfWork.Task.EditTaskAsync(task);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpDelete("delete-task/{taskId}")]
        public async Task<Response> DeleteTask(int taskId)
        {
            var userId = User.GetUserId();
            var response = new Response();

            try
            {
                await _unitOfWork.Task.DeleteAsync(taskId, userId);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpPatch("finish-task/{taskId}")]
        public async Task<Response> FinishTask(int taskId)
        {
            var userId = User.GetUserId();
            var response = new Response();

            try
            {
                await _unitOfWork.Task.FinishAsync(taskId, userId);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


    }
}
