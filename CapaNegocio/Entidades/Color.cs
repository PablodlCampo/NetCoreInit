namespace CapaNegocio.Entidades
{
    public class Color
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public bool Primario 
        {
            get
            {
                bool primario = false;

                if (Nombre.Trim().ToLower().Equals("rojo") || Nombre.Trim().ToLower().Equals("verde") || Nombre.Trim().ToLower().Equals("azul"))
                {
                    primario = true;
                }

                return primario;
            }
        }
    }
}
