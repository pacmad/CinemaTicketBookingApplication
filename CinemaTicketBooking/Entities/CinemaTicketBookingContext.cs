using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CinemaTicketBooking.Entities
{
    public partial class CinemaTicketBookingContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<TblAddress> TblAddress { get; set; }
        public virtual DbSet<TblCinema> TblCinema { get; set; }
        public virtual DbSet<TblCities> TblCities { get; set; }
        public virtual DbSet<TblCountries> TblCountries { get; set; }
        public virtual DbSet<TblCustomerComments> TblCustomerComments { get; set; }
        public virtual DbSet<TblLanguage> TblLanguage { get; set; }
        public virtual DbSet<TblMovie> TblMovie { get; set; }
        public virtual DbSet<TblMovieGenre> TblMovieGenre { get; set; }
        public virtual DbSet<TblPaymentType> TblPaymentType { get; set; }
        public virtual DbSet<TblPromotion> TblPromotion { get; set; }
        public virtual DbSet<TblReservations> TblReservations { get; set; }
        public virtual DbSet<TblReservationStatus> TblReservationStatus { get; set; }
        public virtual DbSet<TblShowTime> TblShowTime { get; set; }
        public virtual DbSet<TblTicket> TblTicket { get; set; }

        public CinemaTicketBookingContext(DbContextOptions<CinemaTicketBookingContext> options)
    : base(options)
        {
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=RINORS-G5;Database=CinemaTicketBooking;Trusted_Connection=True;User Id=sa; Password=P@ssw0rd;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<TblAddress>(entity =>
            {
                entity.HasKey(e => e.AdressId);

                entity.ToTable("tbl_Address");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.StreetName).HasMaxLength(500);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TblAddress)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Address_tbl_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblAddress)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Address_tbl_Countries");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblAddressCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Address_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblAddressLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Address_AspNetUsers1");
            });

            modelBuilder.Entity<TblCinema>(entity =>
            {
                entity.HasKey(e => e.CinemaId);

                entity.ToTable("tbl_Cinema");

                entity.Property(e => e.AdminUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CinemaDescription)
                    .IsRequired()
                    .HasMaxLength(2500);

                entity.Property(e => e.CinemaName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CinemaProfilePicture)
                    .IsRequired()
                    .HasMaxLength(2500);

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.AdminUser)
                    .WithMany(p => p.TblCinemaAdminUser)
                    .HasForeignKey(d => d.AdminUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cinema_AspNetUsers2");

                entity.HasOne(d => d.Adress)
                    .WithMany(p => p.TblCinema)
                    .HasForeignKey(d => d.AdressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cinema_tbl_Address");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblCinemaCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cinema_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblCinemaLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cinema_AspNetUsers1");
            });

            modelBuilder.Entity<TblCities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("tbl_Cities");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.TblCities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cities_tbl_Countries");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblCitiesCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cities_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblCitiesLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Cities_AspNetUsers1");
            });

            modelBuilder.Entity<TblCountries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("tbl_Countries");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblCountriesCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Countries_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblCountriesLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Countries_AspNetUsers1");
            });

            modelBuilder.Entity<TblCustomerComments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("tbl_CustomerComments");

                entity.Property(e => e.CommentDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.TblCustomerComments)
                    .HasForeignKey(d => d.CinemaId)
                    .HasConstraintName("FK_tbl_CustomerComments_tbl_Cinema");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblCustomerCommentsCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_CustomerComments_AspNetUsers2");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblCustomerCommentsLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_CustomerComments_AspNetUsers3");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.TblCustomerComments)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("FK_tbl_CustomerComments_tbl_Movie");
            });

            modelBuilder.Entity<TblLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId);

                entity.ToTable("tbl_Language");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LanguageName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblLanguageCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Language_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblLanguageLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Language_AspNetUsers1");
            });

            modelBuilder.Entity<TblMovie>(entity =>
            {
                entity.HasKey(e => e.MovieId);

                entity.ToTable("tbl_Movie");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.MovieDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.MovieLength)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.MovieName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PriceForAdults).HasColumnType("decimal(2, 2)");

                entity.Property(e => e.PriceForChildrens).HasColumnType("decimal(2, 2)");

                entity.Property(e => e.ReleaseDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ShowTimeIds)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.TblMovie)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Movie_tbl_Cinema");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblMovieCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Movie_AspNetUsers");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.TblMovie)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Movie_tbl_Language");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblMovieLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Movie_AspNetUsers1");

                entity.HasOne(d => d.MovieGenre)
                    .WithMany(p => p.TblMovie)
                    .HasForeignKey(d => d.MovieGenreId)
                    .HasConstraintName("FK_tbl_Movie_tbl_MovieGenre");
            });

            modelBuilder.Entity<TblMovieGenre>(entity =>
            {
                entity.HasKey(e => e.MovieGenreId);

                entity.ToTable("tbl_MovieGenre");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.GenreDescription)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.MovieGenreName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblMovieGenreCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_MovieGenre_AspNetUsers2");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblMovieGenreLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_MovieGenre_AspNetUsers3");
            });

            modelBuilder.Entity<TblPaymentType>(entity =>
            {
                entity.HasKey(e => e.PaymentTypeId);

                entity.ToTable("tbl_PaymentType");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.PaymentTypeName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblPaymentTypeCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_PaymentType_AspNetUsers2");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblPaymentTypeLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_PaymentType_AspNetUsers3");
            });

            modelBuilder.Entity<TblPromotion>(entity =>
            {
                entity.HasKey(e => e.PromotionId);

                entity.ToTable("tbl_Promotion");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.PromotionDescription)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PromotionDiscount).HasColumnType("decimal(2, 2)");

                entity.Property(e => e.PromotionEndDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PromotionStartDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblPromotionCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Promotion_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblPromotionLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Promotion_AspNetUsers1");
            });

            modelBuilder.Entity<TblReservations>(entity =>
            {
                entity.HasKey(e => e.ReservationId);

                entity.ToTable("tbl_Reservations");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.ReservedByCustomerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblReservationsCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_AspNetUsers3");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblReservationsLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_AspNetUsers4");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.TblReservations)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_tbl_PaymentType");

                entity.HasOne(d => d.ReservationStatus)
                    .WithMany(p => p.TblReservations)
                    .HasForeignKey(d => d.ReservationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_tbl_ReservationStatus");

                entity.HasOne(d => d.ReservedByCustomer)
                    .WithMany(p => p.TblReservationsReservedByCustomer)
                    .HasForeignKey(d => d.ReservedByCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_AspNetUsers");

                entity.HasOne(d => d.ReservedForMovie)
                    .WithMany(p => p.TblReservations)
                    .HasForeignKey(d => d.ReservedForMovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_tbl_Movie");

                entity.HasOne(d => d.ReservedInAdressNavigation)
                    .WithMany(p => p.TblReservations)
                    .HasForeignKey(d => d.ReservedInAdress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_tbl_Address");

                entity.HasOne(d => d.ReservedInCinema)
                    .WithMany(p => p.TblReservations)
                    .HasForeignKey(d => d.ReservedInCinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Reservations_tbl_Cinema");
            });

            modelBuilder.Entity<TblReservationStatus>(entity =>
            {
                entity.HasKey(e => e.ReservationStatusId);

                entity.ToTable("tbl_ReservationStatus");

                entity.Property(e => e.ReservationStatusId).ValueGeneratedNever();

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblReservationStatusCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ReservationStatus_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblReservationStatusLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ReservationStatus_AspNetUsers1");
            });

            modelBuilder.Entity<TblShowTime>(entity =>
            {
                entity.HasKey(e => e.ShowTimeId);

                entity.ToTable("tbl_ShowTime");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.TblShowTime)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ShowTime_tbl_Cinema");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblShowTimeCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ShowTime_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblShowTimeLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ShowTime_AspNetUsers1");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.TblShowTime)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_ShowTime_tbl_Movie");
            });

            modelBuilder.Entity<TblTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.ToTable("tbl_Ticket");

                entity.Property(e => e.CreatedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOnDate)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedByUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.LastModifiedOnDate).HasMaxLength(150);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SeatNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalPrice)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cinema)
                    .WithMany(p => p.TblTicket)
                    .HasForeignKey(d => d.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Ticket_tbl_Cinema");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TblTicketCreatedByUser)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Ticket_AspNetUsers1");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblTicketCustomer)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Ticket_AspNetUsers");

                entity.HasOne(d => d.LastModifiedByUser)
                    .WithMany(p => p.TblTicketLastModifiedByUser)
                    .HasForeignKey(d => d.LastModifiedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Ticket_AspNetUsers2");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.TblTicket)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_Ticket_tbl_Movie");
            });
        }
    }
}
