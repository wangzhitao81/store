using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class OperLogMap : EntityTypeConfiguration<OperLog>
    {
        public OperLogMap()
        {
            // Primary Key
            this.HasKey(t => t.OperLogId);

            // Properties
            this.Property(t => t.OperLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperType)
                .HasMaxLength(20);

            this.Property(t => t.OperUserCode)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("OperLog");
            this.Property(t => t.OperLogId).HasColumnName("OperLogId");
            this.Property(t => t.PreAuthorizationInfoId).HasColumnName("PreAuthorizationInfoId");
            this.Property(t => t.OperType).HasColumnName("OperType");
            this.Property(t => t.OperUserCode).HasColumnName("OperUserCode");
            this.Property(t => t.OperTime).HasColumnName("OperTime");
        }
    }
}
