using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class TipMap : EntityTypeConfiguration<Tip>
    {
        public TipMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TipContent)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Tip", "WebSystem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TipContent).HasColumnName("TipContent");
            this.Property(t => t.Triger).HasColumnName("Triger");
            this.Property(t => t.TipType).HasColumnName("TipType");
            this.Property(t => t.DateStart).HasColumnName("DateStart");
            this.Property(t => t.DateEnd).HasColumnName("DateEnd");
            this.Property(t => t.CreateUserID).HasColumnName("CreateUserID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
        }
    }
}
