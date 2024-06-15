using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Helpers
{
    internal class CalcularValorFesta : ICasamento, IFormatura, IFestaAniversario, IFestaEmpresa
    {
        public double CalcularValorAniversario(int numConvidados, Espaco espaco)
        {            
            double valorTipo = (50 * espaco.GetCapacidade()) + (50 * espaco.GetCapacidade()) + (10 * espaco.GetCapacidade()) + (20 * espaco.GetCapacidade());
            double valorComida = 40 * numConvidados;

            return valorTipo + valorComida;
        }

        public double CalcularValorCasamento(int numConvidados, NivelFesta tipoCasamento, Espaco espaco)
        {
            double valorTipo, valorComida;

            if (tipoCasamento == NivelFesta.Standard)
            {
                valorTipo = (50 * espaco.GetCapacidade()) + (50 * espaco.GetCapacidade()) + (10 * espaco.GetCapacidade()) + (20 * espaco.GetCapacidade());
                valorComida = 40 * numConvidados;

                return valorTipo + valorComida;
            }
        
            if(tipoCasamento == NivelFesta.Luxo)
            {
                valorTipo = (75 * espaco.GetCapacidade()) + (75 * espaco.GetCapacidade()) + (15 * espaco.GetCapacidade()) + (25 * espaco.GetCapacidade());
                valorComida = 48 * numConvidados;

                return valorTipo + valorComida;
            }

            valorTipo = (100 * espaco.GetCapacidade()) + (100 * espaco.GetCapacidade()) + (20 * espaco.GetCapacidade()) + (30 * espaco.GetCapacidade());
            valorComida = 60 * numConvidados;

            return valorTipo + valorComida;
        }

        public double CalcularValorEmpresa(int numConvidados, NivelFesta tipoCasamento, Espaco espaco)
        {
            double valorTipo, valorComida;

            if (tipoCasamento == NivelFesta.Standard)
            {
                valorTipo = 20 * espaco.GetCapacidade();
                valorComida = 40 * numConvidados;

                return valorTipo + valorComida;
            }

            if (tipoCasamento == NivelFesta.Luxo)
            {
                valorTipo = 25 * espaco.GetCapacidade();
                valorComida = 48 * numConvidados;

                return valorTipo + valorComida;
            }

            valorTipo = 30 * espaco.GetCapacidade();
            valorComida = 60 * numConvidados;

            return valorTipo + valorComida;
        }

        public double CalcularValorFormatura(int numConvidados, NivelFesta tipoCasamento, Espaco espaco)
        {
            double valorTipo, valorComida;

            if (tipoCasamento == NivelFesta.Standard)
            {
                valorTipo = (50 * espaco.GetCapacidade()) + (50 * espaco.GetCapacidade()) + (20 * espaco.GetCapacidade());
                valorComida = 40 * numConvidados;

                return valorTipo + valorComida;
            }

            if (tipoCasamento == NivelFesta.Luxo)
            {
                valorTipo = (75 * espaco.GetCapacidade()) + (75 * espaco.GetCapacidade()) + (25 * espaco.GetCapacidade());
                valorComida = 48 * numConvidados;

                return valorTipo + valorComida;
            }

            valorTipo = (100 * espaco.GetCapacidade()) + (100 * espaco.GetCapacidade()) + (30 * espaco.GetCapacidade());
            valorComida = 60 * numConvidados;

            return valorTipo + valorComida;
        }
    }
}
