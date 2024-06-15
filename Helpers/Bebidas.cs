using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Helpers
{
    public class Bebidas
    {
        public uint QntAgua { get; set; } = 0;
        public uint QntSuco { get; set; } = 0;
        public uint QntRefri { get; set; } = 0;
        public uint QntCervejaComum { get; set; } = 0;
        public uint QntCervejaArtesanal { get; set; } = 0;
        public uint QntEspumanteNacional { get; set; } = 0;
        public uint QntEspumanteImportado { get; set; } = 0;
        public double PrecoAgua { get; set; } = 0;
        public double PrecoSuco { get; set; } = 0;
        public double PrecoRefri { get; set; } = 0;
        public double PrecoCervejaComum { get; set; } = 0;
        public double PrecoCervejaArtesanal { get; set; } = 0;
        public double PrecoEspumanteNacional { get; set; } = 0;
        public double PrecoEspumanteImportado { get; set; } = 0;

        public static List<Bebidas> Qntbebidas = new List<Bebidas>();

        public static void AdicionarQuantidadeDeBebidas(Bebidas bebidas)
        {
            Qntbebidas.Add(bebidas);
        }

        public double CalcularValorBebida(Bebidas bebidas)
        {
            PrecoAgua = bebidas.QntAgua * 5;
            PrecoSuco = bebidas.QntSuco * 7;
            PrecoRefri = bebidas.QntRefri * 8;
            PrecoCervejaComum = bebidas.QntCervejaComum * 20;
            PrecoCervejaArtesanal = bebidas.QntCervejaArtesanal * 30;
            PrecoEspumanteNacional = bebidas.QntEspumanteNacional * 80;
            PrecoEspumanteImportado = bebidas.QntEspumanteImportado * 140;

            double valorTotal = PrecoAgua + PrecoSuco + PrecoRefri + PrecoCervejaComum + PrecoCervejaArtesanal + PrecoCervejaComum + PrecoEspumanteNacional + PrecoEspumanteImportado;

            return valorTotal;
        }
    }
}
