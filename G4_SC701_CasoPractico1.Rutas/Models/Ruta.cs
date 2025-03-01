namespace G4_SC701_CasoPractico1.Rutas.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public string NombreRuta { get; set; }
        public bool Estado { get; set; }

        public int IdParada { get; set; }

        public DateTime FechaRegistro { get; set; }
        public int IdUsuarioRegistro { get; set; }
        

        public Usuario usuario { get; set; }

        public IEnumerable <Paradas>? paradas { get; set; }

        public IEnumerable <Horarios> horario { get; set; }



    }
}
