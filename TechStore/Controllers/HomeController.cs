using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalProdutos = await _context.Produtos.CountAsync();
        var totalCategorias = await _context.Categorias.CountAsync();
        var totalEstoque = await _context.Produtos.SumAsync(p => (int?)p.QuantidadeEstoque) ?? 0;

        ViewBag.TotalProdutos = totalProdutos;
        ViewBag.TotalCategorias = totalCategorias;
        ViewBag.TotalEstoque = totalEstoque;

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
