using CapaNegocio.Servicios;
using Xunit;

namespace PruebasXUnit
{
    public class NumerosTests
    {
        [Fact]
        public void Suma_DadosDosNumeros()
        {
            NumerosService numService = new NumerosService();

            int numSum = numService.SumarNumeros(1, 2);

            Assert.Equal(3, numSum);
        }
    }
}
