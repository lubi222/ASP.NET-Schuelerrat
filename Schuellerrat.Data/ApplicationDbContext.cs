namespace Schuellerrat.Data
{
    using Microsoft.AspNetCore.Identity;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Club> Clubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Images)
                .WithOne(i => i.Club)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                .HasMany(c => c.Images)
                .WithOne(i => i.Article)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasMany(x => x.Paragraphs)
                .WithOne(x => x.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Paragraphs)
                .WithOne(x => x.Article)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Article>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Article)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}