using System;
using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Mappers;
using api.Dtos;


namespace api.Controllers
{
    [Route("api/Todos")]
    [ApiController]
    public class TodoController : ControllerBase
	{
        private readonly ITodoRepository _todoRepo;
        public TodoController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var todos = await _todoRepo.GetTodosAsync();
            var tododtos = todos.Select(p => p.ToTodoDto());
            return Ok(tododtos);
        }

        [HttpPost("{projectId:int}")]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequestDto todo, [FromRoute] int projectId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var todoModel = await _todoRepo.CreateTodoAsync(todo, projectId);
            return Ok(todoModel);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTodo([FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _todoRepo.GetTodoAsync(id);
            if(todo == null) return NotFound();
            return Ok(todo.ToTodoDto());
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] UpdateTodoRequestDto updatedto)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var todo = await _todoRepo.UpdateTodoAsync(id, updatedto);
            if(todo == null) return NotFound();
            return Ok(todo.ToTodoDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            var todo = await _todoRepo.DeleteTodoAsync(id);
            if (todo == null) return NotFound();
            return NoContent();
        }

    }
}

