using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Persistence.Configurations
{
    public class ConfiguracionPedidoDet : IEntityTypeConfiguration<PedidoDet>
    {
        public void Configure(EntityTypeBuilder<PedidoDet> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.ToTable("PedidoDet");

            builder.HasComment("Listado de Pedidos");

            builder.HasIndex(p => p.IdCliente, "IXFK_PedidoDet_Cliente");

            builder.HasIndex(p => p.IdEmpleado, "IXFK_PedidoDet_Empleado");

            builder.HasIndex(p => p.IdProdMaestro, "IXFK_PedidoDet_ProdMaestro");

            builder.HasIndex(p => p.IdMarca, "IXFK_PedidoDet_Marca");

            builder.HasIndex(p => new { p.IdCliente, p.IdProdMaestro, p.IdMarca, p.Estatus }, "IX_NoDuplicado").IsUnique();

            builder.Property(p => p.Id).HasComment("Id Consecutivo del pedido");

            builder.Property(p => p.Estatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength(true)
                .HasComment("Estatus del articulo del Pedidio");

            builder.Property(p => p.FechaCreacion)
                .HasColumnType("datetime")
                .HasDefaultValue(new DateTime(1900, 1, 1, 00, 00, 00, 000))
                .HasComment("Fecha de Creacion");

            builder.Property(p => p.FechaModificacion)
                .HasColumnType("datetime")
                .HasDefaultValue(new DateTime(1900, 1, 1, 00, 00, 00, 000))
                .HasComment("Fecha de Creacion");

            builder.Property(p => p.IdCliente).HasComment("El id del Cliente");

            builder.Property(p => p.IdEmpleado).HasComment("El id del Empleado");

            builder.Property(p => p.Cantidad).HasComment("Cantidad solicitada");

            builder.Property(p => p.IdProdMaestro).HasComment("Id del catalogo de ProdMaestro");

            builder.Property(p => p.IdMarca).HasComment("Id del Catalogo de Marcas");

            builder.Property(p => p.EsCadaUno).HasComment("Si el precio es por unidad y no por caja");

            builder.Property(p => p.Notas)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Notas para el que va a surtir el reglon del Pedido");
        }
    }
}
