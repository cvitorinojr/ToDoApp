using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using ToDo_Domain.Interfaces.Repositories;
using ToDo_Service.Services;
using TodoApi.ToDo_Domain.Models;
using Xunit;

namespace ToDo_Tests.Service
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repoMock;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repoMock = new Mock<ITaskRepository>();
            _service = new TaskService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsTaskItems()
        {
            var tasks = new List<Todo_Domain.Entities.Task>
            {
                new Todo_Domain.Entities.Task("Tarefa", "Desc", 1, DateTime.UtcNow.AddDays(1))
            };
            _repoMock.Setup(r => r.GetAsync(null, null, null)).ReturnsAsync(tasks);

            var result = await _service.GetAsync(null, null, null);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Tarefa", result.ToList()[0].Title);
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenTitleIsNull()
        {
            var model = new CreateTaskModel { Title = null, Description = "Desc", ExpirationDate = DateTime.UtcNow.AddDays(1) };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(model));
        }

        [Fact]
        public async Task CreateAsync_ThrowsException_WhenExpirationDateIsPast()
        {
            var model = new CreateTaskModel { Title = "Teste", Description = "Desc", ExpirationDate = DateTime.UtcNow.AddDays(-1) };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(model));
        }

        [Fact]
        public async Task CreateAsync_CallsRepository_WhenValid()
        {
            var model = new CreateTaskModel { Title = "Teste", Description = "Desc", ExpirationDate = DateTime.UtcNow.AddDays(1) };

            await _service.CreateAsync(model);

            _repoMock.Verify(r => r.AddAsync(It.IsAny<Todo_Domain.Entities.Task>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            var tasks = new Todo_Domain.Entities.Task("Tarefa", "Desc", 1, DateTime.UtcNow.AddDays(1));

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tasks);

            await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}