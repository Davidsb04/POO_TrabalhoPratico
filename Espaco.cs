using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Espaco
    {
        public double Preco;
        private readonly string Identificador;
        private readonly int Capacidade;

        public Espaco(string identificador, int capacidade, double preco)
        {
            Preco = preco;
            Identificador = identificador;
            Capacidade = capacidade;
        }  
        public string GetIdentificador()
        {
            return Identificador;
        }
        public int GetCapacidade()
        {
            return Capacidade;
        }
        public double GetPreco()
        {
            return Preco;
        }
    }
}
