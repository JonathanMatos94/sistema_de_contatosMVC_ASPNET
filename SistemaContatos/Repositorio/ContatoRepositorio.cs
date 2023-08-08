using SistemaContatos.Data;
using SistemaContatos.Models;

namespace SistemaContatos.Repositorio;

public class ContatoRepositorio : IContatoRepositorio
{
    private readonly BancoContext _bancoContext;
    public ContatoRepositorio(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }
    public List<ContatoModel> BuscarTodosContatos()
    {
        return _bancoContext.Contatos.ToList();
    }
    public ContatoModel ListarPorId(int id)
    {
        return _bancoContext.Contatos.FirstOrDefault(c => c.Id == id);
    }
    public ContatoModel Adicionar(ContatoModel contato)
    {
        _bancoContext.Contatos.Add(contato);
        _bancoContext.SaveChanges();
        
        return(contato);
    }
    public ContatoModel Atualizar(ContatoModel contato)
    {
        ContatoModel contatoDB = ListarPorId(contato.Id);
        if (contatoDB == null)
        {
            throw new Exception("Contato inválido");
        }

        contatoDB.Nome = contato.Nome;
        contatoDB.Email = contato.Email;
        contatoDB.Celular = contato.Celular;
        _bancoContext.Update(contatoDB);
        _bancoContext.SaveChanges();
        
        return (contatoDB);

    }
    public bool Apagar(int id)
    {
        ContatoModel contatoDB = ListarPorId(id);
        if (contatoDB == null)
        {
            throw new Exception("Contato inválido");
        }
        _bancoContext.Contatos.Remove(contatoDB);
        _bancoContext.SaveChanges();
        return true;

    }



}
