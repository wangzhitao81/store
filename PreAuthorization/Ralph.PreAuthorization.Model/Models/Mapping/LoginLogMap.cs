using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class LoginLogMap : EntityTypeConfiguration<LoginLog>
    {
        public LoginLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.IP)
                .HasMaxLength(20);

            this.Property(t => t.Browser)
                .HasMaxLength(20);

            this.Property(t => t.Version)
                .HasMaxLength(20);

            this.Property(t => t.Platform)
                .HasMaxLength(20);

            this.Property(t => t.ComputerName)
                .HasMaxLength(20);

            this.Property(t => t.RequestContent)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("LoginLog", "WebSystem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.Browser).HasColumnName("Browser");
            this.Property(t => t.Version).HasColumnName("Version");
            this.Property(t => t.Platform).HasColumnName("Platform");
            this.Property(t => t.ComputerName).HasColumnName("ComputerName");
            this.Property(t => t.RequestContent).HasColumnName("RequestContent");
            this.Property(t => t.CreateUserID).HasColumnName("CreateUserID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
