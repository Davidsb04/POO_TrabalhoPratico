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
                    int tipoCasamento = int.Parse(Console.ReadLine());

                    while (tipoCasamento < 1 || tipoCasamento > 3)
                    {
                        Console.WriteLine("\nOpção inválida.");

                        Console.Write("\n[1]Premier \n[2]Luxo \n[3]Standard \n\nEscolha uma opção: ");
                        tipoCasamento = int.Parse(Console.ReadLine());
                    }

                    TipoCasamento tipo;

                    if (tipoCasamento == 1) { tipo = TipoCasamento.Premier; }
                    else if (tipoCasamento == 2) { tipo = TipoCasamento.Luxo; }
                    else { tipo = TipoCasamento.Standard; }
                    
                    (DateTime dataCerimonia, Espaco melhorEspaco) = noivaCia.AgendarCerimonia(numConvidados, tipo);


                    if (melhorEspaco.Identificador != "Z")
                    {
                        Console.WriteLine($"\nA data da cerimônia será: {dataCerimonia.ToShortDateString()}, no espaço: {melhorEspaco.Identificador} no valor de R${melhorEspaco.Preco}");
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
