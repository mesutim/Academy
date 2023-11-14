using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.FluentConfig
{ 
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.CategoryId);

            //other validations
            modelBuilder.HasQueryFilter(g => !g.IsDelete);

            modelBuilder.Property(u => u.CategoryTitle).IsRequired();
            modelBuilder.Property(u => u.CategoryTitle).HasMaxLength(200);

            // Relations
            //modelBuilder.HasOne(s => s.Parent).WithMany(m => m.Children)
            //    .HasForeignKey(e => e.ParentId);
        }
    }

    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.CourseId);


            //other validations
            modelBuilder.HasQueryFilter(c => !c.IsDelete);

            modelBuilder.Property(u => u.CourseTitle).IsRequired();
            modelBuilder.Property(u => u.CourseTitle).HasMaxLength(450);

            modelBuilder.Property(u => u.CourseDescription).IsRequired();
            
            modelBuilder.Property(u => u.CoursePrice).IsRequired();

            modelBuilder.Property(u => u.Tags).HasMaxLength(600);
            
            modelBuilder.Property(u => u.CourseImageName).HasMaxLength(50);
            
            modelBuilder.Property(u => u.DemoFileName).HasMaxLength(100);

            modelBuilder.Property(u => u.CreateDate).IsRequired();

            modelBuilder.Property(u => u.ShortKey).HasMaxLength(5);

            //relations
            //modelBuilder.HasOne(b => b.User).WithMany(b => b.Courses)
            //    .HasForeignKey(u => u.TeacherId);
            modelBuilder.HasOne(b => b.CourseStatus).WithMany(b => b.Courses)
                .HasForeignKey(u => u.StatusId);
            modelBuilder.HasOne(b => b.CourseLevel).WithMany(b => b.Courses)
                .HasForeignKey(u => u.LevelId);
        }
    }

    public class CourseCategoryConfig : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> modelBuilder)
        {
            //name of table
            modelBuilder.ToTable("CourseCategoryMap");

            //primary key
            modelBuilder.HasKey(u=> new { u.CourseId, u.CategoryId });

            //relations
            modelBuilder.HasOne(b => b.Course).WithMany(b => b.CourseCategories)
                .HasForeignKey(u => u.CourseId);
            modelBuilder.HasOne(b => b.Category).WithMany(b => b.CourseCategories)
                .HasForeignKey(u => u.CategoryId);
        }
    }

    public class CourseEpisodeConfig : IEntityTypeConfiguration<CourseEpisode>
    {
        public void Configure(EntityTypeBuilder<CourseEpisode> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.EpisodeId);


            //other validations
            modelBuilder.Property(u => u.EpisodeTitle).IsRequired();
            modelBuilder.Property(u => u.EpisodeTitle).HasMaxLength(400);

            modelBuilder.Property(u => u.EpisodeTime).IsRequired();

            //relations
            modelBuilder.HasOne(b => b.Course).WithMany(b => b.CourseEpisodes)
                .HasForeignKey(u => u.CourseId);
        }
    }

    public class CourseLevelConfig : IEntityTypeConfiguration<CourseLevel>
    {
        public void Configure(EntityTypeBuilder<CourseLevel> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.LevelId);

            //other validations
            modelBuilder.Property(u => u.LevelTitle).IsRequired();
            modelBuilder.Property(u => u.LevelTitle).HasMaxLength(150);
        }
    }

    public class CourseStatusConfig : IEntityTypeConfiguration<CourseStatus>
    {
        public void Configure(EntityTypeBuilder<CourseStatus> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.StatusId);

            //other validations
            modelBuilder.Property(u => u.StatusTitle).IsRequired();
            modelBuilder.Property(u => u.StatusTitle).HasMaxLength(150);
        }
    }

    public class UserCourseConfig : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> modelBuilder)
        {
            //name of table
            modelBuilder.ToTable("UserCourseMap");

            //primary key
            modelBuilder.HasKey(u => new { u.CourseId, u.UserId });

            //relations
            modelBuilder.HasOne(b => b.Course).WithMany(b => b.UserCourses)
                .HasForeignKey(u => u.CourseId);
            modelBuilder.HasOne(b => b.User).WithMany(b => b.UserCourses)
                .HasForeignKey(u => u.UserId);
        }
    }
}

