using MySql.Data.MySqlClient;
using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Data
{
    public class DBContext
    {
        static MySqlConnection Conexao;
        //Estabelece a conexão com o banco de dados
        public static void ConfiguracaoConexao()
        {
            try
            {
                string? data_source = LerConnectionStringDeArquivo("AppSettings.txt");

                if (data_source != null)
                {
                    Conexao = new MySqlConnection(data_source);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar com o banco de dados: " + ex.Message);
            }
            
        }
        //Insere a informações da cerimonia no banco de dados
        public static void InserirDadosNoBanco(int numConvidados, List<Festa> festas)
        {
            try
            {
                Festa? ultimaFesta = festas.LastOrDefault();
                Bebidas? bebidasUltimaFesta = Bebidas.Qntbebidas.LastOrDefault();

                if (ultimaFesta != null)
                {
                    string sql = "INSERT INTO cerimonias (tipoFesta, data, identificador, capacidade, convidados, precoTotal, nivelItens, qntAgua, qntSuco, qntRefri, qntCervejaComum, qntCervejaArtesanal, qntEspumanteNacional, qntEspumanteImportado, precoEspaco, precoFesta) " +
                                 "VALUES (@TipoFesta, @Data, @Identificador, @Capacidade, @Convidados, @PrecoTotal, @NivelItens, @QntAgua, @QntSuco, @QntRefri, @QntCervejaComum, @QntCervejaArtesanal, @QntEspumanteNacional, @QntEspumanteImportado, @PrecoEspaco, @PrecoFesta)";

                    using MySqlCommand comando = new MySqlCommand(sql, Conexao);
                    comando.Parameters.AddWithValue("@TipoFesta", ultimaFesta.GetTipoFesta().ToString());
                    comando.Parameters.AddWithValue("@Data", ultimaFesta.GetData());
                    comando.Parameters.AddWithValue("@Identificador", ultimaFesta.GetEspaco().GetIdentificador());
                    comando.Parameters.AddWithValue("@Capacidade", ultimaFesta.GetEspaco().GetCapacidade());
                    comando.Parameters.AddWithValue("@Convidados", numConvidados);
                    if ((int)ultimaFesta.GetTipoFesta() != 5)
                    {
                        comando.Parameters.AddWithValue("@PrecoTotal", ultimaFesta.GetPreco() + ultimaFesta.GetEspaco().GetPreco());
                        comando.Parameters.AddWithValue("@NivelItens", ultimaFesta.GetNivelFesta().ToString());
                        comando.Parameters.AddWithValue("@QntAgua", bebidasUltimaFesta.QntAgua);
                        comando.Parameters.AddWithValue("@QntSuco", bebidasUltimaFesta.QntSuco);
                        comando.Parameters.AddWithValue("@QntRefri", bebidasUltimaFesta.QntRefri);
                        comando.Parameters.AddWithValue("@QntCervejaComum", bebidasUltimaFesta.QntCervejaComum);
                        comando.Parameters.AddWithValue("@QntCervejaArtesanal", bebidasUltimaFesta.QntCervejaArtesanal);
                        comando.Parameters.AddWithValue("@QntEspumanteNacional", bebidasUltimaFesta.QntEspumanteNacional);
                        comando.Parameters.AddWithValue("@QntEspumanteImportado", bebidasUltimaFesta.QntEspumanteImportado);
                        comando.Parameters.AddWithValue("@PrecoFesta", ultimaFesta.GetPreco());
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@PrecoTotal", ultimaFesta.GetEspaco().GetPreco());
                        comando.Parameters.AddWithValue("@NivelItens", "Livre");
                        comando.Parameters.AddWithValue("@QntAgua", 0);
                        comando.Parameters.AddWithValue("@QntSuco", 0);
                        comando.Parameters.AddWithValue("@QntRefri", 0);
                        comando.Parameters.AddWithValue("@QntCervejaComum", 0);
                        comando.Parameters.AddWithValue("@QntCervejaArtesanal", 0);
                        comando.Parameters.AddWithValue("@QntEspumanteNacional", 0);
                        comando.Parameters.AddWithValue("@QntEspumanteImportado", 0);
                        comando.Parameters.AddWithValue("@PrecoFesta", 0);
                    }
                    
                    comando.Parameters.AddWithValue("@PrecoEspaco", ultimaFesta.GetEspaco().GetPreco());

                    Conexao.Open();
                    comando.ExecuteNonQuery(); // Use ExecuteNonQuery para comandos INSERT, UPDATE, DELETE

                    Console.WriteLine("\nA cerimonia foi agendada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nNão foi possível salvar as informações. Preencha todos os campos!");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("\nErro no MySQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nErro: " + ex.Message);
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }

        //Ler todas a linhas que estão no banco de dados e adiciona na lista
        public static void LerDadosDoBanco(List<Festa> festas)
        {
            try
            {
                string sql = "SELECT * FROM cerimonias";

                using MySqlCommand comando = new MySqlCommand(sql, Conexao);

                Conexao.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DateTime data = reader.GetDateTime("data");
                    string identificador = reader.GetString("identificador");
                    int capacidade = reader.GetInt32("capacidade");
                    double preco = reader.GetDouble("precototal");
                    double precoEspaco = reader.GetDouble("precoespaco");
                    string tipoFestaString = reader.GetString("tipofesta");
                    string nivelFestaString = reader.GetString("nivelitens");

                    Espaco espaco = new Espaco(identificador, capacidade, precoEspaco);

                    TipoFesta tipoFesta = (TipoFesta)Enum.Parse(typeof(TipoFesta), tipoFestaString);

                    NivelFesta nivelFesta = (NivelFesta)Enum.Parse(typeof(NivelFesta), nivelFestaString);

                    Festa novaCerimonia = new Festa(preco, data, espaco, tipoFesta, nivelFesta);

                    festas.Add(novaCerimonia);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("\nErro: " + ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }
        //Le o arquivo que contem a conexão com o banco de dados
        public static string? LerConnectionStringDeArquivo(string txt)
        {
            try
            {
                string data_source = File.ReadAllText(txt);
                return data_source;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nErro ao ler a connectionString do arquivo: " + ex.Message);
                return null;
            }

        }
    }
}
