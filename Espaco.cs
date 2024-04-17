using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Espaco
    {
        public string Identificador {  get; private set; }
        public int Capacidade { get; private set; }
        public double Preco { get; private set; }

        public Espaco(string identificador, int capacidade, double preco) 
        {
            Identificador = identificador;
            Capacidade = capacidade;
            Preco = preco;
        }
    }
}
