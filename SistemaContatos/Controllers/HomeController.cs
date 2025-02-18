﻿using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;
using SistemaContatos.Models;
using System.Diagnostics;

namespace SistemaContatos.Controllers;

public class HomeController : Controller
{
    [PaginaParaUsuarioLogado]

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}