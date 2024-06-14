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
    internal class Formatura : Festa
    {
        IFormatura formatura;

        public Formatura(double precoProdutos, double precoBebidas, DateTime data, Espaco espaco, Bebidas bebidas, TipoFesta tipoFesta, NivelFesta nivelFesta, int numConvidados) :
            base(precoProdutos, precoBebidas, data, espaco, tipoFesta, nivelFesta, bebidas)
        {
            formatura = new CalcularValorFesta();

            try
            {
                //Calculo do valor total da festa
                Festa? ultimaFesta = FestaCia.Festas.LastOrDefault();
                ultimaFesta?.SetPrecoProdutos(formatura.CalcularValorFormatura(numConvidados, nivelFesta, espaco));

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
