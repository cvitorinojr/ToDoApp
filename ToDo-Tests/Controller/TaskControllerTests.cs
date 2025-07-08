using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDo_Api.Controllers;
using ToDo_Domain.Interfaces.Services;
using TodoApi.ToDo_Domain.Models;
using Xunit;

namespace ToDo_Tests.Controller
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _serviceMock;
        private readonly TasksController _controller;

        public TaskControllerTests()
        {
            _serviceMock = new Mock<ITaskService>();
            _controller = new TasksController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithTaskItems()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Tarefa 1", Description = "Desc", StatusId = 1, ExpirationDate = DateTime.UtcNow }
            };
            _serviceMock.Setup(s => s.GetAsync(null, null, null)).ReturnsAsync(tasks);

            var result = await _controller.GetAll(null, null, null);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(tasks, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var model = new CreateTaskModel { Title = "Nova", Description = "Desc", ExpirationDate = DateTime.UtcNow.AddDays(1) };
            _serviceMock.Setup(s => s.CreateAsync(model)).Returns(Task.CompletedTask);

            var result = await _controller.Create(model);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.Create), createdResult.ActionName);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenIdsMatch()
        {
            var item = new TaskItem { Id = 1, Title = "Edit", Description = "Desc", StatusId = 1, ExpirationDate = DateTime.UtcNow };
            _serviceMock.Setup(s => s.UpdateAsync(item)).Returns(Task.CompletedTask);

            var result = await _controller.Update(item);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            _serviceMock.Setup(s => s.DeleteAsync(1)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}