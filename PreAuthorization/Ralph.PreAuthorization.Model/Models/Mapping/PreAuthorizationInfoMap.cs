using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ralph.PreAuthorization.Model.Models.Mapping
{
    public class PreAuthorizationInfoMap : EntityTypeConfiguration<PreAuthorizationInfo>
    {
        public PreAuthorizationInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.PreAuthorizationInfoId);

            // Properties
            this.Property(t => t.PreAuthorizationInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActivityTypeCode)
                .HasMaxLength(20);

            this.Property(t => t.ActivityLevelCode)
                .HasMaxLength(20);

            this.Property(t => t.TelNumber)
                .HasMaxLength(20);

            this.Property(t => t.CustomerName)
                .HasMaxLength(50);

            this.Property(t => t.CustomerIdNo)
                .HasMaxLength(50);

            this.Property(t => t.BankCardTypeCode)
                .HasMaxLength(20);

            this.Property(t => t.BankCardNo)
                .HasMaxLength(20);

            this.Property(t => t.BankCardCVN)
                .HasMaxLength(10);

            this.Property(t => t.MarketingActivitySerialNum)
                .HasMaxLength(20);

            this.Property(t => t.OperUserCode)
                .HasMaxLength(20);

            this.Property(t => t.StateCode)
                .HasMaxLength(20);

            this.Property(t => t.PreAuthorizationResult)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("PreAuthorizationInfo");
            this.Property(t => t.PreAuthorizationInfoId).HasColumnName("PreAuthorizationInfoId");
            this.Property(t => t.ActivityTypeCode).HasColumnName("ActivityTypeCode");
            this.Property(t => t.ActivityLevelCode).HasColumnName("ActivityLevelCode");
            this.Property(t => t.TelNumber).HasColumnName("TelNumber");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.CustomerIdNo).HasColumnName("CustomerIdNo");
            this.Property(t => t.BankCardTypeCode).HasColumnName("BankCardTypeCode");
            this.Property(t => t.BankCardNo).HasColumnName("BankCardNo");
            this.Property(t => t.BankCardCVN).HasColumnName("BankCardCVN");
            this.Property(t => t.GuaranteeAmount).HasColumnName("GuaranteeAmount");
            this.Property(t => t.MarketingActivitySerialNum).HasColumnName("MarketingActivitySerialNum");
            this.Property(t => t.BuyPhoneDate).HasColumnName("BuyPhoneDate");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.AgreementDate).HasColumnName("AgreementDate");
            this.Property(t => t.OperUserCode).HasColumnName("OperUserCode");
            this.Property(t => t.OperTime).HasColumnName("OperTime");
            this.Property(t => t.StateCode).HasColumnName("StateCode");
            this.Property(t => t.PreAuthorizationResult).HasColumnName("PreAuthorizationResult");
        }
    }
}
