﻿using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Helper;
using SistemaContatos.Models;
using SistemaContatos.Repositorio;

namespace SistemaContatos.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISessao _sessao;
    private readonly IEmail _email;
    public LoginController(IUsuarioRepositorio usuarioRepositorio,
                           ISessao sessao,
                           IEmail email)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _sessao = sessao;
        _email = email;
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
    public IActionResult RedefinirSenha()
    {
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
    [HttpPost]
    public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                if (usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();                   
                    string msg = $"Sua nova senha é: {novaSenha}";
                    
                    bool emailEnviado = _email.Enviar(usuario.Email,"Sistema de Contatos - Nova Senha",msg);
                    
                    if (emailEnviado)
                    {
                        _usuarioRepositorio.Atualizar(usuario);
                        TempData["MensagemSucesso"] = $"Enviamos para seu E-mail cadastrado uma nova Senha.";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não conseguimos enviar o email de redefinição de senha, tente novamente.";
                    }
                   
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha, verifique os dados informados.";
                }
            }

            return View("Index");
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = $"Não foi possível redefinir sua senha, tente novamente. ERRO: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
}
