using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Cerimonia
    {
        private DateTime Data {  get; set; }
        private int NumConvidados { get; set; }
        private Espaco Espaco {  get; set; }

        public Cerimonia(DateTime data, int numConvidados, Espaco espaco)
        {
            Data = data;
            Espaco = espaco;
            NumConvidados = numConvidados;
        }
    }
}
