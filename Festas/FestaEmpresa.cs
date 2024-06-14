using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Helpers;
using POO_TrabalhoPratico.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Festas
{
    internal class FestaEmpresa : Festa
    {
        IFestaEmpresa festaEmpresa;

        public FestaEmpresa(double precoProdutos, double precoBebidas, DateTime data, Espaco espaco, Bebidas bebidas, TipoFesta tipoFesta, NivelFesta nivelFesta, int numConvidados) :
            base(precoProdutos, precoBebidas, data, espaco, tipoFesta, nivelFesta, bebidas)
        {
            festaEmpresa = new CalcularValorFesta();

            try
            {
                //Calculo do valor total da festa
                Festa? ultimaFesta = FestaCia.Festas.LastOrDefault();
                ultimaFesta?.SetPrecoProdutos(festaEmpresa.CalcularValorEmpresa(numConvidados, nivelFesta, espaco));

                //Calculo do valor total das bebidas
                ultimaFesta?.SetPrecoBebdias(bebidas.CalcularValorBebida(bebidas));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível alterar o valor da festa. Detalhes: " + ex.Message);
            }

            
        }
    }
}
