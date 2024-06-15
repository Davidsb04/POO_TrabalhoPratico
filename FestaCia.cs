using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Helpers;

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
        internal Espaco AgendarFesta(int numConvidados, Festa novaFesta, TipoFesta tipoFesta, NivelFesta nivelFesta, Bebidas bebidas)
        {           
            DateTime dataAtual = DateTime.Today;            

            DateTime dataCerimonia = CalcularProximaData(numConvidados, dataAtual);
            Espaco melhorEspaco = SelecionarMelhorEspaco(numConvidados, dataCerimonia);

            novaFesta = new Festa(0, 0, dataCerimonia, melhorEspaco, tipoFesta, nivelFesta, bebidas);
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
                        return espaco;

                    continue;
                }
                if (numConvidados <= 100 && numConvidados > 50)
                {
                    if(diferenca < 50)
                    {
                        bool espacoOcupado = FestaCia.Festas.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

                        if (espacoOcupado)
                            continue;

                        if (espaco.GetIdentificador() == "A" || espaco.GetIdentificador() == "B" || espaco.GetIdentificador() == "C" || espaco.GetIdentificador() == "D")
                            return espaco;
                    }                    
                }
                if(numConvidados > 100 && numConvidados <= 200)
                {
                    if (diferenca >= 0 && diferenca < 100)
                    {
                        bool espacoOcupado = FestaCia.Festas.Any(c => c.GetEspaco() == espaco && c.GetData().Date == data.Date);

                        if (espacoOcupado)
                            continue;

                        if(espaco.GetIdentificador() == "E" || espaco.GetIdentificador() == "F")
                            return espaco;
                    }                    
                }
                if(numConvidados > 200 &&  numConvidados <= 500)
                {
                    if (espaco.GetIdentificador() == "H")
                        return espaco;

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
                if(ultimaFesta.GetNivelFesta() == NivelFesta.Livre)
                {
                    return
                    $"\nTipo da festa: {ultimaFesta.GetTipoFesta()}\n" +
                    $"Data da Festa: {ultimaFesta.GetData().ToShortDateString()}\n" +
                    $"Espaço da Festa: {ultimaFesta.GetEspaco().GetIdentificador()}\n" +
                    $"Valor Espaço: R${ultimaFesta.GetEspaco().GetPreco()}\n" +
                    $"Valor Total da Festa: R${ultimaFesta.GetEspaco().GetPreco()}";
                }
               
                return
                    $"\nTipo da festa: {ultimaFesta.GetTipoFesta()}\n" +
                    $"Data da Festa: {ultimaFesta.GetData().ToShortDateString()}\n" +
                    $"Espaço da Festa: {ultimaFesta.GetEspaco().GetIdentificador()}\n\n" +
                    "Bebidas:\n" +
                    $"Quantidade de Água: {ultimaFesta.GetBebidas().QntAgua}\n" +
                    $"Quantidade de Suco: {ultimaFesta.GetBebidas().QntSuco}\n" +
                    $"Quantidade de Refrigerante: {ultimaFesta.GetBebidas().QntRefri}\n" +
                    $"Quantidade de Cerveja Comum: {ultimaFesta.GetBebidas().QntCervejaComum}\n" +
                    $"Quantidade de Cerveja Artesanal: {ultimaFesta.GetBebidas().QntCervejaArtesanal}\n" +
                    $"Quantidade de Espumante Nacional: {ultimaFesta.GetBebidas().QntEspumanteNacional}\n" +
                    $"Quantidade de Espumante Importado: {ultimaFesta.GetBebidas().QntEspumanteImportado}\n\n" +
                    $"Nível dos Produtos: {ultimaFesta.GetNivelFesta()}\n" +
                    $"Valor Espaço: R${ultimaFesta.GetEspaco().GetPreco()}\n" +
                    $"Valor Total dos Produtos: R${ultimaFesta.GetPrecoProdutos()}\n" +
                    $"Valor Total das Bebidas: R${ultimaFesta.GetPrecoBebidas()}\n" +
                    $"Valor Total da Festa: R${ultimaFesta.GetPrecoProdutos() + ultimaFesta.GetPrecoBebidas() + ultimaFesta.GetEspaco().GetPreco()}";

            }
            else
            {
                return "Nenhuma cerimônia agendada.";
            }
        }
    }
}
