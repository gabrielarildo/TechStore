using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers;

public class CategoriasController : Controller
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Categorias
    public async Task<IActionResult> Index()
    {
        return View(await _context.Categorias
            .Include(c => c.Produtos)
            .OrderBy(c => c.Nome)
            .ToListAsync());
    }

    // GET: Categorias/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .Include(c => c.Produtos)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // GET: Categorias/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categorias/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            _context.Add(categoria);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Categoria cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

    // GET: Categorias/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        return View(categoria);
    }

    // POST: Categorias/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(categoria);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Categoria atualizada com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(categoria.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

    // GET: Categorias/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .Include(c => c.Produtos)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: Categorias/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Produtos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound();
        }

        if (categoria.Produtos.Any())
        {
            TempData["ErrorMessage"] = $"Não é possível excluir a categoria \"{categoria.Nome}\" porque existem {categoria.Produtos.Count} produto(s) vinculado(s). Exclua ou mova os produtos antes de excluir a categoria.";
            return RedirectToAction(nameof(Index));
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Categoria excluída com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    private bool CategoriaExists(int id)
    {
        return _context.Categorias.Any(e => e.Id == id);
    }
}
