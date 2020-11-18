using CapaNegocio.InterfacesServicios;

namespace CapaNegocio.Servicios
{
    public class NumerosService : INumerosService
    {
        public int SumarNumeros(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
