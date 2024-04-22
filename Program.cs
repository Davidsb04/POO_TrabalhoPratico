using MySql.Data.MySqlClient;

namespace POO_TrabalhoPratico
{
    public class Program
    {
        static MySqlConnection Conexao;
        static NoivaCia noivaCia = new NoivaCia();
        static List<Cerimonia> cerimoniasBanco = new List<Cerimonia>();
        static void Main(string[] args)
        {
            ConfiguracaoConexao();            
            LerDadosDoBanco(noivaCia.Cerimonias);
            int opcao = 0;

            do
            {
                Console.Write("\n[1] Agendar cerimonia \n[2] Sair \n\nEscolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {
                    Console.Write("\nDigite o número de convidados: ");
                    int numConvidados = int.Parse(Console.ReadLine());

                    while (numConvidados > 500 || numConvidados < 0)
                    {
                        Console.WriteLine("\nNão temos espaços que com capacidade para esse número de convidados.");

                        Console.Write("\nDigite o número de convidados: ");
                        numConvidados = int.Parse(Console.ReadLine());
                    }

                    Console.Write("\n[1]Premier \n[2]Luxo \n[3]Standard \n\nEscolha uma opção: ");
                    int opcaoCasamento = int.Parse(Console.ReadLine());

                    while (opcaoCasamento < 1 || opcaoCasamento > 3)
                    {
                        Console.WriteLine("\nOpção inválida.");

                        Console.Write("\n[1]Premier \n[2]Luxo \n[3]Standard \n\nEscolha uma opção: ");
                        opcaoCasamento = int.Parse(Console.ReadLine());
                    }

                    TipoCasamento tipoCasamento;

                    if (opcaoCasamento == 1) { tipoCasamento = TipoCasamento.Premier; }
                    else if (opcaoCasamento == 2) { tipoCasamento = TipoCasamento.Luxo; }
                    else { tipoCasamento = TipoCasamento.Standard; }


                    (DateTime dataCerimonia, Espaco melhorEspaco, Cerimonia novaCerimonia) = noivaCia.AgendarCerimonia(numConvidados, tipoCasamento);

                    foreach (TipoBebida tipoBebida in Enum.GetValues(typeof(TipoBebida)))
                    {
                        if (tipoBebida != TipoBebida.EspumanteImportado && tipoBebida != TipoBebida.CervejaArtesanal)
                        {
                            Console.Write($"\nInforme a quantidade de {tipoBebida}: ");
                            int qntBebida = int.Parse(Console.ReadLine());
                            noivaCia.CalcularValorBebida(tipoBebida, qntBebida);
                        }
                        else
                        {
                            if (tipoCasamento == TipoCasamento.Luxo || tipoCasamento == TipoCasamento.Premier)
                            {
                                Console.Write($"\nInforme a quantidade de {tipoBebida}: ");
                                int qntBebida = int.Parse(Console.ReadLine());
                                noivaCia.CalcularValorBebida(tipoBebida, qntBebida);
                            }
                            else
                            {
                                Console.WriteLine($"\n{tipoBebida} é somente para casamentos Luxo e Premier.");
                            }
                        }
                    }

                    if (melhorEspaco.GetIdentificador() != "Z")
                    {
                        Console.WriteLine(noivaCia.ToString());
                        InserirDadosNoBanco(noivaCia.Cerimonias, numConvidados);
                    }
                    else
                    {
                        Console.WriteLine("\nNão temos espaços que comportam essa quantidade de convidados.");
                    }
                }
                else if (opcao != 2)
                {
                    Console.WriteLine("\nDigite uma opção válida.");
                }
            } while (opcao != 2);
        }
        //Estabelece a conexão com o banco de dados
        static void ConfiguracaoConexao()
        {
            string? data_source = LerConnectionStringDeArquivo("AppSettings.txt");

            if (data_source != null)
            {
                Conexao = new MySqlConnection(data_source);
            }
        }
        //Insere a informações da cerimonia no banco de dados
        static void InserirDadosNoBanco(List<Cerimonia> cerimonias, int numConvidados)
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

            }catch (Exception ex)
            {
                Console.WriteLine("\nErro: " + ex.Message);
            }
            finally
            {
                Conexao.Close();
            }          
        }
        //Ler todas a linhas que estão no banco de dados e adiciona na lista
        static void LerDadosDoBanco(List<Cerimonia> cerimonias)
        {
            try
            {
                string sql = "SELECT * FROM cerimonias";

                using MySqlCommand comando = new MySqlCommand(sql, Conexao);

                Conexao.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                while(reader.Read())
                {
                    DateTime data = reader.GetDateTime("data");
                    string identificador = reader.GetString("identificador");
                    int capacidade = reader.GetInt32("capacidade");
                    double preco = reader.GetDouble("preco");

                    Espaco espaco = new Espaco(identificador, capacidade, preco);

                    Cerimonia novaCerimonia = new Cerimonia(data, espaco);

                    cerimonias.Add(novaCerimonia);
                }

            }catch (Exception ex)
            {
                Console.WriteLine("\nErro: " + ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }
        //Le o arquivo que contem a conexão com o banco de dados
        static string? LerConnectionStringDeArquivo(string txt)
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
