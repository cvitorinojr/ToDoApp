using Microsoft.AspNetCore.Mvc;
using ToDo_Domain.Interfaces.Services;
using TodoApi.ToDo_Domain.Models;

namespace ToDo_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        /// <summary>
        /// Retorna todas as tarefas, com filtros opcionais de status e data.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var result = await taskService.GetAsync(status, startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskModel item)
        {
            try
            {
                await taskService.CreateAsync(item);
                return CreatedAtAction(nameof(Create), new { id = item.Title }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskItem taskItem)
        {
            try
            {
                await taskService.UpdateAsync(taskItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deleta uma tarefa pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await taskService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
