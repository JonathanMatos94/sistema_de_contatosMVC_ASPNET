using SistemaContatos.Models;

namespace SistemaContatos.Repositorio;

public interface IUsuarioRepositorio
{
    UsuarioModel BuscarPorEmailELogin(string login, string email);
    UsuarioModel BuscarPorLogin(string login);
    List<UsuarioModel> BuscarTodosContatos();
    UsuarioModel ListarPorId(int id);
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
    bool Apagar(int id);
}
