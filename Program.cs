using TECHCOOL.UI;
new Database();
Database.Instance.AddCompany(new() {Name = "Test", Street = "Test", StreetNumber = "1", City = "Test", Contry = "Test", Currency = Currency.DKK});
Database.Instance.AddCompany(new() {Name = "Test2", Street = "Test2", StreetNumber = "2", City = "Test2", Contry = "Test2", Currency = Currency.EUR});

CompanyListPage companylistpage = new();
Screen.Display(companylistpage);