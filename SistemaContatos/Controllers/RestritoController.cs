using Microsoft.AspNetCore.Mvc;
using SistemaContatos.Filters;

namespace SistemaContatos.Controllers;

public class RestritoController : Controller
{
    [PaginaParaUsuarioLogado]
    public IActionResult Index()
    {
        return View();
 
    }
}
