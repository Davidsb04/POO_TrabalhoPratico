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

        public (DateTime Data, Espaco Espaco) AgendarCerimonia(int numConvidados, TipoCasamento tipo)
        {
            DateTime dataCerimonia = CalcularProximaData(numConvidados);
            Espaco melhorEspaco = SelecionarMelhorEspaco(numConvidados, dataCerimonia);


            Cerimonia novaCerimonia = new Cerimonia(dataCerimonia, melhorEspaco);
            Cerimonias.Add(novaCerimonia);

            CalcularValorCerimonia(tipo, melhorEspaco, numConvidados, novaCerimonia);

            return (dataCerimonia, melhorEspaco);
        }

        internal DateTime CalcularProximaData(int numConvidados)
        {
            DateTime data = DateTime.Today.AddDays(30);

            while (true)
            {
                if (data.DayOfWeek == DayOfWeek.Friday || data.DayOfWeek == DayOfWeek.Saturday)
                {
                    Espaco espacoEspecifico = SelecionarMelhorEspaco(numConvidados, data);

                    if (espacoEspecifico.Identificador == "Z")
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
            return !Cerimonias.Any(c => c.Data.Date == data.Date && c.Espaco == espacoEspecifico);
        }

        internal Espaco SelecionarMelhorEspaco(int numConvidados, DateTime data)
        {
            var espacosOrdenados = Espacos.OrderBy(espaco => espaco.Capacidade);

            foreach (Espaco espaco in Espacos)
            {
                int diferenca = espaco.Capacidade - numConvidados;

                if (diferenca >= 0 && diferenca < 50)
                {
                    bool espacoOcupado = Cerimonias.Any(c => c.Espaco == espaco && c.Data.Date == data.Date);

                    if (espacoOcupado)
                    {
                        continue;
                    }

                    return espaco;
                }
            }

            return new Espaco("Z", -1, 0);
        }
        internal void CalcularValorCerimonia(TipoCasamento tipo, Espaco espaco, int numConvidados, Cerimonia cerimonia)
        {
            double valorTipo, valorComida, valorBebida;

            if (tipo == TipoCasamento.Premier)
            {
                valorTipo = (100 * espaco.Capacidade) + (100 * espaco.Capacidade) + (20 * espaco.Capacidade) + (30 * espaco.Capacidade);
                valorComida = 60 * numConvidados;
            }
            else if(tipo == TipoCasamento.Luxo)
            {
                valorTipo = (75 * espaco.Capacidade) + (75 * espaco.Capacidade) + (15 * espaco.Capacidade) + (25 * espaco.Capacidade);
                valorComida = 48 * numConvidados;
            }
            else
            {
                valorTipo = (50 * espaco.Capacidade) + (50 * espaco.Capacidade) + (10 * espaco.Capacidade) + (20 * espaco.Capacidade);
                valorComida = 40 * numConvidados;
            }

            double valorTotal = valorTipo + valorComida;

            cerimonia.AlterarPrecoTotal(valorTotal);            
        }
    }
}
