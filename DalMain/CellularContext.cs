namespace DalMain
{
    using Common.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CellularContext : DbContext
    {
        public CellularContext() : base("name=CellularNetworkDB")
        {
            Database.SetInitializer<CellularContext>(new CellularInitializer());
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientType> ClientType { get; set; }
        public DbSet<Call> Call { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<SelectedNumbers> SelectedNumbers { get; set; }
        public DbSet<SMS> SMS { get; set; }
        public DbSet<PackageOptions> PackageOptions { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}