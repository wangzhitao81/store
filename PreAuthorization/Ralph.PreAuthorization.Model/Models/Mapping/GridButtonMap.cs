using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class GridButtonMap : EntityTypeConfiguration<GridButton>
    {
        public GridButtonMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.EName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CName)
                .HasMaxLength(50);

            this.Property(t => t.Css)
                .HasMaxLength(50);

            this.Property(t => t.Icon)
                .HasMaxLength(100);

            this.Property(t => t.OnPress)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GridButton", "Templet");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.EName).HasColumnName("EName");
            this.Property(t => t.CName).HasColumnName("CName");
            this.Property(t => t.Css).HasColumnName("Css");
            this.Property(t => t.Icon).HasColumnName("Icon");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.OnPress).HasColumnName("OnPress");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
