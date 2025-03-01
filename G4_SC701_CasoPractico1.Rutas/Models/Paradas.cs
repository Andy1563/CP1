namespace G4_SC701_CasoPractico1.Rutas.Models
{
    public class Paradas
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        
        public Ruta ruta { get; set; }
    }
}
