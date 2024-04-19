using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TrabalhoPratico.Tests")]

namespace POO_TrabalhoPratico
{
    public class NoivaCia
    {
        private List<Espaco> Espacos;
        internal List<Cerimonia> Cerimonias;

        public NoivaCia()
        {
            Cerimonias = new List<Cerimonia>();
            Espacos = new List<Espaco>
            {
                new Espaco("A", 100, 10000),
                new Espaco("B", 100, 10000),
                new Espaco("C", 100, 10000),
                new Espaco("D", 100, 10000),
                new Espaco("E", 200, 17000),
                new Espaco("F", 200, 17000),
                new Espaco("G", 50, 8000),
                new Espaco("H", 500, 35000),
            };
        }

        public (DateTime Data, Espaco Espaco, Cerimonia novaCerimonia) AgendarCerimonia(int numConvidados, TipoCasamento tipo)
        {
            DateTime dataAtual = DateTime.Today;

            DateTime dataCerimonia = CalcularProximaData(numConvidados, dataAtual);
            Espaco melhorEspaco = SelecionarMelhorEspaco(numConvidados, dataCerimonia);


            Cerimonia novaCerimonia = new Cerimonia(dataCerimonia, melhorEspaco);
            Cerimonias.Add(novaCerimonia);

            CalcularValorCerimonia(tipo, numConvidados, novaCerimonia);

            return (dataCerimonia, melhorEspaco, novaCerimonia);
        }

        internal DateTime CalcularProximaData(int numConvidados, DateTime dataAtual)
        {
            DateTime data = dataAtual.AddDays(30);

            while (true)
            {
                if (data.DayOfWeek == DayOfWeek.Friday || data.DayOfWeek == DayOfWeek.Saturday)
                {
                    Espaco espacoEspecifico = SelecionarMelhorEspaco(numConvidados, data);

                    if (espacoEspecifico.GetIdentificador() == "Z")
                    {
                        data = data.AddDays(1);
                        continue;
                    }

                    if (VerificarCerimonaNaData(espacoEspecifico, data))
                    {
                        return data;
                    }
                }
                data = data.AddDays(1);
            }
        }

        internal bool VerificarCerimonaNaData(Espaco espacoEspecifico, DateTime data)
        {
            return !Cerimonias.Any(c => c.GetData().Date == data.Date && c.GetEspaco() == espacoEspecifico);
        }

        internal Espaco SelecionarMelhorEspaco(int numConvidados, DateTime data)
        {
            var espacosOrdenados = Espacos.OrderBy(espaco => espaco.GetCapacidade());

            foreach (Espaco espaco in Espacos)
            {
                int diferenca = espaco.GetCapacidade() - numConvidados;

                if (diferenca >= 0 && diferenca < 50)
                {
                    bool espacoOcupado = Cerimonias.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

                    if (espacoOcupado)
                    {
                        continue;
                    }

                    return espaco;
                }
            }

            return new Espaco("Z", -1, 0);
        }
        internal void CalcularValorCerimonia(TipoCasamento tipoCasamento, int numConvidados, Cerimonia cerimonia)
        {           

            double valorTipo, valorComida;

            if (tipoCasamento == TipoCasamento.Premier)
            {
                valorTipo = (100 * cerimonia.GetEspaco().GetCapacidade()) + (100 * cerimonia.GetEspaco().GetCapacidade()) + (20 * cerimonia.GetEspaco().GetCapacidade()) + (30 * cerimonia.GetEspaco().GetCapacidade());
                valorComida = 60 * numConvidados;
            }
            else if(tipoCasamento == TipoCasamento.Luxo)
            {
                valorTipo = (75 *       cerimonia.GetEspaco().GetCapacidade()) + (75 * cerimonia.GetEspaco().GetCapacidade()) + (15 * cerimonia.GetEspaco().GetCapacidade()) + (25 * cerimonia.GetEspaco().GetCapacidade());
                valorComida = 48 * numConvidados;
            }
            else
            {
                valorTipo = (50 * cerimonia.GetEspaco().GetCapacidade()) + (50 * cerimonia.GetEspaco().GetCapacidade()) + (10 * cerimonia.GetEspaco().GetCapacidade()) + (20 * cerimonia.GetEspaco().GetCapacidade());
                valorComida = 40 * numConvidados;
            }

            double valorTotal = valorTipo + valorComida;

            cerimonia.AlterarPrecoCerimonia(valorTotal);            
        }

        internal void CalcularValorBebida(TipoBebida tipoBebida, int qntBebida)
        {
            Cerimonia? ultimaCerimonia = Cerimonias.LastOrDefault();
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

            double valorTotal = qntBebida * valoresUnitarios[tipoBebida];

            ultimaCerimonia?.AlterarPrecoCerimonia(valorTotal);
        }

        public override string ToString()
        {
            Cerimonia? ultimaCerimonia = Cerimonias.LastOrDefault();

            if (ultimaCerimonia != null)
            {
                return
                    $"\nData da Cerimonia: {ultimaCerimonia.GetData().ToShortDateString()}\n" +
                    $"Espaço da Cerimonia: {ultimaCerimonia.GetEspaco().GetIdentificador()}\n" +
                    $"Valor Espaço: R${ultimaCerimonia.GetEspaco().GetPreco()}\n" +
                    $"Valor Festa: R${ultimaCerimonia.GetPreco()}\n" +
                    $"Valor Total: R${ultimaCerimonia.GetPreco() + ultimaCerimonia.GetEspaco().GetPreco()}";

            }
            else
            {
                return "Nenhuma cerimônia agendada.";
            }
        }
    }
}
