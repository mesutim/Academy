using Academy.Model.Models.OrderModels;
using Academy.Model.Models.SiteVisitModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.DataAccess.FluentConfig
{
    public class SiteVisitConfig : IEntityTypeConfiguration<SiteVisit>
    {
        public void Configure(EntityTypeBuilder<SiteVisit> modelBuilder)
        {
            //primary key
            modelBuilder.HasKey(u => u.VisitID);

            //Other Validations
            modelBuilder.Property(s=>s.IP).HasMaxLength(50);
        }
    }
}
  
