using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Festa
    {
        private double Preco {  get; set; }

        public Festa(double preco)
        {
            Preco = preco;
        }
        public double GetPreco()
        {
            return Preco;
        }
        public void SetPreco(double preco)
        {
            Preco += preco;
        }
    }
}
