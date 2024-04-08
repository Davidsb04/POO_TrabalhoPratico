using Moq;
using POO_TrabalhoPratico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico.Testes
{
    public class NoivaCiaTests
    {
        [Fact]
        public void AgendarCerimonia_QuandoEntradaValida()
        {
            // Arrange
            NoivaCia noivaCia = new NoivaCia();
            int numConvidados = 100;

            // Act
            (DateTime dataCerimonia, Espaco melhorEspaco) = noivaCia.AgendarCerimonia(numConvidados);

            // Assert
            DateTime dataEsperada = new(2024, 5, 10);
            Assert.Equal(dataEsperada, dataCerimonia);
            Assert.Equal("A", melhorEspaco.Identificador);
        }
        [Fact]
        public void CalcularProximaData_QuandoEntradaValida()
        {
            // Arrange
            NoivaCia noivaCia = new NoivaCia();
            var numConvidados = 100;

            // Act
            DateTime dataCerimonia = noivaCia.CalcularProximaData(numConvidados);

            // Assert
            Assert.Equal(new(2024, 5, 10) ,dataCerimonia);
        }
        [Fact]
        public void VerificarCerimoniaNaData_QuandoRepetido()
        {
            // Arrange
            List<Cerimonia> cerimonias = new List<Cerimonia>();
            DateTime data = new(2024, 5, 10);
            Espaco espaco = new("A", 100);

            Cerimonia novaCerimonia = new(data, espaco);
            cerimonias.Add(novaCerimonia);

            NoivaCia noivaCia = new NoivaCia();

            // Act

            bool verificarCerimonia = !noivaCia.VerificarCerimonaNaData(espaco, data);

            // Assert
            Assert.False(verificarCerimonia);
        }
        [Fact]
        public void SelecionarMelhorEspaco_QuandoEntradaValida()
        {
            // Arrange
            NoivaCia noivaCia = new NoivaCia();
            var numConvidados = 72;
            DateTime data = new(2024, 5, 10);

            // Act
            Espaco melhorEspaco = noivaCia.SelecionarMelhorEspaco(numConvidados, data);

            // Assert
            Assert.Equal("A", melhorEspaco.Identificador);
            Assert.Equal(100, melhorEspaco.Capacidade);
        }
    }
}
