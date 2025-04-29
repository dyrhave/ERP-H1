using TECHCOOL.UI;
new Database();
Database.Instance.AddCompany(new() {Name = "Test", Street = "Teset", StreetNumber = "1", City = "Test", Contry = "Test", Currency = Currency.DKK, PostCode = "1234"});
Database.Instance.AddCompany(new() {Name = "Test2", Street = "Teset2", StreetNumber = "2", City = "Test2", Contry = "Test2", Currency = Currency.EUR, PostCode = "1234"});

CompanyListPage companylistpage = new();
Screen.Display(companylistpage);