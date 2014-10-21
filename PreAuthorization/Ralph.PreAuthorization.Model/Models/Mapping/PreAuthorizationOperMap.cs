using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class PreAuthorizationOperMap : EntityTypeConfiguration<PreAuthorizationOper>
    {
        public PreAuthorizationOperMap()
        {
            // Primary Key
            this.HasKey(t => t.PreAuthorizationOperId);

            // Properties
            this.Property(t => t.PreAuthorizationOperId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperUserCode)
                .HasMaxLength(20);

            this.Property(t => t.OperTypeCode)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("PreAuthorizationOper");
            this.Property(t => t.PreAuthorizationOperId).HasColumnName("PreAuthorizationOperId");
            this.Property(t => t.PreAuthorizationInfoId).HasColumnName("PreAuthorizationInfoId");
            this.Property(t => t.OperUserCode).HasColumnName("OperUserCode");
            this.Property(t => t.OperTime).HasColumnName("OperTime");
            this.Property(t => t.OperTypeCode).HasColumnName("OperTypeCode");
        }
    }
}
