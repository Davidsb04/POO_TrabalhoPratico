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

        public static List<Bebidas> Qntbebidas = new List<Bebidas>();

        public static void AdicionarQuantidadeDeBebidas(Bebidas bebidas)
        {
            Qntbebidas.Add(bebidas);
        }
    }
}
