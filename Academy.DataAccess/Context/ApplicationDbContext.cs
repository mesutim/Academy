using Academy.DataAccess.FluentConfig;
using Academy.DataAccess.SeedData;
using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Course DbSet
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> courseCategoryMap { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatus { get; set; }
        public DbSet<UserCourse> UserCourseMap { get; set; }
        #endregion

        #region User DbSet
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissionMap { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoleMap { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Course Related Entities Configs
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new CourseCategoryConfig());
            modelBuilder.ApplyConfiguration(new CourseEpisodeConfig());
            modelBuilder.ApplyConfiguration(new CourseLevelConfig());
            modelBuilder.ApplyConfiguration(new CourseStatusConfig());
            modelBuilder.ApplyConfiguration(new UserCourseConfig());
            #endregion

            #region Identity Related Entities Configs
            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            #endregion


            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
