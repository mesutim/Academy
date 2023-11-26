using Academy.Model.Models.OrderModels;
using Academy.Model.Models.SiteVisitModels;
using Academy.Model.Models.TicketModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Academy.DataAccess.FluentConfig
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.QuestionId);

            //Other Validations
            modelBuilder.Property(s=>s.Title).HasMaxLength(400);

            //Relations
            modelBuilder.HasOne(s => s.User).WithMany(m => m.Questions)
                .HasForeignKey(e => e.UserId);
            modelBuilder.HasOne(s => s.Course).WithMany(m => m.Questions)
                .HasForeignKey(e => e.CourseId);
        }
    }

    public class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.AnswerId);

            //Relations
            modelBuilder.HasOne(s => s.Question).WithMany(m => m.Answers)
                .HasForeignKey(e => e.QuestionId);
            //modelBuilder.HasOne(s => s.User).WithMany(m => m.Answers)
            //    .HasForeignKey(e => e.UserId);
        }
    }
}