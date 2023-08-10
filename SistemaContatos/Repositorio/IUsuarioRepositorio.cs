using SistemaContatos.Models;

namespace SistemaContatos.Repositorio;

public interface IUsuarioRepositorio
{
    UsuarioModel BuscarPorLogin(string login);
    List<UsuarioModel> BuscarTodosContatos();
    UsuarioModel ListarPorId(int id);
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    bool Apagar(int id);
}
