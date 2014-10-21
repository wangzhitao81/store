using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class tb_UserDataFilterMap : EntityTypeConfiguration<tb_UserDataFilter>
    {
        public tb_UserDataFilterMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DataSourceName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.FilterString)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("tb_UserDataFilter", "Permission");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.F_UserID).HasColumnName("F_UserID");
            this.Property(t => t.F_RoleID).HasColumnName("F_RoleID");
            this.Property(t => t.DataSourceName).HasColumnName("DataSourceName");
            this.Property(t => t.FilterString).HasColumnName("FilterString");

            // Relationships
            this.HasRequired(t => t.tb_Role)
                .WithMany(t => t.tb_UserDataFilter)
                .HasForeignKey(d => d.F_RoleID);

        }
    }
}
