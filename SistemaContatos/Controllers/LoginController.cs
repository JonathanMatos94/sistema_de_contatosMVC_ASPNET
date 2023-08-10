using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    public LoginController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Entrar(LoginModel loginModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                
                if(usuario!=null)
                {
                    if (usuario.VerificaSenha(loginModel.Senha))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["MensagemErro"] = $"Senha inválida, Tente novamente.";
                }
                else
                {
                    TempData["MensagemErro"] = $"Usuário e/ou Senha inválido(s), Tente novamente.";
                }
            }

            return View("Index");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Não foi possível realizar o login, tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
}
