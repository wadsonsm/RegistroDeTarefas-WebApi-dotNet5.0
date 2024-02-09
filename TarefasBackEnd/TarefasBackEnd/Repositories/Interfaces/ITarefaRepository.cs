using System;
using System.Collections.Generic;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories.Interfaces
{
    public interface ITarefaRepository
    {
        List<Tarefa> Read();
        void Create(Tarefa tarefa);
        void Delete(Guid id);
        void Update(Guid id, Tarefa tarefa);
    }
}
