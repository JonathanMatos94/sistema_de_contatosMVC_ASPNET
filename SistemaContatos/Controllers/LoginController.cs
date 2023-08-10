using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Helper;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;
    public LoginController(IUsuarioRepositorio usuarioRepositorio,
                           ISessao sessao)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
    }
    public IActionResult Index()
    {
        //Se usuário tiver logado, redirecionar para Home.
        if (_sessao.BuscarSessaoDoUsuario() != null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    public IActionResult Sair()
    {
        _sessao.RemoverSessaoDoUsuario();
        return RedirectToAction("Index", "Login");
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
                        _sessao.CriarSessaoDoUsuario(usuario);
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
