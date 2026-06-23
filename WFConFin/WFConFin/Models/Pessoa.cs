using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFConFin.Models
{
    public class Pessoa
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 a 200 caracteres!")]
        public string Nome { get; set; }

        [StringLength(20, ErrorMessage = "O campo Telefone deve ter até 20 caracteres!")]
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salario { get; set; }

        [StringLength(20, ErrorMessage = "O campo Gênero deve ter até 20 caracteres!")]
        public string Genero { get; set; }

        public Guid? CidadeId { get; set; } // Propriedade para armazenar o ID da cidade associada

        public Pessoa()
        {
            Id = Guid.NewGuid();
        }

        public Cidade Cidade { get; set; } // Relacionamento entre Pessoa e Cidade, onde cada pessoa pertence a uma cidade específica.
    }
}
