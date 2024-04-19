namespace POO_TrabalhoPratico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NoivaCia noivaCia = new NoivaCia();
            int opcao = 0;

            do
            {
                Console.Write("\n[1] Agendar cerimonia \n[2] Sair \n\nEscolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {
                    Console.Write("\nDigite o número de convidados: ");
                    int numConvidados = int.Parse(Console.ReadLine());

                    while (numConvidados > 500)
                    {
                        Console.WriteLine("\nNúmero de convidados excede a capacidade máxima dos espaços disponíveis.");

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
