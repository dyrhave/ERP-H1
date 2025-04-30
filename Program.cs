using Org.BouncyCastle.Asn1.X509;
using TECHCOOL.UI;

new Database();
Database.Instance.AddCompany(new() {Name = "Test", Street = "Teset", StreetNumber = "1", City = "Test", Contry = "Test", Currency = Currency.DKK, PostCode = "1234"});
Database.Instance.AddCompany(new() {Name = "Test2", Street = "Teset2", StreetNumber = "2", City = "Test2", Contry = "Test2", Currency = Currency.EUR, PostCode = "1234"});
Database.Instance.AddProduct(new() {Name = "Test", Description = "Test", Price = 10, BuyInPrice = 5, Stock = 10, Location = "Test"});

CompanyListPage companylistpage = new();
ProductInfo productInfo = new();
Menu mainMenu = new();
mainMenu.Add(companylistpage);
mainMenu.Add(productInfo);
Screen.Display(mainMenu);