using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Contextos
{
    public class NSysPDbContext : DbContext
    {
        private readonly IFechaHoraServicio _fechaHoraServicio;

        // inyecto la interfaz fechaHora como una dependencia
        public NSysPDbContext(DbContextOptions<NSysPDbContext> options, IFechaHoraServicio fechaHoraServicio) : base(options)
        {
            // Por rendimiento
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this._fechaHoraServicio = fechaHoraServicio;
        }

        public DbSet<PedidoDet> PedidoDet { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.FechaModificacion = _fechaHoraServicio.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.FechaCreacion = _fechaHoraServicio.Now;
                        entry.Entity.Estatus = "P";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
