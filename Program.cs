namespace POO_TrabalhoPratico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Espaco e1 = new Espaco("A", 100);
            Espaco e2 = new Espaco("B", 100);
            Espaco e3 = new Espaco("C", 100);
            Espaco e4 = new Espaco("D", 100);
            Espaco e5 = new Espaco("E", 200);
            Espaco e6 = new Espaco("F", 200);
            Espaco e7 = new Espaco("G", 50);
            Espaco e8 = new Espaco("H", 500);



            Console.Write("Digite a quantidade de convidados: ");
            int qntConvidados = int.Parse(Console.ReadLine());

            Console.Write("Digite a data da cerimonia: ");
            DateTime data = DateTime.Today;

            

            Cerimonia c1 = new Cerimonia(qntConvidados,  );
        }
    }
}
