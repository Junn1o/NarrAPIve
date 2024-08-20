﻿using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository;
using BlogAPI.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(AppDbContext appDbContext, ICategoryRepository categoryRepository)
        {
            _appDbContext = appDbContext;
            _categoryRepository = categoryRepository;
        }
        [HttpGet("category")]
        public IActionResult GetAllCategory()
        {
            var category = _categoryRepository.GetAllCategory();
            if (category == null)
                return NotFound();
            return Ok(category);
        }
        [Authorize(Roles = "User")]
        [HttpGet("category/{categoryId}")]
        public IActionResult GetCategoryWithIdDTO(Guid categoryId, int pageNumber, int pageSize)
        {
            var category = _categoryRepository.GetCategoryWithIdDTO(categoryId, pageNumber, pageSize);
            if (category == null)
                return NotFound();
            return Ok(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("category")]
        public IActionResult AddCategory([FromForm] CategoryRequestFromDTO addcategoryDTO)
        {
            var category = _categoryRepository.AddCategory(addcategoryDTO);
            if (category == null)
                return Ok(500);
            return Ok(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("category")]
        public IActionResult UpdateCategory(Guid categoryId, [FromForm] CategoryRequestFromDTO updatecategoryDTO)
        {
            var category = _categoryRepository.UpdateCategory(categoryId, updatecategoryDTO);
            if (category == null)
                return NotFound();
            return Ok(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("category")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var category = _categoryRepository.DeleteCategory(categoryId);
            if (category == null)
                return NotFound();
            return Ok(category);
        }
    }
}
