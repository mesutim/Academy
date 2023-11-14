using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Academy.Model.Models.IdentityModels;
using Academy.Model.Models.CourseModels;

namespace Academy.DataAccess.SeedData
{
    public static class DataSeeding
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region User
            //Seed Admin User
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Email = "admin@gmail.com",
                UserName = "Admin",
                UserAvatar = "Default.png",
                RegisterDate = DateTime.Now,
                ActiveCode = "1",
                IsActive = true,
                Password = "123"
            });
            #endregion

            #region Role
            //Seed Admin Role
            var adminRole = new Role
            {
                RoleId = 1,
                RoleTitle = "مدیر سایت",
            };
            modelBuilder.Entity<Role>().HasData(adminRole);
            //UserRole
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                RoleId = adminRole.RoleId,
                UserId = 1
            });
            #endregion

            #region Permission
            //Permissions
            modelBuilder.Entity<Permission>().HasData(PermissionsList.Permissions);

            //RolePermission
            modelBuilder.Entity<RolePermission>().HasData(
                PermissionsList.Permissions.Select(permission => new RolePermission
                {
                    PermissionId = permission.PermissionId,
                    RoleId = adminRole.RoleId,
                })
            );
            #endregion

            #region Course
            //Seed Data to Course Level
            var courseLevelList = new List<CourseLevel>()
            {
                new CourseLevel()
                {
                    LevelId = 1,
                    LevelTitle = "مقدماتی"
                },
                new CourseLevel()
                {
                    LevelId = 2,
                    LevelTitle = "متوسط"
                },
                new CourseLevel()
                {
                    LevelId = 3,
                    LevelTitle = "پیشرفته"
                },
                new CourseLevel()
                {
                    LevelId = 4,
                    LevelTitle = "خیلی پیشرفته"
                },
            };
            modelBuilder.Entity<CourseLevel>().HasData(courseLevelList);
            //Seed Data to Course Status
            var courseStatusList = new List<CourseStatus>()
            {
                new CourseStatus()
                {
                    StatusId = 1,
                    StatusTitle = "درحال برگزاری"
                },
                new CourseStatus()
                {
                    StatusId = 2,
                    StatusTitle = "اتمام شده"
                },
                new CourseStatus()
                {
                    StatusId = 3,
                    StatusTitle = "متوقف شده"
                }
            };
            modelBuilder.Entity<CourseStatus>().HasData(courseStatusList);
            #endregion
        }
    }
}

