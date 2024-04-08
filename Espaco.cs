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

        public Espaco(string identificador, int capacidade) 
        {
            Identificador = identificador;
            Capacidade = capacidade;
        }

    }
}
