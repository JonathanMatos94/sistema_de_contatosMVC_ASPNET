using Microsoft.EntityFrameworkCore;
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
    public UsuarioModel BuscarPorEmailELogin(string email, string login)
    {
        return _bancoContext.Usuarios.FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper() &&
                                                          u.Login.ToUpper() == login.ToUpper());
    }
    public List<UsuarioModel> BuscarTodosContatos()
    {
        return _bancoContext.Usuarios
            .Include(x=>x.Contatos)
            .ToList();
    }
    public UsuarioModel ListarPorId(int id)
    {
        return _bancoContext.Usuarios.FirstOrDefault(c => c.Id == id);
    }
    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        usuario.DataCadastro = DateTime.Now;
        usuario.SetSenhaHash();
        _bancoContext.Usuarios.Add(usuario);
        _bancoContext.SaveChanges();
        
        return usuario;
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

        _bancoContext.Usuarios.Update(usuarioDB);
        _bancoContext.SaveChanges();
        
        return usuarioDB;

    }
    public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
    {
        var usuarioDB = ListarPorId(alterarSenhaModel.Id);

        if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não econtrado.");

        if (!usuarioDB.VerificaSenha(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere");

        if (usuarioDB.VerificaSenha(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual.");

        usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
        usuarioDB.DataAtualizacao = DateTime.Now;
        _bancoContext.Usuarios.Update(usuarioDB);
        _bancoContext.SaveChanges();

        return usuarioDB;
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
