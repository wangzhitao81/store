using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class BlockMessageMap : EntityTypeConfiguration<BlockMessage>
    {
        public BlockMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.F_Key)
                .HasMaxLength(500);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.MsgContent)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Type)
                .HasMaxLength(10);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BlockMessage", "WebSystem");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.F_Key).HasColumnName("F_Key");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.MsgContent).HasColumnName("MsgContent");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.DataState).HasColumnName("DataState");
            this.Property(t => t.CreateUserID).HasColumnName("CreateUserID");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.IsEnable).HasColumnName("IsEnable");
        }
    }
}
