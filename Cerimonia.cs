using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Cerimonia
    {


        public DateTime Data {  get; private set; }
        public Espaco Espaco {  get; private set; }
        public double Preco {  get; private set; }

        public Cerimonia(DateTime data, Espaco espaco)
        {
            Data = data;
            Espaco = espaco;
        }

        public void AlterarPrecoTotal(double valorTotal)
        {
            Preco += valorTotal;
        }
    }
}
