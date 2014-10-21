using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class SystemInfoMap : EntityTypeConfiguration<SystemInfo>
    {
        public SystemInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SystemName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SystemPassword)
                .HasMaxLength(50);

            this.Property(t => t.CompanyName)
                .HasMaxLength(50);

            this.Property(t => t.Developer)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SystemInfo", "WebSystem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.SystemName).HasColumnName("SystemName");
            this.Property(t => t.SystemPassword).HasColumnName("SystemPassword");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.Developer).HasColumnName("Developer");
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.ReleasesDate).HasColumnName("ReleasesDate");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
        }
    }
}
