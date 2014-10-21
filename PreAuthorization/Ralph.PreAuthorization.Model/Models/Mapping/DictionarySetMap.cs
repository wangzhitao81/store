using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class DictionarySetMap : EntityTypeConfiguration<DictionarySet>
    {
        public DictionarySetMap()
        {
            // Primary Key
            this.HasKey(t => t.DictionaryId);

            // Properties
            this.Property(t => t.DictionaryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DictionaryCode)
                .HasMaxLength(20);

            this.Property(t => t.DictionaryName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("DictionarySet");
            this.Property(t => t.DictionaryId).HasColumnName("DictionaryId");
            this.Property(t => t.DictionaryCode).HasColumnName("DictionaryCode");
            this.Property(t => t.DictionaryName).HasColumnName("DictionaryName");
            this.Property(t => t.DictionaryType).HasColumnName("DictionaryType");
        }
    }
}
