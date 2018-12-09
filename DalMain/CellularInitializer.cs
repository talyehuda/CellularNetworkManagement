using System;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain
{
   // public class CellularInitializer : CreateDatabaseIfNotExists<CellularContext>
     public class CellularInitializer :DropCreateDatabaseAlways<CellularContext>
    {
        protected override void Seed(CellularContext context)
        {
            ClientType clientType1 = new ClientType() { MinutePrice = 2, SMSPrice = 1.5, TypeName = "VIP" };
            ClientType clientType2 = new ClientType() { MinutePrice = 4, SMSPrice = 2.7, TypeName = "Client" };
            context.ClientType.Add(clientType1);
            context.ClientType.Add(clientType2);

            PackageOptions packageOptions1 = new PackageOptions() { MostCalledNumber = true , PackageName = "call free to one cellular"};
            PackageOptions packageOptions2 = new PackageOptions() { InsideFamilyCalls = true, PackageName = "call free to Inside Family" };
            PackageOptions packageOptions3 = new PackageOptions() { FixedPrice=55, MaxMinute=10 , PackageName = "10 Minute for 55 FixedPrice" };
            PackageOptions packageOptions4 = new PackageOptions() { FavouriteNumbers=true, PackageName = "call free to 3 frends" };
            context.PackageOptions.Add(packageOptions1);
            context.PackageOptions.Add(packageOptions2);
            context.PackageOptions.Add(packageOptions3);
            context.PackageOptions.Add(packageOptions4);
            context.SaveChanges();

            Client client1 = new Client().NewClient(987654321, "tal", "yehuda", 1, "jcd djd ", "052", 0);
            context.Client.Add(client1);
            Client client2 = new Client().NewClient(123456789, "asaad", "cdcd", 2, "erererer", "054-798424", 0);
            context.Client.Add(client2);
            context.SaveChanges();

            SelectedNumbers selectedNumbers = new SelectedNumbers(1, "0526654811", "0526654812", "0526654804");
            Package package = new Package().NewPackage("student", 1, 100, 10, null,null, true,true);
            Package package2 = new Package().NewPackage("allpackage", 1, 100, 50, 1, selectedNumbers, true, true);
            Line line1 = new Line(0, 0, client1, "0526654811", Status.Used, 0, package, false);
            Line line2 = new Line(0, 0, client1, "0526654812", Status.Used, 0, package, false);
            Line line3 = new Line(0, 0, client1, "0526654813", Status.Used, 0, package, false);
            Line line4 = new Line(0, 0, client1, "0526654814", Status.Used, 0, package, false);
            Line line5 = new Line(0, 1, client2, "0526654821", Status.Used, 0, package, false);
            Line line6 = new Line(0, 1, client2, "0526654822", Status.Used, 0, package2, false);
            context.Package.Add(package);
            context.Line.Add(line1);
            context.Line.Add(line2);
            context.Line.Add(line3);
            context.Line.Add(line4);
            context.Line.Add(line5);
            context.Line.Add(line6);
            context.SaveChanges();

            Employee employee = new Employee(1234,"1234","emploee", "tal",UserAuthType.Employee);
            Employee employee1 = new Employee(123, "123", "emploee", "asaad", UserAuthType.Employee);
            Employee employee2 = new Employee(456, "456", "Manager", "tal", UserAuthType.Manager);
            context.Employee.Add(employee);
            context.Employee.Add(employee1);
            context.Employee.Add(employee2);
            context.SaveChanges();
        }
    }
}
