﻿// <auto-generated />
using System;
using Academy.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Academy.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Academy.Model.Models.CourseModels.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseImageName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CoursePrice")
                        .HasColumnType("int");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DemoFileName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("ShortKey")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.HasIndex("LevelId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseCategory", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CourseCategoryMap", (string)null);
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseEpisode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EpisodeId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("EpisodeFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EpisodeTime")
                        .HasColumnType("time");

                    b.Property<string>("EpisodeTitle")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.HasKey("EpisodeId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseEpisodes", (string)null);
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseLevel", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LevelId"));

                    b.Property<string>("LevelTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("LevelId");

                    b.ToTable("CourseLevels", (string)null);

                    b.HasData(
                        new
                        {
                            LevelId = 1,
                            LevelTitle = "مقدماتی"
                        },
                        new
                        {
                            LevelId = 2,
                            LevelTitle = "متوسط"
                        },
                        new
                        {
                            LevelId = 3,
                            LevelTitle = "پیشرفته"
                        },
                        new
                        {
                            LevelId = 4,
                            LevelTitle = "خیلی پیشرفته"
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("StatusId");

                    b.ToTable("CourseStatus", (string)null);

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusTitle = "درحال برگزاری"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusTitle = "اتمام شده"
                        },
                        new
                        {
                            StatusId = 3,
                            StatusTitle = "متوقف شده"
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.UserCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourseMap", (string)null);
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PermissionId");

                    b.HasIndex("ParentID");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            PermissionId = 1,
                            PermissionTitle = "پنل مدیریت"
                        },
                        new
                        {
                            PermissionId = 2,
                            ParentID = 1,
                            PermissionTitle = "مدیریت کاربران"
                        },
                        new
                        {
                            PermissionId = 3,
                            ParentID = 2,
                            PermissionTitle = "لیست کاربر"
                        },
                        new
                        {
                            PermissionId = 4,
                            ParentID = 2,
                            PermissionTitle = "ایجاد کاربر جدید"
                        },
                        new
                        {
                            PermissionId = 5,
                            ParentID = 2,
                            PermissionTitle = "ویرایش کاربر"
                        },
                        new
                        {
                            PermissionId = 6,
                            ParentID = 2,
                            PermissionTitle = "حذف کاربر"
                        },
                        new
                        {
                            PermissionId = 7,
                            ParentID = 1,
                            PermissionTitle = "مدیریت نقش ها"
                        },
                        new
                        {
                            PermissionId = 8,
                            ParentID = 7,
                            PermissionTitle = "لیست نقش ها"
                        },
                        new
                        {
                            PermissionId = 9,
                            ParentID = 7,
                            PermissionTitle = "افزودن نقش جدید"
                        },
                        new
                        {
                            PermissionId = 10,
                            ParentID = 7,
                            PermissionTitle = "ویرایش نقش"
                        },
                        new
                        {
                            PermissionId = 11,
                            ParentID = 7,
                            PermissionTitle = "حذف نقش"
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            IsDelete = false,
                            RoleTitle = "مدیر سایت"
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissionMap", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 11
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ActiveCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAvatar")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            ActiveCode = "1",
                            Email = "admin@gmail.com",
                            IsActive = true,
                            IsDelete = false,
                            Password = "123",
                            RegisterDate = new DateTime(2023, 11, 15, 0, 27, 49, 52, DateTimeKind.Local).AddTicks(4856),
                            UserAvatar = "Default.png",
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleMap", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.Category", b =>
                {
                    b.HasOne("Academy.Model.Models.CourseModels.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.Course", b =>
                {
                    b.HasOne("Academy.Model.Models.CourseModels.CourseLevel", "CourseLevel")
                        .WithMany("Courses")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academy.Model.Models.CourseModels.CourseStatus", "CourseStatus")
                        .WithMany("Courses")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academy.Model.Models.IdentityModels.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId");

                    b.Navigation("CourseLevel");

                    b.Navigation("CourseStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseCategory", b =>
                {
                    b.HasOne("Academy.Model.Models.CourseModels.Category", "Category")
                        .WithMany("CourseCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academy.Model.Models.CourseModels.Course", "Course")
                        .WithMany("CourseCategories")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseEpisode", b =>
                {
                    b.HasOne("Academy.Model.Models.CourseModels.Course", "Course")
                        .WithMany("CourseEpisodes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.UserCourse", b =>
                {
                    b.HasOne("Academy.Model.Models.CourseModels.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academy.Model.Models.IdentityModels.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.Permission", b =>
                {
                    b.HasOne("Academy.Model.Models.IdentityModels.Permission", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentID");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.RolePermission", b =>
                {
                    b.HasOne("Academy.Model.Models.IdentityModels.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academy.Model.Models.IdentityModels.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.UserRole", b =>
                {
                    b.HasOne("Academy.Model.Models.IdentityModels.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Academy.Model.Models.IdentityModels.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("CourseCategories");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.Course", b =>
                {
                    b.Navigation("CourseCategories");

                    b.Navigation("CourseEpisodes");

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseLevel", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Academy.Model.Models.CourseModels.CourseStatus", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.Permission", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Academy.Model.Models.IdentityModels.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("UserCourses");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
