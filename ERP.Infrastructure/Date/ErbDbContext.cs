using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Entities.Cross_cutting;
using ERP.Domain.Entities.Customers___Sales;
using ERP.Domain.Entities.HR;
using ERP.Domain.Entities.Inventory;

using ERP.Domain.Entities.Suppliers___Purchase;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Date;

public class ErbDbContext : DbContext
{
    public ErbDbContext(DbContextOptions<ErbDbContext> options)
        : base(options)
    {
    }

    // ==========================
    // Auth
    // ==========================
    public DbSet<Users> Users { get; set; }

    public DbSet<Roles> Roles { get; set; }

    public DbSet<UserRoles> UserRoles { get; set; }

    public DbSet<Permissions> Permissions { get; set; }

    public DbSet<RolePermissions> RolePermissions { get; set; }

    public DbSet<RefreshTokens> RefreshTokens { get; set; }

    // ==========================
    // HR
    // ==========================
    public DbSet<Departments> Departments { get; set; }

    public DbSet<Employees> Employees { get; set; }

    public DbSet<Attendance> Attendances { get; set; }

    public DbSet<Payroll> Payrolls { get; set; }

    // ==========================
    // Inventory
    // ==========================
    public DbSet<Categories> Categories { get; set; }

    public DbSet<Products> Products { get; set; }

    public DbSet<Warehouses> Warehouses { get; set; }

    public DbSet<StockItems> StockItems { get; set; }

    public DbSet<StockMovements> StockMovements { get; set; }

    // ==========================
    // Purchasing
    // ==========================
    public DbSet<Suppliers> Suppliers { get; set; }

    public DbSet<PurchaseOrders> PurchaseOrders { get; set; }

    public DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }

    // ==========================
    // Sales
    // ==========================
    public DbSet<Customers> Customers { get; set; }

    public DbSet<SalesOrders> SalesOrders { get; set; }

    public DbSet<SalesOrdersItems> SalesOrderItems { get; set; }

    // ==========================
    // Cross-Cutting
    // ==========================
    public DbSet<Notifications> Notifications { get; set; }

    public DbSet<AuditLogs> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErbDbContext).Assembly);
    }
}