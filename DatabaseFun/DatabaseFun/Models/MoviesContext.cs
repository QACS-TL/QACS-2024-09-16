﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFun.Models;

public partial class MoviesContext : DbContext
{
    public MoviesContext()
    {
    }

    public MoviesContext(DbContextOptions<MoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCast> MovieCasts { get; set; }

    public virtual DbSet<MovieCrew> MovieCrews { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=Movies;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK_department_department_id");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK_gender_gender_id");

            entity.ToTable("gender");

            entity.Property(e => e.GenderId)
                .ValueGeneratedNever()
                .HasColumnName("gender_id");
            entity.Property(e => e.Gender1)
                .HasMaxLength(20)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("gender");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_genre_genre_id");

            entity.ToTable("genre");

            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("genre_id");
            entity.Property(e => e.GenreName)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK_movie_movie_id");

            entity.ToTable("movie");

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.Budget)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("budget");
            entity.Property(e => e.Homepage)
                .HasMaxLength(1000)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("homepage");
            entity.Property(e => e.MovieStatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("movie_status");
            entity.Property(e => e.Overview)
                .HasMaxLength(1000)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("overview");
            entity.Property(e => e.Popularity)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("decimal(12, 6)")
                .HasColumnName("popularity");
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("release_date");
            entity.Property(e => e.Revenue)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("revenue");
            entity.Property(e => e.Runtime)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("runtime");
            entity.Property(e => e.Tagline)
                .HasMaxLength(1000)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("tagline");
            entity.Property(e => e.Title)
                .HasMaxLength(1000)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("title");
            entity.Property(e => e.VoteAverage)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("vote_average");
            entity.Property(e => e.VoteCount)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("vote_count");

            entity.HasMany(d => d.Companies).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCompany",
                    r => r.HasOne<ProductionCompany>().WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_movie_company_production_company"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_movie_company_movie1"),
                    j =>
                    {
                        j.HasKey("MovieId", "CompanyId").HasName("PK_movie_company_movie_id");
                        j.ToTable("movie_company");
                        j.IndexerProperty<int>("MovieId").HasColumnName("movie_id");
                        j.IndexerProperty<int>("CompanyId").HasColumnName("company_id");
                    });
        });

        modelBuilder.Entity<MovieCast>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("movie_cast");

            entity.Property(e => e.CastOrder)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cast_order");
            entity.Property(e => e.CharacterName)
                .HasMaxLength(400)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("character_name");
            entity.Property(e => e.GenderId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("gender_id");
            entity.Property(e => e.MovieId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("movie_id");
            entity.Property(e => e.PersonId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("person_id");

            entity.HasOne(d => d.Gender).WithMany()
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("movie_cast$fk_gender");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_movie_cast_movie");

            entity.HasOne(d => d.Person).WithMany()
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("movie_cast$fk_person_2");
        });

        modelBuilder.Entity<MovieCrew>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("movie_crew");

            entity.Property(e => e.DepartmentId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("department_id");
            entity.Property(e => e.Job)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("job");
            entity.Property(e => e.MovieId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("movie_id");
            entity.Property(e => e.PersonId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("person_id");

            entity.HasOne(d => d.Department).WithMany()
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("movie_crew$fk_department");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_movie_crew_movie");

            entity.HasOne(d => d.Person).WithMany()
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("movie_crew$fk_person");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.ToTable("movie_genres");

            entity.Property(e => e.MovieGenreId).HasColumnName("movie_genre_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Genre).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movie_genres$fk_mg_genre");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_movie_genres_movie");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_person_person_id");

            entity.ToTable("person");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("person_id");
            entity.Property(e => e.PersonName)
                .HasMaxLength(500)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("person_name");
        });

        modelBuilder.Entity<ProductionCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_production_company_company_id");

            entity.ToTable("production_company");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedNever()
                .HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("company_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
