using Microsoft.EntityFrameworkCore;
using Tempero_do_Dragao.Model;

namespace Tempero_do_Dragao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── TABELAS SIMPLES ──────────────────────────────────
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Difficulty>().ToTable("Difficulty");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Measurement>().ToTable("Measurement");

            // ── USER ─────────────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.IsAdmin).HasDefaultValue(false);
                entity.Property(u => u.Status).HasDefaultValue(0);
            });

            // ── RECIPE ───────────────────────────────────────────
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");
                entity.Property(r => r.Status).HasDefaultValue(0);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(r => r.Description).IsRequired(false);

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Recipes)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Category)
                    .WithMany(c => c.Recipes)
                    .HasForeignKey(r => r.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Difficulty)
                    .WithMany(d => d.Recipes)
                    .HasForeignKey(r => r.DifficultyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── RECIPE INGREDIENT ─────────────────────────────────
            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.ToTable("RecipeIngredient");

                entity.HasOne(ri => ri.Recipe)
                    .WithMany(r => r.RecipeIngredients)
                    .HasForeignKey(ri => ri.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ri => ri.Ingredient)
                    .WithMany(i => i.RecipeIngredients)
                    .HasForeignKey(ri => ri.IngredientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ri => ri.Measurement)
                    .WithMany(m => m.RecipeIngredients)
                    .HasForeignKey(ri => ri.MeasurementId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── FAVORITE ──────────────────────────────────────────
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("Favorite");
                entity.HasIndex(f => new { f.UserId, f.RecipeId }).IsUnique();

                entity.HasOne(f => f.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Recipe)
                    .WithMany(r => r.Favorites)
                    .HasForeignKey(f => f.RecipeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── COMMENT ───────────────────────────────────────────
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Recipe)
                    .WithMany(r => r.Comments)
                    .HasForeignKey(c => c.RecipeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── RATING ────────────────────────────────────────────
            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");
                entity.HasIndex(r => new { r.UserId, r.RecipeId }).IsUnique();

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Ratings)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Recipe)
                    .WithMany(r => r.Ratings)
                    .HasForeignKey(r => r.RecipeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}