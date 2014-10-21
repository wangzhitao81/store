using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class LogUpdateSystemMap : EntityTypeConfiguration<LogUpdateSystem>
    {
        public LogUpdateSystemMap()
        {
            // Primary Key
            this.HasKey(t => t.LogUpdateSystemID);

            // Properties
            // Table & Column Mappings
            this.ToTable("LogUpdateSystem", "WebSystem");
            this.Property(t => t.LogUpdateSystemID).HasColumnName("LogUpdateSystemID");
            this.Property(t => t.CreateUserID).HasColumnName("CreateUserID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
