namespace ML
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Fecha { get; set; }
        public List<object> MovieList { get; set; }
    }
}