using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
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
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.PermissionId);


            //other validations
            modelBuilder.Property(u => u.PermissionTitle).IsRequired();
            modelBuilder.Property(u => u.PermissionTitle).HasMaxLength(200);


            // Relations
            //modelBuilder.HasOne(s => s.Parent).WithMany(m => m.Children)
            //    .HasForeignKey(e => e.ParentID);
        }
    }

    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.RoleId);


            //other validations
            modelBuilder.HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Property(u => u.RoleTitle).IsRequired();
            modelBuilder.Property(u => u.RoleTitle).HasMaxLength(200);
        }
    }

    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> modelBuilder)
        {

            //name of table
            modelBuilder.ToTable("RolePermissionMap");

            //primary key
            modelBuilder.HasKey(u => new { u.RoleId, u.PermissionId });

            //relations
            modelBuilder.HasOne(b => b.Role).WithMany(b => b.RolePermissions)
                .HasForeignKey(u => u.RoleId);
            modelBuilder.HasOne(b => b.Permission).WithMany(b => b.RolePermissions)
                .HasForeignKey(u => u.PermissionId);
        }
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.UserId);


            //other validations
            modelBuilder.HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Property(u => u.UserName).IsRequired();
            modelBuilder.Property(u => u.UserName).HasMaxLength(200);

            modelBuilder.Property(u => u.Email).IsRequired();
            modelBuilder.Property(u => u.Email).HasMaxLength(200);

            modelBuilder.Property(u => u.Password).IsRequired();
            modelBuilder.Property(u => u.Password).HasMaxLength(200);

            modelBuilder.Property(u => u.ActiveCode).HasMaxLength(50);

            modelBuilder.Property(u => u.UserAvatar).HasMaxLength(200);

        }
    }

    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> modelBuilder)
        {

            //name of table
            modelBuilder.ToTable("UserRoleMap");

            //primary key
            modelBuilder.HasKey(u => new { u.RoleId, u.UserId });

            //relations
            modelBuilder.HasOne(b => b.Role).WithMany(b => b.UserRoles)
                .HasForeignKey(u => u.RoleId);
            modelBuilder.HasOne(b => b.User).WithMany(b => b.UserRoles)
                .HasForeignKey(u => u.UserId);
        }
    }
}