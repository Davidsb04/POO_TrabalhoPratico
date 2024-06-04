using MySql.Data.MySqlClient;
using POO_TrabalhoPratico.Data;
using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.TiposCerimonia;

namespace POO_TrabalhoPratico
{
    public class Program
    {
        static NoivaCia noivaCia = new NoivaCia();
        static void Main(string[] args)
        {
            DBContext.ConfiguracaoConexao();            
            DBContext.LerDadosDoBanco(noivaCia.Cerimonias);
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
                    TipoCasamento opcaoCasamento = (TipoCasamento)int.Parse(Console.ReadLine());

                    while ((int)opcaoCasamento < 1 || (int)opcaoCasamento > 3)
                    {
                        Console.WriteLine("\nOpção inválida.");

                        Console.Write("\n[1]Premier \n[2]Luxo \n[3]Standard \n\nEscolha uma opção: ");
                        opcaoCasamento = (TipoCasamento)int.Parse(Console.ReadLine());
                    }

                    Cerimonia novaCerimonia;

                    if (opcaoCasamento == TipoCasamento.Premier) 
                    {
                        novaCerimonia = new TipoPremier(DateTime.Today, new Espaco("Z", 100, 1000));
                    }
                    else if (opcaoCasamento == TipoCasamento.Luxo) 
                    {
                        novaCerimonia = new TipoLuxo(DateTime.Today, new Espaco("Z", 100, 1000));
                    }
                    else 
                    {
                        novaCerimonia = new TipoStandard(DateTime.Today, new Espaco("Z", 100, 1000));
                    }                  

                    Espaco melhorEspaco = noivaCia.AgendarCerimonia(numConvidados, novaCerimonia);

                    novaCerimonia.AlterarPrecoCerimonia(noivaCia.Cerimonias, numConvidados, melhorEspaco);
                    novaCerimonia.CalcularValorBebida(noivaCia.Cerimonias);

                    if (melhorEspaco.GetIdentificador() != "Z")
                    {
                        Console.WriteLine(noivaCia.ToString());
                        DBContext.InserirDadosNoBanco(noivaCia.Cerimonias, numConvidados);
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
        
    }
}
