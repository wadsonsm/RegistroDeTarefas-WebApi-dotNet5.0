using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories.Interfaces;

namespace TarefasBackEnd.Controllers
{
    [Authorize]
    [Route("tarefa")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        [HttpGet("/listarTarefas")]
        [AllowAnonymous]
        
        public IActionResult Read([FromServices] ITarefaRepository repository)
        {
            var id = new Guid(User.Identity.Name);
            var tarefas = repository.Read(id);
            return Ok(tarefas);
        }

        [HttpPost("/inserirTarefa")]        
        public IActionResult Create([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.UsuarioId = new Guid(User.Identity.Name);

            repository.Create(model);
            return Ok();
        }

        [HttpPut("/editarTarefa/{id}")]
        
        public IActionResult Update(string id,[FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            repository.Update(new Guid(id), model);
            return Ok();
        }

        [HttpDelete("/excluirTarefa/{id}")]
        
        public IActionResult Delete(string id, [FromServices] ITarefaRepository repository)
        {            
            repository.Delete(new Guid(id));
            return Ok();
        }
    }
}