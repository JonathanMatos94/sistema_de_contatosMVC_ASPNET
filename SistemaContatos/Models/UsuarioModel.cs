using SistemaContatos.Enums;
using SistemaContatos.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models;

public class UsuarioModel
{
    public virtual List<ContatoModel> Contatos { get; set; }
    public int Id { get; set; }

    [Required(ErrorMessage ="Nome obrigatório!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Login não informado!")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Digite o E-mail.")]
    [EmailAddress(ErrorMessage ="E-mail inválido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe o Perfil do Usuário")]
    public PerfilEnum? Perfil { get; set; }

    [Required(ErrorMessage = "Digite a senha do usuário")]
    public string Senha { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get;set; }
    public bool VerificaSenha(string senha)
    {
        return Senha == senha.GerarHash();
    }
    public void SetSenhaHash()
    {
        Senha = Senha.GerarHash();
    }
    public string GerarNovaSenha()
    {
        string novaSenha = Guid.NewGuid().
            ToString().Substring(0,8);

        Senha = novaSenha.GerarHash();

        return novaSenha;
    }
    public void SetNovaSenha(string novaSenha)
    {
        Senha = novaSenha.GerarHash();
    }
}
