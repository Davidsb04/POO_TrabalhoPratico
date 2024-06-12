using POO_TrabalhoPratico.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Festa
    {
        protected double Preco;
        private DateTime Data;
        private Espaco Espaco;
        private TipoFesta TipoFesta;
        private NivelFesta NivelFesta;

        public Festa(double preco, DateTime data, Espaco espaco, TipoFesta tipoFesta, NivelFesta nivelFesta)
        {
            Preco = preco;
            Data = data;
            Espaco = espaco;
            TipoFesta = tipoFesta;
            NivelFesta = nivelFesta;
        }
        public double GetPreco()
        {
            return Preco;
        }
        public void SetPreco(double preco)
        {
            Preco += preco;
        }
        public DateTime GetData()
        {
            return Data;
        }
        public Espaco GetEspaco()
        {
            return Espaco;
        }
        public TipoFesta GetTipoFesta()
        {
            return TipoFesta;
        }
        public NivelFesta GetNivelFesta() 
        {
            return NivelFesta;
        }
    }
}
