using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico
{
    public class Festa
    {
        protected double PrecoProdutos;
        protected double PrecoBebidas;
        private DateTime Data;
        private Espaco Espaco;
        private TipoFesta TipoFesta;
        private NivelFesta NivelFesta;
        private Bebidas Bebidas;

        public Festa(double precoProdutos, double precoBebidas, DateTime data, Espaco espaco, TipoFesta tipoFesta, NivelFesta nivelFesta, Bebidas bebidas)
        {
            PrecoProdutos = precoProdutos;
            PrecoBebidas = precoBebidas;
            Data = data;
            Espaco = espaco;
            TipoFesta = tipoFesta;
            NivelFesta = nivelFesta;
            Bebidas = bebidas;
        }
        public double GetPrecoProdutos()
        {
            return PrecoProdutos;
        }
        public void SetPrecoProdutos(double preco)
        {
            PrecoProdutos += preco;
        }
        public double GetPrecoBebidas()
        {
            return PrecoBebidas;
        }
        public void SetPrecoBebdias(double preco)
        {
            PrecoBebidas += preco;
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
        public Bebidas GetBebidas()
        {
            return Bebidas;
        }
    }
}
