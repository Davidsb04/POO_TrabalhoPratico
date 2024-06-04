using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        public static void InserirDadosNoBanco(List<Cerimonia> cerimonias, int numConvidados)
        {
            try
            {
                Cerimonia? ultimaCerimonia = cerimonias.LastOrDefault();

                if (ultimaCerimonia != null)
                {
                    string sql = "INSERT INTO cerimonias (data, identificador, capacidade, convidados, preco) " +
                    "VALUES (@Data, @Identificador, @Capacidade, @Convidados, @Preco)";

                    using MySqlCommand comando = new MySqlCommand(sql, Conexao);
                    comando.Parameters.AddWithValue("@Data", ultimaCerimonia.GetData());
                    comando.Parameters.AddWithValue("@Identificador", ultimaCerimonia.GetEspaco().GetIdentificador());
                    comando.Parameters.AddWithValue("@Capacidade", ultimaCerimonia.GetEspaco().GetCapacidade());
                    comando.Parameters.AddWithValue("@Convidados", numConvidados);
                    comando.Parameters.AddWithValue("@Preco", ultimaCerimonia.GetPreco() + ultimaCerimonia.GetEspaco().GetPreco());

                    Conexao.Open();

                    comando.ExecuteReader();

                    Console.WriteLine("\nA cerimonia foi agendada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nNão foi possível salvar as informações. Preencha todos os campos!");
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
        //Ler todas a linhas que estão no banco de dados e adiciona na lista
        public static void LerDadosDoBanco(List<Cerimonia> cerimonias)
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
                    double preco = reader.GetDouble("preco");

                    Espaco espaco = new Espaco(identificador, capacidade, preco);

                    Cerimonia novaCerimonia = new Cerimonia(data, espaco);

                    cerimonias.Add(novaCerimonia);
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
