using System.ComponentModel.DataAnnotations;

namespace CapaDominio.Entities
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Primario 
        {
            get
            {
                bool primario = false;

                if (!string.IsNullOrEmpty(Nombre) && (Nombre.Trim().ToLower().Equals("rojo") || Nombre.Trim().ToLower().Equals("verde") || Nombre.Trim().ToLower().Equals("azul")))
                {
                    primario = true;
                }

                return primario;
            }
        }
    }
}
