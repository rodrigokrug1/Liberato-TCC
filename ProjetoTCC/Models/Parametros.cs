namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Parametros
    {
        [StringLength(30)]
        public string Clube { get; set; }

        [Key]
        [StringLength(14)]
        public string CNPJ { get; set; }

        [StringLength(8)]
        public string CEP { get; set; }

        [StringLength(80)]
        public string Endereco { get; set; }

        [StringLength(5)]
        public string Numero { get; set; }

        [StringLength(5)]
        public string Compl { get; set; }

        [StringLength(40)]
        public string Bairro { get; set; }

        [StringLength(40)]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(20)]
        public string Pais { get; set; }
    }
}
