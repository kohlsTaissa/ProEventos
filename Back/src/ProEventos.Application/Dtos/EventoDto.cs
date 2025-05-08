using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório"),
            StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve ter no entre 3 e 50 caracteres")
        // outra forma de validar o tamanho MinLength(3, ErrorMessage = "{0} deve no mínimo 4 caracteres"),
        // MaxLength(50, ErrorMessage = "{0} deve ter no máximo 50 caracteres")
        ]	
        public string Tema { get; set; }

        [Range(1, 120000, ErrorMessage = "{0} não pode ser menor que 1 e maior que 1200.000")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Formato de imagem inválido")]
        public string ImagemUrl { get; set; } 

        [
            Display(Name = "telefone"),
            Required(ErrorMessage = "O campo {0} é obrigatório"),
            Phone(ErrorMessage = "O {0} não é válido")
        ]  
        public string Telefone { get; set; }

        [
            Required(ErrorMessage = "O campo {0} é obrigatório"),
            Display(Name = "e-mail"),
            EmailAddress(ErrorMessage = "O {0} não é válido")
        ]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; } 
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}