using System;
using System.ComponentModel.DataAnnotations;

namespace LojaDeProdutos.API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Descricao { get; set; }
        [Range(1, 10, ErrorMessage = "Valor fora da faixa")]
        public int Quantidade { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
