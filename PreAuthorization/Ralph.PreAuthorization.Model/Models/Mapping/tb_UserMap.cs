using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class tb_UserMap : EntityTypeConfiguration<tb_User>
    {
        public tb_UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.LoginName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.ChinaName)
                .HasMaxLength(20);

            this.Property(t => t.EnglishName)
                .HasMaxLength(30);

            this.Property(t => t.Gender)
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tb_User", "Permission");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.LoginName).HasColumnName("LoginName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.ChinaName).HasColumnName("ChinaName");
            this.Property(t => t.EnglishName).HasColumnName("EnglishName");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Used).HasColumnName("Used");
            this.Property(t => t.UpdateUser).HasColumnName("UpdateUser");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
        }
    }
}
