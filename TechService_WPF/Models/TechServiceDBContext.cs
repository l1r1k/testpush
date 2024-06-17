using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TechService_WPF.Models
{
    public partial class TechServiceDBContext : DbContext
    {
        public TechServiceDBContext()
        {
        }

        public TechServiceDBContext(DbContextOptions<TechServiceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<Executor> Executors { get; set; } = null!;
        public virtual DbSet<ExecutorComment> ExecutorComments { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<StatusRequest> StatusRequests { get; set; } = null!;
        public virtual DbSet<TypeErrorSupply> TypeErrorSupplies { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserService> UserServices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-HFJP1ODU\\NEWSERVER;Initial Catalog=TechServiceDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PK__Client__B5AE4EC8A56BDEA3");

                entity.ToTable("Client");

                entity.Property(e => e.IdClient).HasColumnName("ID_Client");

                entity.Property(e => e.NameClient)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Name_Client");

                entity.Property(e => e.PatronymicClient)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Patronymic_Client");

                entity.Property(e => e.PhoneNumberClient)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("PhoneNumber_Client");

                entity.Property(e => e.SurnameClient)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Surname_Client");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.IdEquipment)
                    .HasName("PK__Equipmen__4DDD08B20D16C645");

                entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");

                entity.Property(e => e.NameEquipment)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Name_Equipment");
            });

            modelBuilder.Entity<Executor>(entity =>
            {
                entity.HasKey(e => e.IdExecutor)
                    .HasName("PK__Executor__67652F4558FB1CF2");

                entity.ToTable("Executor");

                entity.Property(e => e.IdExecutor).HasColumnName("ID_Executor");

                entity.Property(e => e.IdRequest).HasColumnName("ID_Request");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.HasOne(d => d.IdRequestNavigation)
                    .WithMany(p => p.Executors)
                    .HasForeignKey(d => d.IdRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Executor__ID_Req__5FB337D6");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Executors)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Executor__ID_Use__5EBF139D");
            });

            modelBuilder.Entity<ExecutorComment>(entity =>
            {
                entity.HasKey(e => e.IdExecutorComment)
                    .HasName("PK__Executor__55263512F67796E9");

                entity.ToTable("ExecutorComment");

                entity.Property(e => e.IdExecutorComment).HasColumnName("ID_ExecutorComment");

                entity.Property(e => e.CommentExecutorComment)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("Comment_ExecutorComment");

                entity.Property(e => e.IdExecutor).HasColumnName("ID_Executor");

                entity.HasOne(d => d.IdExecutorNavigation)
                    .WithMany(p => p.ExecutorComments)
                    .HasForeignKey(d => d.IdExecutor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExecutorC__ID_Ex__628FA481");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.IdRequest)
                    .HasName("PK__Request__D55098805D1A766F");

                entity.ToTable("Request");

                entity.Property(e => e.IdRequest).HasColumnName("ID_Request");

                entity.Property(e => e.DateAddingRequest)
                    .HasColumnType("date")
                    .HasColumnName("DateAdding_Request")
                    .HasDefaultValueSql("(CONVERT([date],getdate()))");

                entity.Property(e => e.DateClosingRequest)
                    .HasColumnType("date")
                    .HasColumnName("DateClosing_Request")
                    .HasDefaultValueSql("(CONVERT([date],getdate()))");

                entity.Property(e => e.IdClient).HasColumnName("ID_Client");

                entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");

                entity.Property(e => e.IdStatusRequest).HasColumnName("ID_StatusRequest");

                entity.Property(e => e.IdTypeErrorSupply).HasColumnName("ID_TypeErrorSupply");

                entity.Property(e => e.InfoAboutProblemRequest)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("InfoAboutProblem_Request");

                entity.Property(e => e.NumberRequest)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Number_Request");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__ID_Clie__5AEE82B9");

                entity.HasOne(d => d.IdEquipmentNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdEquipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__ID_Equi__59063A47");

                entity.HasOne(d => d.IdStatusRequestNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdStatusRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__ID_Stat__5BE2A6F2");

                entity.HasOne(d => d.IdTypeErrorSupplyNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.IdTypeErrorSupply)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__ID_Type__59FA5E80");
            });

            modelBuilder.Entity<StatusRequest>(entity =>
            {
                entity.HasKey(e => e.IdStatusRequest)
                    .HasName("PK__StatusRe__6A26B0E52CE518E5");

                entity.ToTable("StatusRequest");

                entity.Property(e => e.IdStatusRequest).HasColumnName("ID_StatusRequest");

                entity.Property(e => e.NameStatusRequest)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Name_StatusRequest");
            });

            modelBuilder.Entity<TypeErrorSupply>(entity =>
            {
                entity.HasKey(e => e.IdTypeErrorSupply)
                    .HasName("PK__TypeErro__92672888432AD42E");

                entity.ToTable("TypeErrorSupply");

                entity.Property(e => e.IdTypeErrorSupply).HasColumnName("ID_TypeErrorSupply");

                entity.Property(e => e.NameTypeErrorSupply)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Name_TypeErrorSupply");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IdUserRole)
                    .HasName("PK__UserRole__8C6D9E877B621499");

                entity.ToTable("UserRole");

                entity.Property(e => e.IdUserRole).HasColumnName("ID_UserRole");

                entity.Property(e => e.NameUserRole)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Name_UserRole");
            });

            modelBuilder.Entity<UserService>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__UserServ__ED4DE44261FFEF31");

                entity.ToTable("UserService");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.IdUserRole).HasColumnName("ID_UserRole");

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Login_User");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Name_User");

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Password_User");

                entity.Property(e => e.PatronymicUser)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Patronymic_User")
                    .HasDefaultValueSql("('Отсутствует')");

                entity.Property(e => e.SurnameUser)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("Surname_User");

                entity.HasOne(d => d.IdUserRoleNavigation)
                    .WithMany(p => p.UserServices)
                    .HasForeignKey(d => d.IdUserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserServi__ID_Us__4CA06362");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
