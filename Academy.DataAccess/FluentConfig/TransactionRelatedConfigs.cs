using Academy.Model.Models.TransactionModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Academy.DataAccess.FluentConfig
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.TransactionId);

            //Other Validations
            modelBuilder.Property(s=>s.Description).HasMaxLength(400);

            //Relations
            modelBuilder.HasOne(s => s.User).WithMany(m => m.Transactions)
                .HasForeignKey(e => e.UserId);
            modelBuilder.HasOne(s => s.TransactionType).WithMany(m => m.Transactions)
                .HasForeignKey(e => e.TypeId);
        }
    }

    public class TransactionTypeConfig : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.TransactionTypeId);

            //Other Validations
            modelBuilder.Property(t=>t.TransactionTypeTitle).HasMaxLength(150);
        }
    }
}