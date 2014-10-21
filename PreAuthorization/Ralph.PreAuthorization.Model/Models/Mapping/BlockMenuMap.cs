using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class BlockMenuMap : EntityTypeConfiguration<BlockMenu>
    {
        public BlockMenuMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.ImagePath)
                .HasMaxLength(100);

            this.Property(t => t.UrlPath)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("BlockMenu", "WebSystem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.UrlPath).HasColumnName("UrlPath");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsShow).HasColumnName("IsShow");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.OrderIndex).HasColumnName("OrderIndex");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
        }
    }
}
