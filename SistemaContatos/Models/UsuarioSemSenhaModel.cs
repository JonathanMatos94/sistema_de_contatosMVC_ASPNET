using SistemaContatos.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models;

public class UsuarioSemSenhaModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Nome obrigatório!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Login não informado!")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Digite o E-mail.")]
    [EmailAddress(ErrorMessage ="E-mail inválido!")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Informe o Perfil do Usuário")]
    public PerfilEnum? Perfil { get; set; }
}
