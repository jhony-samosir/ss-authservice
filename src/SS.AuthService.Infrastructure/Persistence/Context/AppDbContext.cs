using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SS.AuthService.Infrastructure.Persistence.Entities;

namespace SS.AuthService.Infrastructure.Persistence.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<auth_session> auth_sessions { get; set; }

    public virtual DbSet<email_verification> email_verifications { get; set; }

    public virtual DbSet<login_attempt> login_attempts { get; set; }

    public virtual DbSet<menu> menus { get; set; }

    public virtual DbSet<mfa_recovery_code> mfa_recovery_codes { get; set; }

    public virtual DbSet<password_history> password_histories { get; set; }

    public virtual DbSet<password_reset> password_resets { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<role_menu> role_menus { get; set; }

    public virtual DbSet<social_account> social_accounts { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<auth_session>(entity =>
        {
            entity.HasKey(e => e.id).HasName("auth_sessions_pkey");

            entity.HasIndex(e => e.public_id, "auth_sessions_public_id_key").IsUnique();

            entity.HasIndex(e => e.refresh_token_hash, "auth_sessions_refresh_token_hash_key").IsUnique();

            entity.HasIndex(e => new { e.expires_at, e.is_revoked }, "idx_auth_sessions_expires_revoked");

            entity.HasIndex(e => e.user_id, "idx_auth_sessions_user_id");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.device_info).HasMaxLength(255);
            entity.Property(e => e.public_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.refresh_token_hash).HasMaxLength(255);
            entity.Property(e => e.updated_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<email_verification>(entity =>
        {
            entity.HasKey(e => e.id).HasName("email_verifications_pkey");

            entity.HasIndex(e => e.verification_token_hash, "email_verifications_verification_token_hash_key").IsUnique();

            entity.HasIndex(e => e.user_id, "idx_email_verifications_user_id");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.verification_token_hash).HasMaxLength(255);
        });

        modelBuilder.Entity<login_attempt>(entity =>
        {
            entity.HasKey(e => e.id).HasName("login_attempts_pkey");

            entity.HasIndex(e => e.attempted_at, "idx_login_attempts_attempted_at");

            entity.HasIndex(e => e.ip_address, "idx_login_attempts_ip_address");

            entity.HasIndex(e => e.user_id, "idx_login_attempts_user_id");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.attempted_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.device_info).HasMaxLength(255);
            entity.Property(e => e.email_attempted).HasMaxLength(255);
        });

        modelBuilder.Entity<menu>(entity =>
        {
            entity.HasKey(e => e.id).HasName("menus_pkey");

            entity.HasIndex(e => e.id, "idx_menus_active_only").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.parent_id, "idx_menus_parent_id");

            entity.HasIndex(e => e.public_id, "menus_public_id_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.icon).HasMaxLength(50);
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.path).HasMaxLength(255);
            entity.Property(e => e.public_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<mfa_recovery_code>(entity =>
        {
            entity.HasKey(e => e.id).HasName("mfa_recovery_codes_pkey");

            entity.HasIndex(e => e.user_id, "idx_mfa_recovery_codes_user_id");

            entity.HasIndex(e => e.code_hash, "mfa_recovery_codes_code_hash_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.code_hash).HasMaxLength(255);
        });

        modelBuilder.Entity<password_history>(entity =>
        {
            entity.HasKey(e => e.id).HasName("password_histories_pkey");

            entity.HasIndex(e => e.user_id, "idx_password_histories_user_id");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.password_hash).HasMaxLength(255);
        });

        modelBuilder.Entity<password_reset>(entity =>
        {
            entity.HasKey(e => e.id).HasName("password_resets_pkey");

            entity.HasIndex(e => e.user_id, "idx_password_resets_user_id");

            entity.HasIndex(e => e.reset_token_hash, "password_resets_reset_token_hash_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.reset_token_hash).HasMaxLength(255);
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("roles_pkey");

            entity.HasIndex(e => e.id, "idx_roles_active_only").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.name, "roles_name_key").IsUnique();

            entity.HasIndex(e => e.public_id, "roles_public_id_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.description).HasMaxLength(255);
            entity.Property(e => e.name).HasMaxLength(50);
            entity.Property(e => e.public_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<role_menu>(entity =>
        {
            entity.HasKey(e => e.id).HasName("role_menu_pkey");

            entity.ToTable("role_menu");

            entity.HasIndex(e => new { e.role_id, e.menu_id }, "idx_role_menu_role_menu");

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.can_read).HasDefaultValue(true);
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.updated_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<social_account>(entity =>
        {
            entity.HasKey(e => e.id).HasName("social_accounts_pkey");

            entity.HasIndex(e => e.user_id, "idx_social_accounts_user_id");

            entity.HasIndex(e => new { e.provider, e.provider_account_id }, "uq_provider_account").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.provider).HasMaxLength(50);
            entity.Property(e => e.provider_account_id).HasMaxLength(255);
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");

            entity.HasIndex(e => e.id, "idx_users_active_only").HasFilter("(deleted_at IS NULL)");

            entity.HasIndex(e => e.email, "idx_users_email");

            entity.HasIndex(e => e.is_active, "idx_users_is_active");

            entity.HasIndex(e => e.role_id, "idx_users_role_id");

            entity.HasIndex(e => e.email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.public_id, "users_public_id_key").IsUnique();

            entity.Property(e => e.id).UseIdentityAlwaysColumn();
            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.email).HasMaxLength(255);
            entity.Property(e => e.full_name).HasMaxLength(100);
            entity.Property(e => e.is_active).HasDefaultValue(true);
            entity.Property(e => e.mfa_secret).HasMaxLength(255);
            entity.Property(e => e.password_hash).HasMaxLength(255);
            entity.Property(e => e.public_id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.updated_at).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
