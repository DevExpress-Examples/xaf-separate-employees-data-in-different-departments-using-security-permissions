using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;

namespace FilterRecords.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class FilterRecordsContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<FilterRecordsEFCoreDbContext>()
            .UseSqlServer(@";");
        return new FilterRecordsEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class FilterRecordsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FilterRecordsEFCoreDbContext> {
	public FilterRecordsEFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		//var optionsBuilder = new DbContextOptionsBuilder<FilterRecordsEFCoreDbContext>();
		//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=FilterRecords");
		//return new FilterRecordsEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(FilterRecordsContextInitializer))]
public class FilterRecordsEFCoreDbContext : DbContext {
	public FilterRecordsEFCoreDbContext(DbContextOptions<FilterRecordsEFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
	public DbSet<ModelDifference> ModelDifferences { get; set; }
	public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	public DbSet<PermissionPolicyRole> Roles { get; set; }
	public DbSet<FilterRecords.Module.BusinessObjects.ApplicationUser> Users { get; set; }
    public DbSet<FilterRecords.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<MyTask> MyTasks { get; set; }
    public DbSet<DepartmentGoal> DepartmentGoals { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<FilterRecords.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
    }
}
