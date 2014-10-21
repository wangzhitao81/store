using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class MarketingSaleMap : EntityTypeConfiguration<MarketingSale>
    {
        public MarketingSaleMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingSalesId);

            // Properties
            this.Property(t => t.MarketingSalesId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MarketingSalesName)
                .HasMaxLength(50);

            this.Property(t => t.MarketingSalesPlanId)
                .HasMaxLength(20);

            this.Property(t => t.MarketingSalesEmployeeName)
                .HasMaxLength(20);

            this.Property(t => t.MarketingSalesEmployeeCrmNo)
                .HasMaxLength(20);

            this.Property(t => t.BusinessHallName)
                .HasMaxLength(50);

            this.Property(t => t.BusinessHallOrgId)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("MarketingSales");
            this.Property(t => t.MarketingSalesId).HasColumnName("MarketingSalesId");
            this.Property(t => t.MarketingSalesName).HasColumnName("MarketingSalesName");
            this.Property(t => t.MarketingSalesPlanId).HasColumnName("MarketingSalesPlanId");
            this.Property(t => t.MarketingSalesDealTime).HasColumnName("MarketingSalesDealTime");
            this.Property(t => t.MarketingSalesEmployeeName).HasColumnName("MarketingSalesEmployeeName");
            this.Property(t => t.MarketingSalesEmployeeCrmNo).HasColumnName("MarketingSalesEmployeeCrmNo");
            this.Property(t => t.BusinessHallName).HasColumnName("BusinessHallName");
            this.Property(t => t.BusinessHallOrgId).HasColumnName("BusinessHallOrgId");
        }
    }
}
