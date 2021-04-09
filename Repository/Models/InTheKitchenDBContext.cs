using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.Models
{
    public partial class InTheKitchenDBContext : DbContext
    {
        public InTheKitchenDBContext()
        {
        }

        public InTheKitchenDBContext(DbContextOptions<InTheKitchenDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeAuthor> RecipeAuthors { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual DbSet<RecipeTag> RecipeTags { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserSearchHistory> UserSearchHistories { get; set; }
        public virtual DbSet<UserViewHistory> UserViewHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:cookserver.database.windows.net,1433;Database=InTheKitchenDB;Uid=sa_kitchen;Pwd=Password123;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;");
            }
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.IngredientDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IngredientImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdPartyApiId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastPrepared).HasColumnType("datetime");

                entity.Property(e => e.RecipeAuthor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RecipeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RecipeAuthor>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.UserId })
                    .HasName("RecipeAuthorKey");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeAuthors)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__RecipeAut__Recip__71D1E811");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RecipeAuthors)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RecipeAut__UserI__72C60C4A");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId })
                    .HasName("RecipeIngredientKey");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("FK__RecipeIng__Ingre__6A30C649");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__RecipeIng__Recip__693CA210");
            });

            modelBuilder.Entity<RecipeTag>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.TagId })
                    .HasName("RecipeTagKey");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeTags)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__RecipeTag__Recip__656C112C");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.RecipeTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__RecipeTag__TagId__66603565");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Review__RecipeId__7C4F7684");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Review__UserId__7D439ABD");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.Property(e => e.StepDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StepImage)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Steps)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Steps__RecipeId__628FA481");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Auth0)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateLastAccessed).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Users__Permissio__6EF57B66");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId)
                    .HasName("PK__UserPerm__EFA6FB2FD765CA65");

                entity.Property(e => e.PermissionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserSearchHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__UserSear__4D7B4ABD05F46B17");

                entity.ToTable("UserSearchHistory");

                entity.Property(e => e.SearchDate).HasColumnType("datetime");

                entity.Property(e => e.SearchString)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSearchHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__UserSearc__UserI__75A278F5");
            });

            modelBuilder.Entity<UserViewHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__UserView__4D7B4ABD1960AEDC");

                entity.ToTable("UserViewHistory");

                entity.Property(e => e.ViewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.UserViewHistories)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__UserViewH__Recip__787EE5A0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserViewHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__UserViewH__UserI__797309D9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
