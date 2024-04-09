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
                new Espaco("A", 100),
                new Espaco("B", 100),
                new Espaco("C", 100),
                new Espaco("D", 100),
                new Espaco("E", 200),
                new Espaco("F", 200),
                new Espaco("G", 50),
                new Espaco("H", 500),
            };
        }

        public (DateTime Data, Espaco Espaco) AgendarCerimonia(int numConvidados)
        {
            DateTime dataCerimonia = CalcularProximaData(numConvidados);
            Espaco melhorEspaco = SelecionarMelhorEspaco(numConvidados, dataCerimonia);

            Cerimonia novaCerimonia = new Cerimonia(dataCerimonia, melhorEspaco);
            Cerimonias.Add(novaCerimonia);

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

            return new Espaco("Z", -1);
        }
    }
}
