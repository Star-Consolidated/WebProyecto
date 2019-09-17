using WebProyecto.Models;
using Microsoft.EntityFrameworkCore;
namespace WebProyecto.Data
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {}
        public DbSet<Caracteristica> Caracteristicas{get;set;}
        public DbSet<Carrito> Carritos{get;set;}
        public DbSet<Categoria> Categorias{get;set;}
        public DbSet<Detalle_Pedido> Detalle_Pedidos{get;set;}
        public DbSet<Opcion> Opciones{get;set;}
        public DbSet<Pedido> Pedidos{get;set;}
        public DbSet<Producto_Categoria> Producto_Categorias{get;set;}
        public DbSet<Producto> Productos{get;set;}
        public DbSet<Tienda> Tiendas{get;set;}
        public DbSet<Usuario> Usuarios{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Caracteristica>().ToTable("caracteristicas");
            modelBuilder.Entity<Carrito>().ToTable("carritos");
            modelBuilder.Entity<Categoria>().ToTable("categorias");
            modelBuilder.Entity<Detalle_Pedido>().ToTable("detalle_pedidos");
            modelBuilder.Entity<Opcion>().ToTable("opciones");
            modelBuilder.Entity<Pedido>().ToTable("pedidos");
            modelBuilder.Entity<Producto_Categoria>().ToTable("producto_categorias");
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Tienda>().ToTable("tiendas");
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
        }
    }
}