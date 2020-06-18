using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ProjetoTCC
{
    public class Functions
    {
        public static string Conexao()
        {
            string strConexao = ConfigurationManager.ConnectionStrings["EstudoTCCDB"].ConnectionString;

            return strConexao;
        }

        #region Prestações
        /// <summary>
        /// Verifica se não existe outra prestação baseada na chave única da tabela (matrícula, conta, chave, sequência)
        /// para evitar duplicidade de prestações inseridas no banco de dados.
        /// </summary>
        /// <param name="Matricula"></param>
        /// <param name="Conta"></param>
        /// <param name="Chave"></param>
        /// <param name="Vencimento"> Vencimento é convertido em sequência (yyyyMM) </param>
        /// <returns></returns>
        public static bool ValidaPrestacao(int Matricula, string Conta, string Chave, DateTime Vencimento)
        {
            using
            (
            var connection = new SqlConnection(Conexao())
            )
            {
                var prestacao = connection
                    .Query<Prestacoes>("SELECT TOP 1 NrPrest FROM Prestacoes(NOLOCK) WHERE Matricula = @Matricula AND Conta = @Conta AND Chave = @Chave AND Sequencia = @Sequencia",
                    new { Matricula, Conta, Chave, @Sequencia = Vencimento.ToString("yyyyMM") }).ToList();

                if (prestacao.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion

        #region Membros
        /// <summary>
        /// Verifica se o CPF ou CNPJ é válido. Validação feita por função no banco de dados.
        /// </summary>
        /// <param name="CPFCNPJ"></param>
        /// <returns></returns>
        [HttpPost]
        public static bool ValidaCPFCNPJ(string CPFCNPJ)
        {
            using
            (
                var connection = new SqlConnection(Conexao())
            )
            {
                CPFCNPJ = CPFCNPJ.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);

                var retorno = connection.ExecuteScalar<int>("SELECT dbo.FC_VALIDA_CNPJCPF('" + CPFCNPJ + "')");

                if (retorno == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        /// <summary>
        /// Busca o CNPJ na tabela de parâmetros.
        /// </summary>
        /// <returns></returns>
        public static string BuscaParametro()
        {
            using
            (
            var connection = new SqlConnection(Conexao())
            )
            {
                var CNPJ = connection.ExecuteScalar<string>("SELECT TOP 1 CNPJ FROM Parametros(NOLOCK)");

                return CNPJ.ToString();
            }
        }
    }
}