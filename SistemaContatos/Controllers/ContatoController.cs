using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Helper;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers;

public class ContatoController : Controller
{
    private readonly IContatoRepositorio _contatoRepositorio;
    private readonly ISessao _sessao;
    public ContatoController(IContatoRepositorio contatoRepositorio,
                             ISessao sessao)
    {
        _contatoRepositorio = contatoRepositorio;
        _sessao = sessao;
    }

    [PaginaParaUsuarioLogado]

    public IActionResult Index()
    {
        UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
        List<ContatoModel> contatos = _contatoRepositorio.BuscarTodosContatos(usuarioLogado.Id);

        return View(contatos);
    }

    public IActionResult Criar()
    {
        return View();
    }

    public IActionResult Editar(int id)
    {
        ContatoModel contato = _contatoRepositorio.ListarPorId(id);
        return View(contato);
    }
    
    public IActionResult ApagarConfirmacao(int id)
    {
        ContatoModel contato = _contatoRepositorio.ListarPorId(id);
        return View(contato);
    }
    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _contatoRepositorio.Apagar(id);
            if (apagado)
            {
                TempData["MensagemSucesso"] = "Contato apagado com Sucesso";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar seu contato, tente novamente.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {

            TempData["MensagemErro"] = $"O contato não foi apagado, Tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");

        }
    }
    [HttpPost]
    public IActionResult Criar(ContatoModel contato)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                contato.UsuarioId = usuarioLogado.Id;

                _contatoRepositorio.Adicionar(contato);
                TempData["MensagemSucesso"] = "Contato Cadastrado com Sucesso";
                return RedirectToAction("Index");
            }
            else
            {
                return View(contato);
            }
        }
        catch(Exception ex)
        {
            TempData["MensagemErro"] = $"O Cadastro não foi efetuado, Tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    public IActionResult Editar(ContatoModel contato)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                contato.UsuarioId = usuarioLogado.Id;

                _contatoRepositorio.Atualizar(contato);
                TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                return RedirectToAction("Index");
            }
            else
            {
                return View(contato);
            }

        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"A atualização não foi efetuada, Tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
}
