using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class User_TipMap : EntityTypeConfiguration<User_Tip>
    {
        public User_TipMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserID, t.TipID });

            // Properties
            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("User_Tip", "WebSystem");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.TipID).HasColumnName("TipID");

            // Relationships
            this.HasRequired(t => t.Tip)
                .WithMany(t => t.User_Tip)
                .HasForeignKey(d => d.TipID);

        }
    }
}
