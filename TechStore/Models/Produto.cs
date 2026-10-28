using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
    [Display(Name = "Estoque")]
    public int QuantidadeEstoque { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    [Display(Name = "Categoria")]
    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    public Categoria Categoria { get; set; } = null!;
}
