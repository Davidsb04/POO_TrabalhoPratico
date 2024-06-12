using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using POO_TrabalhoPratico.Enums;

[assembly: InternalsVisibleTo("TrabalhoPratico.Tests")]

namespace POO_TrabalhoPratico
{
    public class FestaCia
    {
        private List<Espaco> Espacos;
        public static List<Festa> Festas;

        public FestaCia()
        {
            Festas = new List<Festa>();
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
        //Agenda a cerimonia, salvando as informações da festa
        internal Espaco AgendarFesta(int numConvidados, Festa novaFesta, TipoFesta tipoFesta, double valorBebidas, NivelFesta nivelFesta)
        {           
            DateTime dataAtual = DateTime.Today;            

            DateTime dataCerimonia = CalcularProximaData(numConvidados, dataAtual);
            Espaco melhorEspaco = SelecionarMelhorEspaco(numConvidados, dataCerimonia);

            novaFesta = new Festa(valorBebidas, dataCerimonia, melhorEspaco, tipoFesta, nivelFesta);
            Festas.Add(novaFesta);

            return melhorEspaco;
        }
        //Retorna uma data válida para a cerimonia
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

                    if (VerificarFestaNaData(espacoEspecifico, data))
                    {
                        data = data.AddDays(1);
                        continue;
                    }

                    return data;
                }
                data = data.AddDays(1);
            }
        }

        //Verifica sem tem alguma cerimonia no dia e espaço específico
        internal bool VerificarFestaNaData(Espaco espacoEspecifico, DateTime data)
        {
            return Festas.Any(c => c.GetData().Date == data.Date && c.GetEspaco().GetIdentificador() == espacoEspecifico.GetIdentificador());
        }
        //Seleciona o melhor espaço possível com base no número de convidados
        internal Espaco SelecionarMelhorEspaco(int numConvidados, DateTime data)
        {
            foreach (Espaco espaco in Espacos)
            {
                int diferenca = espaco.GetCapacidade() - numConvidados;

                if (numConvidados <= 50)
                {
                    if (espaco.GetIdentificador() == "G")
                    {
                        return espaco;
                    }
                    continue;
                }
                else if (numConvidados <= 100)
                {
                    if(diferenca < 50)
                    {
                        bool espacoOcupado = Festas.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

                        if (espacoOcupado)
                        {
                            continue;
                        }

                        return espaco;
                    }                    
                }
                else if(numConvidados <= 200)
                {
                    if (diferenca >= 0 && diferenca < 100)
                    {
                        bool espacoOcupado = Festas.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

                        if (espacoOcupado)
                        {
                            continue;
                        }

                        return espaco;
                    }                    
                }
                else
                {
                    if (espaco.GetIdentificador() == "H")
                    {
                        return espaco;
                    }
                    continue;
                }
                
            }

            return new Espaco("Z", -1, 0);
        }

        //Retorna todas as festas agendadas
        public string CalendarioDeFestas()
        {
            if (Festas.Count == 0)
                return "\nNão foi possível encontrar nenhuma festa.";

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var festa in Festas)
            {
                stringBuilder.AppendLine($"\nTipo da festa: {festa.GetTipoFesta()}")
                       .AppendLine($"Data da Festa: {festa.GetData().ToShortDateString()}");
            }

            return stringBuilder.ToString();

        }

        //Retorna as informações para os noivos
        public override string ToString()
        {
            Festa? ultimaFesta = Festas.LastOrDefault();

            if (ultimaFesta != null)
            {
                return
                    $"\nTipo da festa: {ultimaFesta.GetTipoFesta()}\n" +
                    $"Data da Festa: {ultimaFesta.GetData().ToShortDateString()}\n" +
                    $"Espaço da Festa: {ultimaFesta.GetEspaco().GetIdentificador()}\n" +
                    $"Nível da Festa: {ultimaFesta.GetNivelFesta()}\n" +
                    $"Valor Espaço: R${ultimaFesta.GetEspaco().GetPreco()}\n" +
                    $"Valor Festa: R${ultimaFesta.GetPreco()}\n" +
                    $"Valor Total: R${ultimaFesta.GetPreco() + ultimaFesta.GetEspaco().GetPreco()}";

            }
            else
            {
                return "Nenhuma cerimônia agendada.";
            }
        }
    }
}
