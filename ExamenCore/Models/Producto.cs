namespace ExamenCore.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
    }
}
