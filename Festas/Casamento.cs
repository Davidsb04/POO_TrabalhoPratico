﻿using POO_TrabalhoPratico.Enums;
using POO_TrabalhoPratico.Helpers;
using POO_TrabalhoPratico.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_TrabalhoPratico.Festas
{
    internal class Casamento : Festa
    {
        ICasamento casamento;

        public Casamento(double preco, DateTime data, Espaco espaco, TipoFesta tipoFesta, NivelFesta nivelFesta, int numConvidados) 
            : base(preco, data, espaco, tipoFesta, nivelFesta)
        {
            casamento = new CalcularValorFesta();

            try
            {
                Festa? ultimaFesta = FestaCia.Festas.LastOrDefault();
                ultimaFesta?.SetPreco(casamento.CalcularValorCasamento(numConvidados, nivelFesta, espaco));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possível alterar o valor da festa. Detalhes: " + ex.Message);
            }

            
        }
    }
}