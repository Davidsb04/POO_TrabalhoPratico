using POO_TrabalhoPratico.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.TiposCerimonia
{
    internal class TipoPremier : Cerimonia
    {
        public TipoPremier(DateTime data, Espaco espaco) : base(data, espaco)
        {
        }

        public override void AlterarPrecoCerimonia(List<Cerimonia> cerimonias, int numConvidados, Espaco espaco)
        {
            Cerimonia? ultimaCerimonia = cerimonias.LastOrDefault();
            double valorTipo, valorComida;

            valorTipo = (100 * espaco.GetCapacidade()) + (100 * espaco.GetCapacidade()) + (20 * espaco.GetCapacidade()) + (30 * espaco.GetCapacidade());
            valorComida = 60 * numConvidados;

            double valorTotal = valorTipo + valorComida;

            ultimaCerimonia?.SetPreco(valorTotal);
        }

        internal override void CalcularValorBebida(List<Cerimonia> cerimonias)
        {
            double valorTotal = 0;
            Dictionary<TipoBebida, double> valoresUnitarios = new Dictionary<TipoBebida, double>
            {
                { TipoBebida.Agua, 5.00 },
                { TipoBebida.Suco, 7.00 },
                { TipoBebida.Refrigerante, 8.00 },
                { TipoBebida.CervejaComum, 20.00 },
                { TipoBebida.CervejaArtesanal, 30.00 },
                { TipoBebida.EspumanteNacional, 80.00 },
                { TipoBebida.EspumanteImportado, 140.00 }
            };

            foreach (TipoBebida tipoBebida in Enum.GetValues(typeof(TipoBebida)))
            {
                Console.Write($"\nInforme a quantidade de {tipoBebida}: ");
                int qntBebida = int.Parse(Console.ReadLine());
                valorTotal += qntBebida * valoresUnitarios[tipoBebida];
            }

            SetPreco(valorTotal);
        }
    }
}
