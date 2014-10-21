using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class Table_ButtonMap : EntityTypeConfiguration<Table_Button>
    {
        public Table_ButtonMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TID, t.BID });

            // Properties
            this.Property(t => t.TID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OnPress)
                .HasMaxLength(50);

            this.Property(t => t.Expression)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("Table_Button", "Templet");
            this.Property(t => t.TID).HasColumnName("TID");
            this.Property(t => t.BID).HasColumnName("BID");
            this.Property(t => t.OnPress).HasColumnName("OnPress");
            this.Property(t => t.Expression).HasColumnName("Expression");

            // Relationships
            this.HasRequired(t => t.GridButton)
                .WithMany(t => t.Table_Button)
                .HasForeignKey(d => d.BID);
            this.HasRequired(t => t.GridTable)
                .WithMany(t => t.Table_Button)
                .HasForeignKey(d => d.TID);

        }
    }
}
