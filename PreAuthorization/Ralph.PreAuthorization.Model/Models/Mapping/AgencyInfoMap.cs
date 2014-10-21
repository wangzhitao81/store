using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class AgencyInfoMap : EntityTypeConfiguration<AgencyInfo>
    {
        public AgencyInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.AgencyInfoId);

            // Properties
            this.Property(t => t.AgencyInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AgencyCode)
                .HasMaxLength(20);

            this.Property(t => t.AgencyName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AgencyInfo");
            this.Property(t => t.AgencyInfoId).HasColumnName("AgencyInfoId");
            this.Property(t => t.AgencyCode).HasColumnName("AgencyCode");
            this.Property(t => t.AgencyName).HasColumnName("AgencyName");
        }
    }
}
