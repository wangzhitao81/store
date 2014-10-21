using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class tb_RoleMap : EntityTypeConfiguration<tb_Role>
    {
        public tb_RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tb_Role", "Permission");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ParentRoleID).HasColumnName("ParentRoleID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");

            // Relationships
            this.HasMany(t => t.tb_User)
                .WithMany(t => t.tb_Role)
                .Map(m =>
                    {
                        m.ToTable("User_Role", "Permission");
                        m.MapLeftKey("RoleID");
                        m.MapRightKey("UserID");
                    });


        }
    }
}
