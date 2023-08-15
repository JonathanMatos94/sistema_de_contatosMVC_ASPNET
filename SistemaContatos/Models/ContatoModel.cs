using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models;

public class ContatoModel
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Informação obrigatória")]
    public string Nome { get; set; }
    [Required(ErrorMessage ="Digite o E-mail do Contato")]
    [EmailAddress(ErrorMessage ="E-mail inválido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Digite o Celular do Contato")]
    [Phone(ErrorMessage ="Celular informado inválido")]
    public string Celular { get; set; }
    public int? UsuarioId { get; set; }
    public UsuarioModel Usuario { get; set; }

}
