using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class AgencyEmployeeMap : EntityTypeConfiguration<AgencyEmployee>
    {
        public AgencyEmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.AgencyEmployeeId);

            // Properties
            this.Property(t => t.AgencyEmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AgencyEmployeeCode)
                .HasMaxLength(20);

            this.Property(t => t.AgencyEmployeeName)
                .HasMaxLength(50);

            this.Property(t => t.AgencyEmployeeImsNo)
                .HasMaxLength(20);

            this.Property(t => t.BelongedBusinessHallName)
                .HasMaxLength(50);

            this.Property(t => t.BelongedBusinessHallOrgId)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("AgencyEmployee");
            this.Property(t => t.AgencyEmployeeId).HasColumnName("AgencyEmployeeId");
            this.Property(t => t.AgencyEmployeeCode).HasColumnName("AgencyEmployeeCode");
            this.Property(t => t.AgencyEmployeeName).HasColumnName("AgencyEmployeeName");
            this.Property(t => t.AgencyEmployeeImsNo).HasColumnName("AgencyEmployeeImsNo");
            this.Property(t => t.BelongedBusinessHallName).HasColumnName("BelongedBusinessHallName");
            this.Property(t => t.BelongedBusinessHallOrgId).HasColumnName("BelongedBusinessHallOrgId");
        }
    }
}
