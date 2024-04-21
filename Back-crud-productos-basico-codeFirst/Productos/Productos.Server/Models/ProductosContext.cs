using Microsoft.EntityFrameworkCore;

namespace Productos.Server.Models
{
    public class ProductosContext : DbContext // HEREDA
    {
        //CONSTRUCTOR
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        { }

        public DbSet<Producto> Productos { get; set; } //se agrega la clase producto a la base de datos por medio de dbset y se va a llamar Productos

        //sobreescribimos el metodo para q el nombre de c producto sea unico
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>().HasIndex(c => c.Nombre).IsUnique(); 
        }
    }
}
