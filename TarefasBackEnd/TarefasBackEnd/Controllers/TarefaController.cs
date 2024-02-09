using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories.Interfaces;

namespace TarefasBackEnd.Controllers
{
    [Route("tarefa")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        [HttpGet("/listarTarefas")]
        
        public IActionResult Read([FromServices] ITarefaRepository repository)
        {
            var tarefas = repository.Read();
            return Ok(tarefas);
        }

        [HttpPost("/inserirTarefa")]
        
        public IActionResult Craete([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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