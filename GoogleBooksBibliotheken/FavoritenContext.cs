using System.Data.Entity;

namespace GoogleBooksBibliotheken
{
    public class FavoritenContext : DbContext
    {
        public DbSet<Buch> Favoriten { get; set; }

        public FavoritenContext() : base("Favoriten_EF_DB")
        {
            Database.SetInitializer<FavoritenContext>(new DropCreateDatabaseIfModelChanges<FavoritenContext>());
        }
    }
}
