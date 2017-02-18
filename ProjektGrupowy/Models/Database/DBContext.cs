using ProjektGrupowy.Models.Database;
using ProjektGrupowy.Models.Game.Definitions;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ProjektGrupowy.Models.Platform
{
    public class DBContext : DbContext
    {

        public DBContext() : base("DBContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DBPermission> Permissions { get; set; }
        public DbSet<DBCookiePermission> CookiePermissions { get; set; }
        public DbSet<DBGameInstance> GameInstances { get; set; }
        public DbSet<DBGameDefinition> GameDefinitions { get; set; }

        public User GetUser(string email)
        {
            return Users.Where(u => u.Email == email).FirstOrDefault();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }

    }
}