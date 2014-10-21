using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class GridTableMap : EntityTypeConfiguration<GridTable>
    {
        public GridTableMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.InvokePage)
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.SqlExpression)
                .HasMaxLength(2000);

            this.Property(t => t.TableExpression)
                .HasMaxLength(1000);

            this.Property(t => t.Url)
                .HasMaxLength(100);

            this.Property(t => t.Tag)
                .HasMaxLength(50);

            this.Property(t => t.SortName)
                .HasMaxLength(50);

            this.Property(t => t.OnSuccess)
                .HasMaxLength(100);

            this.Property(t => t.OnSubmit)
                .HasMaxLength(100);

            this.Property(t => t.OnError)
                .HasMaxLength(100);

            this.Property(t => t.KeyExpression)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("GridTable", "Templet");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.InvokePage).HasColumnName("InvokePage");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.SqlExpression).HasColumnName("SqlExpression");
            this.Property(t => t.TableExpression).HasColumnName("TableExpression");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.SortName).HasColumnName("SortName");
            this.Property(t => t.PageSize).HasColumnName("PageSize");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.UsePager).HasColumnName("UsePager");
            this.Property(t => t.UseRP).HasColumnName("UseRP");
            this.Property(t => t.UseCalculate).HasColumnName("UseCalculate");
            this.Property(t => t.UseQuery).HasColumnName("UseQuery");
            this.Property(t => t.UseWrap).HasColumnName("UseWrap");
            this.Property(t => t.UseAutoLoad).HasColumnName("UseAutoLoad");
            this.Property(t => t.OnSuccess).HasColumnName("OnSuccess");
            this.Property(t => t.OnSubmit).HasColumnName("OnSubmit");
            this.Property(t => t.OnError).HasColumnName("OnError");
            this.Property(t => t.KeyExpression).HasColumnName("KeyExpression");
            this.Property(t => t.ShowCheckBox).HasColumnName("ShowCheckBox");
            this.Property(t => t.ShowToggleBtn).HasColumnName("ShowToggleBtn");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
