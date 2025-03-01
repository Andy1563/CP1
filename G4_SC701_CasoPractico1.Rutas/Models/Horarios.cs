namespace G4_SC701_CasoPractico1.Rutas.Models
{
    public class Horarios
    {
        public int Id { get; set; }
        public DateTime Horario { get; set; }

        public Ruta rutas { get; set; }
    }
}
