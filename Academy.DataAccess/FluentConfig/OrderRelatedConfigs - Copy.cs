using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
using Academy.Model.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Academy.DataAccess.FluentConfig
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.OrderId);


            //Relations
            modelBuilder.HasOne(s => s.User).WithMany(m => m.Orders)
                .HasForeignKey(e => e.UserId);
        }
    }

    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.DetailId);


            //Relations
            modelBuilder.HasOne(s => s.Order).WithMany(m => m.OrderDetails)
                .HasForeignKey(e => e.OrderId);
            modelBuilder.HasOne(s => s.Course).WithMany(m => m.OrderDetails)
                .HasForeignKey(e => e.CourseId);
        }
    }

    public class DiscountConfig : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.DiscountId);

            //other validations
            modelBuilder.Property(u => u.DiscountCode).HasMaxLength(150);
        }
    }

    public class UserDiscountCodeConfig : IEntityTypeConfiguration<UserDiscountCode>
    {
        public void Configure(EntityTypeBuilder<UserDiscountCode> modelBuilder)
        {
            //name of table
            modelBuilder.ToTable("UserDiscountMap");

            //primary key
            modelBuilder.HasKey(u => new { u.DiscountId, u.UserId });

            //relations
            modelBuilder.HasOne(b => b.Discount).WithMany(b => b.UserDiscountCodes)
                .HasForeignKey(u => u.DiscountId);
            modelBuilder.HasOne(b => b.User).WithMany(b => b.UserDiscountCodes)
                .HasForeignKey(u => u.UserId);
        }
    }
}
  
