using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodosContatos();

        return View(usuarios);
    }
    [HttpGet]
    public IActionResult Criar()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Criar(UsuarioModel usuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.Adicionar(usuario);
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            else
            {
                return View(usuario);
            }
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"O Cadastro não foi efetuado, Tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public IActionResult Editar(int id)
    {
        UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario);
    }
    [HttpPost]
    public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
    {
        try
        {
            UsuarioModel usuario = null;
            if (ModelState.IsValid)
            {
                usuario = new UsuarioModel()
                {
                    Id = usuarioSemSenhaModel.Id,
                    Nome = usuarioSemSenhaModel.Nome,
                    Login = usuarioSemSenhaModel.Login,
                    Email = usuarioSemSenhaModel.Email,
                    Perfil = usuarioSemSenhaModel.Perfil
                };
                _usuarioRepositorio.Atualizar(usuario);
                TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                return RedirectToAction("Index");
            }
            else
            {
                return View(usuario);
            }

        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"A atualização não foi efetuada, Tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public IActionResult ApagarConfirmacaoUsuario(int id)
    {
        UsuarioModel contato = _usuarioRepositorio.ListarPorId(id);
        return View(contato);
    }
    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _usuarioRepositorio.Apagar(id);
            if (apagado)
            {
                TempData["MensagemSucesso"] = "Usuário apagado com Sucesso";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar o usuário, tente novamente.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {

            TempData["MensagemErro"] = $"O usuário não foi apagado, Tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");

        }
    }
}
