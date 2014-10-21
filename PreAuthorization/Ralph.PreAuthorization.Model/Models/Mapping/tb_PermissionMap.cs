using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class tb_PermissionMap : EntityTypeConfiguration<tb_Permission>
    {
        public tb_PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tb_Permission", "Permission");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PermissionsMatch).HasColumnName("PermissionsMatch");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");

            // Relationships
            this.HasMany(t => t.tb_Role)
                .WithMany(t => t.tb_Permission)
                .Map(m =>
                    {
                        m.ToTable("Role_Permission", "Permission");
                        m.MapLeftKey("PermissionID");
                        m.MapRightKey("RoleID");
                    });


        }
    }
}
