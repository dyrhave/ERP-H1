using Org.BouncyCastle.Asn1.X509;
using TECHCOOL.UI;
using Microsoft.Data.SqlClient;

using var connection = Database.Instance.GetConnection();

 //Database.Instance?.AddCompany(new() {Name = "Test", Street = "Teset", StreetNumber = "1", City = "Test", Country = "Test", Currency = Currency.DKK, PostCode = "1234"});
// Database.Instance?.AddCompany(new() {Name = "Test2", Street = "Teset2", StreetNumber = "2", City = "Test2", Contry = "Test2", Currency = Currency.EUR, PostCode = "1234"});
// Database.Instance?.AddProduct(new() { Name = "Test", Quantity = 10,BuyInPrice = 5, Price = 10,});

CompanyListPage companylistpage = new();
ProductListPage productlistpage = new();
CustomerListPage customerlistPage = new();
SalesOrderList salesOrderList = new();


Menu mainMenu = new();
mainMenu.Add(companylistpage);
mainMenu.Add(productlistpage);
mainMenu.Add(customerlistPage);
mainMenu.Add(salesOrderList);
Screen.Display(mainMenu);