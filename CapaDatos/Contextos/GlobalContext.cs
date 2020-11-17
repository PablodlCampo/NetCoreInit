using CapaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CapaDatos.Contextos
{
    //DbContext combina las funcionalidades de Unit of Work y de los repositorios para permitir interactuar con la BD
    public class GlobalContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //Todavía no se porque hay que añadir esta linea de opciones y pasarlas al base. La lógica interna parece compleja.
        public GlobalContext(DbContextOptions<GlobalContext> options) : base(options) { }

        //Cada Dbset permite interactuar con la tabla de su mismo tipo. Traduce LINQ en codigo SQL comprensible por la base de datos.
        public DbSet<Color> Colores { get; set; }

        //Para replicar las entidades en base de datos hay que tener instalada la librerias Microsoft.EntityFrameworkCore.Tools
        // y ejecutar el comando Add-Migration y despues el comando Update-Database
    }
}
