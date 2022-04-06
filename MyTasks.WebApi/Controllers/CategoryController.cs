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
using System.Threading.Tasks;

namespace MyTasks.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("get-categories")]
        public async Task<DataResponse<IEnumerable<Category>>> GetCategories()
        {
            var userId = User.GetUserId();
            var response = new DataResponse<IEnumerable<Category>>();

            try
            {
                response.Data = await _unitOfWork.Category.GetCategoriesAsync(userId);
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpGet("get-category/{categoryId}")]
        public async Task<DataResponse<Category>> GetCategory(int categoryId)
        {
            var userId = User.GetUserId();
            var response = new DataResponse<Category>();

            try
            {
                response.Data = await _unitOfWork.Category.GetCategoryAsync(categoryId, userId);
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpPost("add-category")]
        public async Task<DataResponse<Category>> AddEditCategory([FromBody] Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            var response = new DataResponse<Category>();

            try
            {
                await _unitOfWork.Category.AddCategoryAsync(category);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpPut("edit-category")]
        public async Task<DataResponse<Category>> EditCategory([FromBody] Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            var response = new DataResponse<Category>();

            try
            {
                await _unitOfWork.Category.EditCategoryAsync(category);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error(ex.Source, ex.Message));
            }

            return response;
        }


        [HttpDelete("remove-category/{categoryId}")]
        public async Task<Response> RemoveCategory(int categoryId)
        {
            var userId = User.GetUserId();
            var response = new Response();

            try
            {
                await _unitOfWork.Category.DeleteCategoryAsync(categoryId, userId);
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
