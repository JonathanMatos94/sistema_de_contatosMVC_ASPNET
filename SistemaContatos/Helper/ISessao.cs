using SistemaContatos.Models;

namespace SistemaContatos.Helper;

public interface ISessao
{
    void CriarSessaoDoUsuario(UsuarioModel usuario);
    void RemoverSessaoDoUsuario();
    UsuarioModel BuscarSessaoDoUsuario();
}
