using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Cerimonia : Festa
    {
        private readonly DateTime Data;
        private readonly Espaco Espaco;

        public Cerimonia(DateTime data, Espaco espaco) : base(0)
        {
            Data = data;
            Espaco = espaco;
        }
        public DateTime GetData()
        { 
            return Data;
        }
        public Espaco GetEspaco()
        {
            return Espaco;
        }
        public void AlterarPrecoCerimonia(double valorTotal)
        {
            SetPreco(valorTotal);
        }
    }
}
