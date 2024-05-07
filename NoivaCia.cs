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
    public class NoivaCia
    {
        private List<Espaco> Espacos;
        public List<Cerimonia> Cerimonias;

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
        public List<Cerimonia> GetCerimonias()
        {
            return Cerimonias;
        }
        //Agenda a cerimonia, salvando as informações da festa
        internal Espaco AgendarCerimonia(int numConvidados, Cerimonia novaCerimonia)
        {           
            DateTime dataAtual = DateTime.Today;            

            DateTime dataCerimonia = CalcularProximaData(numConvidados, dataAtual);
            Espaco melhorEspaco = SelecionarMelhorEspaco(numConvidados, dataCerimonia);


            novaCerimonia = new Cerimonia(dataCerimonia, melhorEspaco);
            Cerimonias.Add(novaCerimonia);

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

                    if (VerificarCerimonaNaData(espacoEspecifico, data))
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
        internal bool VerificarCerimonaNaData(Espaco espacoEspecifico, DateTime data)
        {
            return Cerimonias.Any(c => c.GetData().Date == data.Date && c.GetEspaco().GetIdentificador() == espacoEspecifico.GetIdentificador());
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
                        bool espacoOcupado = Cerimonias.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

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
                        bool espacoOcupado = Cerimonias.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

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
        //Retorna as informações para os noivos
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
