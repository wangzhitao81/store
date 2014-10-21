using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class tb_PageGridDataFilterMap : EntityTypeConfiguration<tb_PageGridDataFilter>
    {
        public tb_PageGridDataFilterMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.PageGridName)
                .HasMaxLength(50);

            this.Property(t => t.PageGridSql)
                .HasMaxLength(500);

            this.Property(t => t.PageGridSqlFilter)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("tb_PageGridDataFilter", "Permission");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.PageGridName).HasColumnName("PageGridName");
            this.Property(t => t.PageGridSql).HasColumnName("PageGridSql");
            this.Property(t => t.PageGridSqlFilter).HasColumnName("PageGridSqlFilter");

            // Relationships
            this.HasRequired(t => t.tb_Role)
                .WithMany(t => t.tb_PageGridDataFilter)
                .HasForeignKey(d => d.RoleID);

        }
    }
}
