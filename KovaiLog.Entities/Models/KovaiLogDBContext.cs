using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace KovaiLog.Entities.Models
{

    public class KovaiLogDBContext : DbContext
    {
        public KovaiLogDBContext()
            : base("DefaultConnection")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }
        
        public static KovaiLogDBContext Create()
        {
            return new KovaiLogDBContext();
        }

        public DbSet<User> User { get; set; }
        public DbSet<TypeMaster> Type { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Content>()
            //.HasRequired<TypeMaster>(s => s.ContentTypes)
            //.WithMany(g => g.Content)
            //.HasForeignKey<int>(s => s.ContentType);
        }
    }
}