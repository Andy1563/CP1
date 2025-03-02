using Microsoft.EntityFrameworkCore;
using G4_SC701_CasoPractico1.Rutas.Models;

namespace G4_SC701_CasoPractico1.Rutas.Models
{
    public class CP1Context : DbContext
    {
        public CP1Context(DbContextOptions<CP1Context> options) : base(options){ }

        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<Boleto> Boletos { get; set; }

        public DbSet<Ruta> Rutas { get; set; }

        public DbSet<Paradas> Parada {  get; set; }

        public DbSet<Horarios> Horarios { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(Usuario =>
            {
                Usuario.HasKey(u => u.Id);
                Usuario.Property(u => u.NombreUsuario).HasMaxLength(50).IsRequired().HasColumnName("Usuario");
                Usuario.Property(u => u.NombreCompleto).HasMaxLength(100).IsRequired().IsUnicode(true);
                Usuario.Property(u => u.CorreoElectronico).HasMaxLength(100).IsRequired();
            });
            modelBuilder.Entity<Rol>(Rol =>
            {
                Rol.HasKey(r => r.Id);
                Rol.Property(r => r.Nombre).HasMaxLength(50).IsRequired();

            });

            modelBuilder.Entity<Rol>().HasMany<Usuario>(r => r.Usuarios)
                                    .WithOne(u => u.Rol)
                                    .HasForeignKey(u => u.RolId)
                                    .HasConstraintName("FK_Usuario_Rol")
                                    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Vehiculo>(Vehiculo =>
            {
                Vehiculo.HasKey(u => u.Id);
                Vehiculo.Property(n => n.Placa).IsRequired().HasMaxLength(6);
                Vehiculo.Property(m => m.Modelo).HasMaxLength(200).IsRequired();
                Vehiculo.Property(c => c.CapacidadPasajeros).IsRequired();
                Vehiculo.Property(e => e.Estado).IsRequired().HasMaxLength(50);
                
            });
            modelBuilder.Entity<Vehiculo>().HasMany<Usuario>(ve => ve.usuario).WithOne(user => user.vehiculo).HasForeignKey(v => v.idVehiculo);

            /*Boletos*/
            modelBuilder.Entity<Boleto>(boleto =>
            {
                boleto.HasKey(b => b.Id);

                boleto.HasOne(b => b.ruta).WithMany().HasForeignKey(b => b.IdRuta).OnDelete(DeleteBehavior.Restrict);

                boleto.HasOne(b => b.usuario).WithMany().HasForeignKey(b => b.IdUsuario).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ruta>(ruta =>
            {
                ruta.HasKey(r => r.Id);

                ruta.HasOne(r => r.usuario).WithMany().HasForeignKey(r => r.IdUsuarioRegistro).OnDelete(DeleteBehavior.Restrict);

                ruta.HasOne(r => r.vehiculo).WithMany().HasForeignKey(r => r.IdVehiculo).OnDelete(DeleteBehavior.Restrict);

                ruta.HasMany(r => r.paradas).WithOne(p => p.ruta).OnDelete(DeleteBehavior.Cascade);

                ruta.HasMany(r => r.horario).WithOne(h => h.rutas).OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Paradas
            modelBuilder.Entity<Paradas>(parada =>
            {
                parada.HasKey(p => p.Id);

                parada.HasOne(p => p.ruta).WithMany(r => r.paradas).OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de Horarios
            modelBuilder.Entity<Horarios>(horario =>
            {
                horario.HasKey(h => h.Id);

                horario.HasOne(h => h.rutas).WithMany(r => r.horario).OnDelete(DeleteBehavior.Cascade);
            });



        }
        public DbSet<G4_SC701_CasoPractico1.Rutas.Models.Ruta> Ruta { get; set; } = default!;
        public DbSet<G4_SC701_CasoPractico1.Rutas.Models.Paradas> Paradas { get; set; } = default!;
        
    }
}


    
