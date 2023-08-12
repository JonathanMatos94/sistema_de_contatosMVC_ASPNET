using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models;

public class AlterarSenhaModel
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Senha atual não confere.")]
    public string SenhaAtual { get; set; }

    [Required(ErrorMessage = "Digite sua nova senha.")]
    public string NovaSenha { get; set; }

    [Required(ErrorMessage = "Digite a confirmação da sua nova senha.")]
    [Compare("NovaSenha", ErrorMessage ="Senha diferente de Nova Senha")]
    public string ConfirmarNovaSenha { get; set; }
}
