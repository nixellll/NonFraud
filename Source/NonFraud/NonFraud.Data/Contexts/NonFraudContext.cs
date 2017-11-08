namespace NonFraud.Data.Contexts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using NonFraud.Data.Entities;

    /// <summary>
    /// EF generated class for DB Context and define each entity
    /// </summary>
    public partial class NonFraudContext : DbContext
    {
        public NonFraudContext()
            : base("name=NonFraudContext")
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerBalance> CustomerBalance { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerBalance)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Transaction)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerDestID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Transaction1)
                .WithRequired(e => e.Customer1)
                .HasForeignKey(e => e.CustomerOrigID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerBalance>()
                .Property(e => e.OldBalance)
                .HasPrecision(25, 4);

            modelBuilder.Entity<CustomerBalance>()
                .Property(e => e.NewBalance)
                .HasPrecision(25, 4);

            modelBuilder.Entity<Profile>()
                .Property(e => e.ProfileName)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Profile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Amount)
                .HasPrecision(25, 4);

            modelBuilder.Entity<TransactionType>()
                .Property(e => e.TransactionTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionType>()
                .HasMany(e => e.Transaction)
                .WithRequired(e => e.TransactionType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
