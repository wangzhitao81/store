using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class GlobalVariableMap : EntityTypeConfiguration<GlobalVariable>
    {
        public GlobalVariableMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Key, t.Value, t.IsEnable });

            // Properties
            this.Property(t => t.Key)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("GlobalVariable", "WebSystem");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
        }
    }
}
