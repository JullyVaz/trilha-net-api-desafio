using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF - feito!
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada - Realizado!
            
             Tarefa tarefa = _context.Tarefas.Find(id);
                if (tarefa == null)
                return NotFound();
                return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF - Realizado!!!
            
            List<Tarefa> tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData - Realizado!

            List<Tarefa> tarefasPorTitulo = _context.Tarefas.Where(x => x.Titulo == titulo).ToList();
            return Ok(tarefasPorTitulo);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {

            List<Tarefa> tarefaPorData = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefaPorData);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            //Realizado!!!
            List<Tarefa> tarefaPorStatus = _context.Tarefas.Where(x => x.Status == status).ToList();
            return Ok(tarefaPorStatus);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Realizado!!!
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Realizado! 
                 tarefaBanco.Titulo = tarefa.Titulo;
                 tarefaBanco.Descricao = tarefa.Descricao;
                 tarefaBanco.Data = tarefa.Data;
                tarefaBanco.Status = tarefa.Status;
                _context.Tarefas.Update(tarefaBanco);
                _context.SaveChanges();
                return Ok(tarefaBanco);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            Tarefa tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // Realizado!!!
                _context.Tarefas.Remove(tarefaBanco);
                _context.SaveChanges();
                return NoContent();
        }
    }
}