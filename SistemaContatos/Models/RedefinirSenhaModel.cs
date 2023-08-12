using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models;

public class RedefinirSenhaModel
{
    [Required(ErrorMessage = "Digite o Login")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Digite a Email")]
    public string Email { get; set; }
}
