using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;
[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength (20, ErrorMessage = "O nome deve ter entre 5 e 20 caracteres",
        MinimumLength = 5)]
    //[PrimeiraLetraMaiuscula]
    public string Nome { get; set; }
    [Required]
    [MaxLength(300)]
    public string Descricao { get; set; }
    [Required]
    [Range(1,10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }
    [Required]
    [MaxLength(500)]
    public string ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public Categoria Categoria { get; set; }
    public int CategoriaId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(Nome))
        {
            var primeiraLetra = Nome[0].ToString();
            if(primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do " +
                    "produto deve ser maiúscula.", new[] {nameof(Nome)});

            }
        }
        if(Estoque <= 0)
        {
            yield return new ValidationResult("O estoque deve ser " +
                    "maior que zero.", new[] { nameof(Estoque) });
        }
    }
}
