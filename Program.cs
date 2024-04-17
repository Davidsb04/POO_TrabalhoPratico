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



                    (DateTime dataCerimonia, Espaco melhorEspaco) = noivaCia.AgendarCerimonia(numConvidados);

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
