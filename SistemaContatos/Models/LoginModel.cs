﻿using System.ComponentModel.DataAnnotations;

namespace SistemaContatos.Models;

public class LoginModel
{
    [Required(ErrorMessage ="Digite o Login")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Digite a Senha")]
    public string Senha { get; set; }    
}
