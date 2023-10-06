using Microsoft.EntityFrameworkCore;

namespace  Models{
    public class Context : DbContext{

        public DbSet<Slika> Slike{get; set;}
        public DbSet<Boja> Boje{get; set;}
        public DbSet<Materijal> Materijali{get; set;}
        public DbSet<Kategorija> Kategorije{get; set;}
        public DbSet<Prodavnica> Prodavnice{get; set;}
        public Context(DbContextOptions options) : base(options){

        }
    }
}