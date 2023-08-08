using SistemaContatos.Models;

namespace SistemaContatos.Repositorio;

public interface IContatoRepositorio
{
    List<ContatoModel> BuscarTodosContatos();
    ContatoModel ListarPorId(int id);
    ContatoModel Adicionar(ContatoModel contato);
    ContatoModel Atualizar(ContatoModel contato);
    bool Apagar(int id);
}
