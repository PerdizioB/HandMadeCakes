
using HandMadeCakes.Models;
using HandMadeCakes.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HandMadeCakes.Data
    {
    public class AppDbContext : IdentityDbContext<ApplicationUser>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

        // Adicione outros DbSet se necessário
        public DbSet<CakeModel> Cake { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CakeImage> CakeImages { get; set; }
        public DbSet<ProductModel> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura o tipo decimal para evitar truncamento
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);  // até 18 dígitos no total, 2 após a vírgula

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CakeImage>()
                .HasOne(ci => ci.Cake)       // cada imagem tem 1 bolo
                .WithMany(c => c.Images)     // cada bolo tem muitas imagens
                .HasForeignKey(ci => ci.CakeId) // fk
                .OnDelete(DeleteBehavior.Cascade); // deletar bolo = deleta imagens

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)       // cada imagem tem 1 produto
                .WithMany(p => p.Images)     // cada produto tem muitas imagens
                .HasForeignKey(pi => pi.ProductId) // fk
                .OnDelete(DeleteBehavior.Cascade); // deletar bolo = deleta imagens

        }
    }
}

    






