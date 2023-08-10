using SistemaContatos.Data;
using SistemaContatos.Models;

namespace SistemaContatos.Repositorio;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly BancoContext _bancoContext;
    public UsuarioRepositorio(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }
    public UsuarioModel BuscarPorLogin(string login)
    {
        return _bancoContext.Usuarios.FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper());
    }
    public List<UsuarioModel> BuscarTodosContatos()
    {
        return _bancoContext.Usuarios.ToList();
    }
    public UsuarioModel ListarPorId(int id)
    {
        return _bancoContext.Usuarios.FirstOrDefault(c => c.Id == id);
    }
    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        usuario.DataCadastro = DateTime.Now;
        _bancoContext.Usuarios.Add(usuario);
        _bancoContext.SaveChanges();
        
        return(usuario);
    }
    public UsuarioModel Atualizar(UsuarioModel usuario)
    {
        UsuarioModel usuarioDB = ListarPorId(usuario.Id);
        if (usuarioDB == null)
        {
            throw new Exception("Usuário inválido");
        }

        usuarioDB.Nome = usuario.Nome;
        usuarioDB.Email = usuario.Email;
        usuarioDB.Login = usuario.Login;
        usuarioDB.Perfil = usuario.Perfil;
        usuarioDB.DataAtualizacao = DateTime.Now;

        _bancoContext.Update(usuarioDB);
        _bancoContext.SaveChanges();
        
        return (usuarioDB);

    }
    public bool Apagar(int id)
    {
        UsuarioModel usuarioDB = ListarPorId(id);
        if (usuarioDB == null)
        {
            throw new Exception("Usuário inválido");
        }
        _bancoContext.Usuarios.Remove(usuarioDB);
        _bancoContext.SaveChanges();
        return true;

    }

}
