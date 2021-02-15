using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RealTimeChatApp.Models
{
    public partial class CHATAPPDBContext : DbContext
    {
        public CHATAPPDBContext()
        {
        }

        public CHATAPPDBContext(DbContextOptions<CHATAPPDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<MessageLog> MessageLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=CHATAPPDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.ToTable("Channel");

                entity.Property(e => e.ChannelName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<MessageLog>(entity =>
            {
                entity.ToTable("MessageLog");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.MessageLogs)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__MessageLo__Chann__4E88ABD4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
