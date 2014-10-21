using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class GridColumnMap : EntityTypeConfiguration<GridColumn>
    {
        public GridColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CName)
                .HasMaxLength(50);

            this.Property(t => t.EName)
                .HasMaxLength(50);

            this.Property(t => t.Data)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Align)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.RenderProcess)
                .HasMaxLength(50);

            this.Property(t => t.QueryProcess)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GridColumn", "Templet");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CName).HasColumnName("CName");
            this.Property(t => t.EName).HasColumnName("EName");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.SortAble).HasColumnName("SortAble");
            this.Property(t => t.QueryAble).HasColumnName("QueryAble");
            this.Property(t => t.Align).HasColumnName("Align");
            this.Property(t => t.Show).HasColumnName("Show");
            this.Property(t => t.FilterAble).HasColumnName("FilterAble");
            this.Property(t => t.IsCalculation).HasColumnName("IsCalculation");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.Editable).HasColumnName("Editable");
            this.Property(t => t.RenderProcess).HasColumnName("RenderProcess");
            this.Property(t => t.QueryProcess).HasColumnName("QueryProcess");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.TID).HasColumnName("TID");

            // Relationships
            this.HasRequired(t => t.GridTable)
                .WithMany(t => t.GridColumns)
                .HasForeignKey(d => d.TID);

        }
    }
}
