using POO_TrabalhoPratico.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Helpers.Interfaces
{
    internal interface IFestaEmpresa
    {
        double CalcularValorEmpresa(int numConvidados, NivelFesta tipoCasamento, Espaco espaco);
    }
}
