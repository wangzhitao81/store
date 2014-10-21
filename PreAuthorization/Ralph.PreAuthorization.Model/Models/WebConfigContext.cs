using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Ralph.PreAuthorization.Model.Models.Mapping;

namespace Ralph.PreAuthorization.Model.Models
{
    public partial class WebConfigContext : DbContext
    {
        static WebConfigContext()
        {
            Database.SetInitializer<WebConfigContext>(null);
        }

        public WebConfigContext()
            : base("Name=WebConfigContext")
        {
        }

        public DbSet<AgencyEmployee> AgencyEmployees { get; set; }
        public DbSet<AgencyInfo> AgencyInfoes { get; set; }
        public DbSet<AgencySetting> AgencySettings { get; set; }
        public DbSet<DictionarySet> DictionarySets { get; set; }
        public DbSet<MarketingSale> MarketingSales { get; set; }
        public DbSet<OperLog> OperLogs { get; set; }
        public DbSet<PreAuthorizationInfo> PreAuthorizationInfoes { get; set; }
        public DbSet<PreAuthorizationOper> PreAuthorizationOpers { get; set; }
        public DbSet<tb_PageGridDataFilter> tb_PageGridDataFilter { get; set; }
        public DbSet<tb_Permission> tb_Permission { get; set; }
        public DbSet<tb_Role> tb_Role { get; set; }
        public DbSet<tb_User> tb_User { get; set; }
        public DbSet<tb_UserDataFilter> tb_UserDataFilter { get; set; }
        public DbSet<GridButton> GridButtons { get; set; }
        public DbSet<GridColumn> GridColumns { get; set; }
        public DbSet<GridTable> GridTables { get; set; }
        public DbSet<Table_Button> Table_Button { get; set; }
        public DbSet<BlockMenu> BlockMenus { get; set; }
        public DbSet<BlockMessage> BlockMessages { get; set; }
        public DbSet<GlobalVariable> GlobalVariables { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<LogUpdateSystem> LogUpdateSystems { get; set; }
        public DbSet<LogUpdateTable> LogUpdateTables { get; set; }
        public DbSet<System_Table> System_Table { get; set; }
        public DbSet<SystemInfo> SystemInfoes { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<User_Tip> User_Tip { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AgencyEmployeeMap());
            modelBuilder.Configurations.Add(new AgencyInfoMap());
            modelBuilder.Configurations.Add(new AgencySettingMap());
            modelBuilder.Configurations.Add(new DictionarySetMap());
            modelBuilder.Configurations.Add(new MarketingSaleMap());
            modelBuilder.Configurations.Add(new OperLogMap());
            modelBuilder.Configurations.Add(new PreAuthorizationInfoMap());
            modelBuilder.Configurations.Add(new PreAuthorizationOperMap());
            modelBuilder.Configurations.Add(new tb_PageGridDataFilterMap());
            modelBuilder.Configurations.Add(new tb_PermissionMap());
            modelBuilder.Configurations.Add(new tb_RoleMap());
            modelBuilder.Configurations.Add(new tb_UserMap());
            modelBuilder.Configurations.Add(new tb_UserDataFilterMap());
            modelBuilder.Configurations.Add(new GridButtonMap());
            modelBuilder.Configurations.Add(new GridColumnMap());
            modelBuilder.Configurations.Add(new GridTableMap());
            modelBuilder.Configurations.Add(new Table_ButtonMap());
            modelBuilder.Configurations.Add(new BlockMenuMap());
            modelBuilder.Configurations.Add(new BlockMessageMap());
            modelBuilder.Configurations.Add(new GlobalVariableMap());
            modelBuilder.Configurations.Add(new LoginLogMap());
            modelBuilder.Configurations.Add(new LogUpdateSystemMap());
            modelBuilder.Configurations.Add(new LogUpdateTableMap());
            modelBuilder.Configurations.Add(new System_TableMap());
            modelBuilder.Configurations.Add(new SystemInfoMap());
            modelBuilder.Configurations.Add(new TipMap());
            modelBuilder.Configurations.Add(new User_TipMap());
        }
    }
}
