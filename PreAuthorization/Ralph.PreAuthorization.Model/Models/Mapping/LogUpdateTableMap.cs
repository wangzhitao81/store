using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class LogUpdateTableMap : EntityTypeConfiguration<LogUpdateTable>
    {
        public LogUpdateTableMap()
        {
            // Primary Key
            this.HasKey(t => t.LogUpdateTableID);

            // Properties
            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TableBackupName)
                .HasMaxLength(50);

            this.Property(t => t.UploadFileName)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("LogUpdateTable", "WebSystem");
            this.Property(t => t.LogUpdateTableID).HasColumnName("LogUpdateTableID");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.TableBackupName).HasColumnName("TableBackupName");
            this.Property(t => t.UploadFileName).HasColumnName("UploadFileName");
            this.Property(t => t.CreateUserID).HasColumnName("CreateUserID");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
