using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class System_TableMap : EntityTypeConfiguration<System_Table>
    {
        public System_TableMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LogUpdateSystemID, t.LogUpdateTableID });

            // Properties
            this.Property(t => t.LogUpdateSystemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LogUpdateTableID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("System_Table", "WebSystem");
            this.Property(t => t.LogUpdateSystemID).HasColumnName("LogUpdateSystemID");
            this.Property(t => t.LogUpdateTableID).HasColumnName("LogUpdateTableID");
        }
    }
}
