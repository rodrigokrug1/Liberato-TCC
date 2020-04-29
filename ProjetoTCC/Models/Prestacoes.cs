namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Linq.Expressions;

    public partial class Prestacoes
    {
        private dynamic nrPrest;

        public Prestacoes()
        {
        }

        public Prestacoes(dynamic nrPrest)
        {
            this.nrPrest = nrPrest;
        }

        [Key]
        [Display(Name = "Número")]
        public int Nrprest { get; set; }

        [Display(Name = "Membro")]
        public int Matricula { get; set; }

        [StringLength(3)]
        public string Conta { get; set; }

        [StringLength(11)]
        public string Chave { get; set; }
        
        [Display(Name = "Sequência")]
        public string Sequencia { get; set; }

        public decimal? Valor { get; set; }

        [Display(Name = "Valor calculado")]
        public decimal? ValorCalculado { get; set; }

        [Display(Name = "Vencimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DtVencimento { get; set; }

        [Display(Name = "Pagamento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DtPagamento { get; set; }
                
        [StringLength(1)]
        [Display(Name = "Situação")]
        public string Situacao { get; set; }
        
        [StringLength(1)]
        [Display(Name = "Forma de pagamento")]
        public string FormaPagamento { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Obs { get; set; }

        [StringLength(100)]
        public string Ass { get; set; }

        public virtual Chaves Chaves { get; set; }

        public virtual Contas Contas { get; set; }

        public virtual FormaPagamento FormaPagamento1 { get; set; }

        public virtual Membros Membros { get; set; }
    }
}
