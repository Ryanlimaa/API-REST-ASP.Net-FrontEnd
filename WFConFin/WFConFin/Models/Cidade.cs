using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WFConFin.Models
{
    public class Cidade
    {
        [Key]
        public Guid Id { get; set;}

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 a 200 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "O campo Sigla deve ter 2 caracteres!")]
        public string EstadoSigla { get; set; } // Propriedade para armazenar a sigla do estado associado
        public Cidade()
        {
            Id = Guid.NewGuid(); // Gera um GUID para a cidade
        }

        [JsonIgnore] // Ignora a serialização da propriedade Estado para evitar referência circular
        public Estado Estado { get; set; } // Relacionamento entre Cidade e Estado, onde cada cidade pertence a um estado específico. 

    }
}
