using Academy.DataAccess.FluentConfig;
using Academy.DataAccess.SeedData;
using Academy.Model.Models.CourseModels;
using Academy.Model.Models.IdentityModels;
using Academy.Model.Models.OrderModels;
using Academy.Model.Models.SiteVisitModels;
using Academy.Model.Models.TicketModels;
using Academy.Model.Models.TransactionModels;
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
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }
        #endregion

        #region Identity DbSet
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissionMap { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoleMap { get; set; }
        #endregion

        #region Order DbSet
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }
        #endregion

        #region SiteSetting DbSet
        public DbSet<SiteVisit> SiteVisits { get; set; }
        #endregion

        #region Ticket DbSet
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        #endregion

        #region Transaction DbSet
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
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
            modelBuilder.ApplyConfiguration(new CourseCommentConfig());
            modelBuilder.ApplyConfiguration(new CourseVoteConfig());
            #endregion

            #region Identity Related Entities Configs
            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new RolePermissionConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            #endregion

            #region Orser Related Entities Configs
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new OrderDetailConfig());
            modelBuilder.ApplyConfiguration(new DiscountConfig());
            modelBuilder.ApplyConfiguration(new UserDiscountCodeConfig());
            #endregion

            #region SiteSetting Related Entities Configs
            modelBuilder.ApplyConfiguration(new SiteVisitConfig());
            #endregion

            #region Ticket Related Entities Configs
            modelBuilder.ApplyConfiguration(new QuestionConfig());
            modelBuilder.ApplyConfiguration(new AnswerConfig());
            #endregion

            #region Transaction Related Entities Configs
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfig());
            #endregion

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
