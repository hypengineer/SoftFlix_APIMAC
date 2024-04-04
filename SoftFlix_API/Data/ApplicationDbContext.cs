using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftFlix_API.Models;

namespace SoftFlix_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<SoftFlixUser, SoftFlixRole, long> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Fluent API
            builder.Entity<MediaCategory>().HasKey(mc => new { mc.MediaId, mc.CategoryId });
            builder.Entity<MediaDirector>().HasKey(md => new { md.MediaId, md.DirectorId });
            builder.Entity<MediaRestriction>().HasKey(mr => new { mr.MediaId, mr.RestrictionId });
            builder.Entity<MediaStar>().HasKey(ms => new { ms.MediaId, ms.StarId });
            builder.Entity<UserFavorite>().HasKey(uf => new { uf.UserId, uf.MediId });
            builder.Entity<UserWatched>().HasKey(uw => new { uw.UserId, uw.EpisodeId });

        }

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Director> Directors { get; set; } = default!;
        public DbSet<Episode> Episodes { get; set; } = default!;
        public DbSet<Media> Medias { get; set; } = default!;
        public DbSet<MediaCategory> MediaCategories { get; set; } = default!;
        public DbSet<MediaDirector> MediaDirectors { get; set; } = default!;
        public DbSet<MediaRestriction> MediaRestrictions { get; set; } = default!;
        public DbSet<MediaStar> MediaStars { get; set; } = default!;
        public DbSet<Plan> Plans { get; set; } = default!;
        public DbSet<Restriction> Restrictions { get; set; } = default!;
        public DbSet<Star> Stars { get; set; } = default!;
        public DbSet<UserFavorite> UserFavorites { get; set; } = default!;
        public DbSet<UserPlan> UserPlans { get; set; } = default!;
        public DbSet<UserWatched> UserWatcheds { get; set; } = default!;


    }
}
