using System;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMain
{
    public class CellularInitializer : CreateDatabaseIfNotExists<CellularContext>
    // public class CellularInitializer :DropCreateDatabaseAlways<CellularContext>
    {
        protected override void Seed(CellularContext context)
        {
            ClientType clientType1 = new ClientType() { MinutePrice = 2, SMSPrice = 1.5, TypeName = "VIP" };
            ClientType clientType2 = new ClientType() { MinutePrice = 4, SMSPrice = 2.7, TypeName = "Client" };
            context.ClientType.Add(clientType1);
            context.ClientType.Add(clientType2);

            PackageOptions packageOptions1 = new PackageOptions() { MostCalledNumber = true , PackageName = "call free to one cellular"};
            PackageOptions packageOptions2 = new PackageOptions() { InsideFamilyCalls = true, PackageName = "call free to Inside Family" };
            PackageOptions packageOptions3 = new PackageOptions() { FixedPrice=200, MaxMinute=100 , PackageName = "100 Minute for 200 FixedPrice" };
            PackageOptions packageOptions4 = new PackageOptions() { FavouriteNumbers=true, PackageName = "call free to 3 frends" };
            PackageOptions packageOptions5 = new PackageOptions() { FavouriteNumbers = true, PackageName = "call free to 3 frends",FixedPrice = 150, MaxMinute = 50 };
            PackageOptions packageOptions6 = new PackageOptions() { FixedPrice = 100, MaxMinute = 50, PackageName = "50 Minute for 100 FixedPrice" };
            context.PackageOptions.Add(packageOptions1);
            context.PackageOptions.Add(packageOptions2);
            context.PackageOptions.Add(packageOptions3);
            context.PackageOptions.Add(packageOptions4);
            context.PackageOptions.Add(packageOptions5);
            context.PackageOptions.Add(packageOptions6);

            context.SaveChanges();

            Client client1 = new Client().NewClient(987654321, "tal", "yehuda", 1, "tel aviv ", "0526654808", 2);
            context.Client.Add(client1);
            Client client2 = new Client().NewClient(123456789, "asaad", "cdcd", 2, "ramat gan", "0547967126", 5);
            context.Client.Add(client2);
            Client client3 = new Client().NewClient(31851752, "tomer", "ron", 1, "cfar yona ", "0525458474", 8);
            context.Client.Add(client3);
            Client client4 = new Client().NewClient(317957574, "liat", "yehuda", 2, "eilat", "0544798424", 12);
            context.Client.Add(client4);
            Client client5 = new Client().NewClient(029717271, "sivan", "hayat", 1, "sharon ", "052857574", 4);
            context.Client.Add(client5);
            Client client6 = new Client().NewClient(89742374, "hila", "siman", 2, "tel aviv", "05454984829", 1);
            context.Client.Add(client6);
            Client client7 = new Client().NewClient(78693254, "maxim", "zluf", 1, "eilat ", "0525858215", 7);
            context.Client.Add(client7);
            Client client8 = new Client().NewClient(71453658, "dan", "avi", 2, "tel aviv", "0541984824", 4);
            context.Client.Add(client8);
            context.SaveChanges();

            //SelectedNumbers selectedNumbers = new SelectedNumbers(1, "0526654811", "0526654812", "0526654804");
            //Package package = new Package().NewPackage("student", 1, 100, 10, null,null, true,true);
            //Package package2 = new Package().NewPackage("allpackage", 1, 100, 50, 1, selectedNumbers, true, true);
            Line line1 = new Line(0, 1, client1, "0526654811", Status.Used, null, null, false);
            context.Line.Add(line1);
            context.SaveChanges();

            Line line2 = new Line(0, 1, client1,  "0526654812", Status.Used, null, null, false);
            Line line3 = new Line(0, 1, client1,  "0526654813", Status.Used, null, null, false);
            Line line4 = new Line(0, 1, client1 , "0526654814", Status.Used, null, null, false);
            Line line5 = new Line(0, 2, client2 , "0526654821", Status.Used, null, null, false);
            Line line6 = new Line(0, 2, client2,  "0526654822", Status.Used, null, null, false);
            Line line7 = new Line(0, 2, client2,  "0526654823", Status.Used, null, null, false);
            Line line8 = new Line(0, 3, client3,  "0526654831", Status.Used, null, null, false);
            Line line9 = new Line(0, 3, client3,  "0526654832", Status.Used, null, null, false);
            Line line10 = new Line(0, 4, client4, "0526654841", Status.Used, null, null, false);
            Line line11 = new Line(0, 4, client4, "0526654842", Status.Used, null, null, false);
            Line line12 = new Line(0, 5, client5, "0526654851", Status.Used, null, null, false);
            Line line13 = new Line(0, 6, client6, "0526654861", Status.Used, null, null, false);
            Line line14 = new Line(0, 7, client7, "0526654871", Status.Used, null, null, false);
            Line line15 = new Line(0, 8, client8, "0526654881", Status.Used, null, null, false);
            Line line16 = new Line(0, 8, client8, "0526654882", Status.Used, null, null, false);
            // context.Package.Add(package);
            context.Line.Add(line2);
            context.Line.Add(line3);
            context.Line.Add(line4);
            context.Line.Add(line5);
            context.Line.Add(line6);
            context.Line.Add(line7);
            context.Line.Add(line8);
            context.Line.Add(line9);
            context.Line.Add(line10);
            context.Line.Add(line11);
            context.Line.Add(line12);
            context.Line.Add(line13);
            context.Line.Add(line14);
            context.Line.Add(line15);
            context.Line.Add(line16);
            context.SaveChanges();

            Employee employee = new Employee(1234,"1234","emploee", "tal",UserAuthType.Employee,7);
            Employee employee1 = new Employee(123, "123", "emploee", "asaad", UserAuthType.Employee,5);
            Employee employee2 = new Employee(456, "456", "Manager", "tamir", UserAuthType.Manager,4);
            Employee employee3 = new Employee(789, "123", "emploee", "lior", UserAuthType.Manager, 2);
            Employee employee4 = new Employee(137, "137", "Manager", "tal", UserAuthType.Employee, 3);
            Employee employee5 = new Employee(987, "987", "Manager", "tal", UserAuthType.Employee, 1);
            context.Employee.Add(employee);
            context.Employee.Add(employee1);
            context.Employee.Add(employee2);
            context.Employee.Add(employee3);
            context.Employee.Add(employee4);
            context.Employee.Add(employee5);

            context.SaveChanges();
        }
    }
}
