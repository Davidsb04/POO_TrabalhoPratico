using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Helpers
{
    public class Bebidas
    {
        public int QntAgua { get; set; } = 0;
        public int QntSuco { get; set; } = 0;
        public int QntRefri { get; set; } = 0;
        public int QntCervejaComum { get; set; } = 0;
        public int QntCervejaArtesanal { get; set; } = 0;
        public int QntEspumanteNacional { get; set; } = 0;
        public int QntEspumanteImportado { get; set; } = 0;

        public static List<Bebidas> Qntbebidas = new List<Bebidas>();

        public static void AdicionarQuantidadeDeBebidas(Bebidas bebidas)
        {
            Qntbebidas.Add(bebidas);
        }
    }
}
