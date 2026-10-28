using System.ComponentModel.DataAnnotations;

namespace TechStore.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }

    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
