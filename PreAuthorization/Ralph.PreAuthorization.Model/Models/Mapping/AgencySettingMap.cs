using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class AgencySettingMap : EntityTypeConfiguration<AgencySetting>
    {
        public AgencySettingMap()
        {
            // Primary Key
            this.HasKey(t => t.AgencySettingsId);

            // Properties
            this.Property(t => t.AgencySettingsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CommunicationKey)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("AgencySettings");
            this.Property(t => t.AgencySettingsId).HasColumnName("AgencySettingsId");
            this.Property(t => t.CommunicationKey).HasColumnName("CommunicationKey");
        }
    }
}
