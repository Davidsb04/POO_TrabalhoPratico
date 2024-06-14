using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using POO_TrabalhoPratico.Data;
using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Festas;
using POO_TrabalhoPratico.Helpers;

namespace POO_TrabalhoPratico
{
    public class Program
    {
        static FestaCia festaCia = new FestaCia();
        static CalcularValorFesta calcularValorFesta = new CalcularValorFesta();
        static List<Festa> ListaFestas = FestaCia.Festas;
        static void Main(string[] args)
        {
            DBContext.ConfiguracaoConexao();            
            DBContext.LerDadosDoBanco(ListaFestas);

            foreach(var festa in ListaFestas)
            {
                Console.WriteLine($"Data: {festa.GetData()} \nLocal: {festa.GetEspaco().GetIdentificador()}");
            }

            MenuInicial();

        }

        public static void MenuInicial()
        {
            int opcao = 0;            
                
            do
            {
                try
                {
                    Console.Write("\n[1] Agendar cerimonia \n[2] Calendário de Festas \n[3] Sair\n\nEscolha uma opção: ");
                    opcao = int.Parse(Console.ReadLine());

                    while (opcao < 1 || opcao > 3)
                    {
                        Console.WriteLine("\nOpção inválida.");

                        Console.Write("\n[1] Agendar cerimonia \n[2] Calendário de festas \n[3] Sair\n\nEscolha uma opção: ");
                        opcao = int.Parse(Console.ReadLine());
                    }

                    if (opcao == 1)
                    {
                        Console.Write("\nDigite o número de convidados: ");
                        int numConvidados = int.Parse(Console.ReadLine());

                        while (numConvidados > 500 || numConvidados < 1)
                        {
                            Console.WriteLine("\nNão temos espaços que com capacidade para esse número de convidados.");

                            Console.Write("\nDigite o número de convidados: ");
                            numConvidados = int.Parse(Console.ReadLine());
                        }

                        Console.Write("\n[1]Casamento \n[2]Formatura \n[3]Festa de Empresa \n[4]Festa de Aniversário \n[5]Livre \n\nEscolha uma opção: ");
                        TipoFesta tipoFesta = (TipoFesta)int.Parse(Console.ReadLine());

                        while ((int)tipoFesta < 1 || (int)tipoFesta > 5)
                        {
                            Console.WriteLine("\nOpção inválida.");

                            Console.Write("\n[1]Casamento \n[2]Formatura \n[3]Festa de Empresa \n[4]Festa de Aniversário \n[5]Livre \n\nEscolha uma opção: ");
                            tipoFesta = (TipoFesta)int.Parse(Console.ReadLine());
                        }

                        NivelFesta nivelFesta = NivelFesta.Standard;

                        if (tipoFesta != TipoFesta.Livre && tipoFesta != TipoFesta.FestaAniversario)
                        {
                            Console.Write("\n[1]Premier \n[2]Luxo \n[3]Standard \n\nEscolha uma opção: ");
                            nivelFesta = (NivelFesta)int.Parse(Console.ReadLine());

                            while ((int)nivelFesta < 1 || (int)nivelFesta > 3)
                            {
                                Console.WriteLine("\nOpção inválida.");

                                Console.Write("\n[1]Premier \n[2]Luxo \n[3]Standard \n\nEscolha uma opção: ");
                                nivelFesta = (NivelFesta)int.Parse(Console.ReadLine());
                            }
                        }

                        Bebidas bebidas = null;

                        if (tipoFesta != TipoFesta.Livre)
                        {
                            bool bebidasValidas = false;
                            while (!bebidasValidas)
                            {
                                try
                                {
                                    bebidas = SelecionarBebidas(nivelFesta);
                                    bebidasValidas = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("\nEntrada inválida. Por favor, digite um número inteiro válido.");
                                }                                
                            }
                        }

                        Festa novaFesta;

                        novaFesta = new Festa(0, 0, DateTime.Today, new Espaco("Z", 100, 10000), tipoFesta, nivelFesta, bebidas);

                        Espaco melhorEspaco = festaCia.AgendarFesta(numConvidados, novaFesta, tipoFesta, nivelFesta, bebidas);

                        //Para casamentos
                        if (tipoFesta == TipoFesta.Casamento)
                            novaFesta = new Casamento(0, 0, DateTime.Today, melhorEspaco, bebidas, tipoFesta, nivelFesta, numConvidados);
                        //Para fomaturas
                        else if (tipoFesta == TipoFesta.Formatura)
                            novaFesta = new Formatura(0, 0, DateTime.Today, melhorEspaco, bebidas, tipoFesta, nivelFesta, numConvidados);
                        //Para festas de empresa
                        else if (tipoFesta == TipoFesta.FestaEmpresa)
                            novaFesta = new FestaEmpresa(0, 0, DateTime.Today, melhorEspaco, bebidas, tipoFesta, nivelFesta, numConvidados);
                        //Para festas de aniversário
                        else if (tipoFesta == TipoFesta.FestaAniversario)
                            novaFesta = new FestaAniversario(0, 0, DateTime.Today, melhorEspaco, bebidas, tipoFesta, nivelFesta, numConvidados);
                        //Para festas livres
                        else
                            novaFesta = new Festa(0, 0, DateTime.Today, melhorEspaco, tipoFesta, nivelFesta, bebidas);


                        if (melhorEspaco.GetIdentificador() != "Z")
                        {
                            Console.WriteLine(festaCia.ToString());
                            DBContext.InserirDadosNoBanco(numConvidados, ListaFestas);
                        }
                        else
                        {
                            throw new Exception("\nNão temos espaços que comportam essa quantidade de convidados.");
                        }
                    }
                    else if (opcao == 2)
                    {
                        Console.WriteLine(festaCia.CalendarioDeFestas());
                    }
                    else if (opcao != 3)
                    {
                        Console.WriteLine("\nDigite uma opção válida.");
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("\nEntrada inválida. Por favor, digite um número inteiro válido.");
                }
                
            } while (opcao != 3);
        }

        public static Bebidas SelecionarBebidas(NivelFesta nivelFesta)
        {
            var bebidas = new Bebidas();
            bebidas.QntCervejaArtesanal = 0;
            bebidas.QntEspumanteImportado = 0;

            Console.Write("\nInforme a quantidade de água (1,5L): ");
            bebidas.QntAgua = uint.Parse(Console.ReadLine());

            Console.Write("\nInforme a quantidade de suco (1L): ");
            bebidas.QntSuco = uint.Parse(Console.ReadLine());

            Console.Write("\nInforme a quantidade de refrigerante (2L): ");
            bebidas.QntRefri = uint.Parse(Console.ReadLine());

            Console.Write("\nInforme a quantidade de cerveja comum (600ml): ");
            bebidas.QntCervejaComum = uint.Parse(Console.ReadLine());

            Console.Write("\nInforme a quantidade de espumante nacional (750ml): ");
            bebidas.QntEspumanteNacional = uint.Parse(Console.ReadLine());

            if (nivelFesta == NivelFesta.Luxo || nivelFesta == NivelFesta.Premier)
            {
                Console.Write("\nInforme a quantidade de cerveja artesanal (600ml): ");
                bebidas.QntCervejaArtesanal = uint.Parse(Console.ReadLine());

                Console.Write("\nInforme a quantidade de espumante importado (750ml): ");
                bebidas.QntEspumanteImportado = uint.Parse(Console.ReadLine());
            }

            Bebidas.AdicionarQuantidadeDeBebidas(bebidas);

            return bebidas;
        }
        
    }
}
