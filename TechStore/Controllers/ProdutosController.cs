using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers;

public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Produtos
    public async Task<IActionResult> Index(string busca, int? categoriaId)
    {
        var categorias = await _context.Categorias
            .OrderBy(c => c.Nome)
            .ToListAsync();

        var listaCategorias = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
        {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "", Text = "Todas as categorias" }
        };
        listaCategorias.AddRange(categorias.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Nome,
            Selected = categoriaId.HasValue && categoriaId.Value == c.Id
        }));

        ViewBag.Categorias = listaCategorias;
        ViewBag.Busca = busca;
        ViewBag.CategoriaId = categoriaId;

        var consulta = _context.Produtos
            .Include(p => p.Categoria)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            consulta = consulta.Where(p => p.Nome.Contains(busca));
        }

        if (categoriaId.HasValue && categoriaId.Value > 0)
        {
            consulta = consulta.Where(p => p.CategoriaId == categoriaId.Value);
        }

        var produtos = await consulta
            .OrderBy(p => p.Nome)
            .ToListAsync();

        return View(produtos);
    }

    // GET: Produtos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // GET: Produtos/Create
    public async Task<IActionResult> Create()
    {
        await CarregarCategorias();
        return View();
    }

    // POST: Produtos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque,CategoriaId")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Produto cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        await CarregarCategorias(produto.CategoriaId);
        return View(produto);
    }

    // GET: Produtos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        await CarregarCategorias(produto.CategoriaId);
        return View(produto);
    }

    // POST: Produtos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque,CategoriaId")] Produto produto)
    {
        if (id != produto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Produto atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
                {
                    return NotFound();
                }
                throw;
            }
        }
        await CarregarCategorias(produto.CategoriaId);
        return View(produto);
    }

    // GET: Produtos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // POST: Produtos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Produto excluído com sucesso!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }

    private async Task CarregarCategorias(int? selecionada = null)
    {
        var categorias = await _context.Categorias
            .OrderBy(c => c.Nome)
            .ToListAsync();
        ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", selecionada);
    }
}
