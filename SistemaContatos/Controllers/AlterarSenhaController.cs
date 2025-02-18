﻿using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Helper;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers;

public class AlterarSenhaController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;
    public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio,
                                  ISessao sessao)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;   
    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
    {
        try
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            alterarSenhaModel.Id = usuarioLogado.Id;

            if (ModelState.IsValid)
            {
                _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                TempData["MensagemSucesso"] = $"Senha alterada.";
                return View("Index", alterarSenhaModel);
            }
            
            return View("Index",alterarSenhaModel);
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Não conseguimos alterar sua senha, por favor tente novamente.ERRO: {ex.Message}";
            return View("Index", alterarSenhaModel);
        }
    }
}
