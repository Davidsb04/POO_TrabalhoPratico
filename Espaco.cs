using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Espaco
    {
        private string Identificador {  get; set; }
        private int Capacidade { get; set; }

        public Espaco(string identificador, int capacidade) 
        {
            Identificador = identificador;
            Capacidade = capacidade;
        }

    }
}
