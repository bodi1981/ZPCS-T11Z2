using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTasks.WebApi.Extensions;
using MyTasks.WebApi.Models;
using MyTasks.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAltController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryAltController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var userId = User.GetUserId();
            var categories = await _unitOfWork.Category.GetCategoriesAsync(userId);

            return Ok(categories);
        }


        [HttpGet("get-category/{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var userId = User.GetUserId();

            var categories = await _unitOfWork.Category.GetCategoryAsync(categoryId, userId);

            return Ok(categories);
        }


        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            await _unitOfWork.Category.AddCategoryAsync(category);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpPut("edit-category")]
        public async Task<IActionResult> EditCategory([FromBody] Category category)
        {
            var userId = User.GetUserId();
            category.UserId = userId;

            await _unitOfWork.Category.EditCategoryAsync(category);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }


        [HttpDelete("remove-category/{categoryId}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var userId = User.GetUserId();

            await _unitOfWork.Category.DeleteCategoryAsync(categoryId, userId);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
