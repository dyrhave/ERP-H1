using TECHCOOL.UI;

public class ProductListPage : Screen
{
    public override string Title {get; set;} = "Products";
    void Back(Product _)
    {        
        Quit();       
    }

    protected override void Draw()
    {
        ListPage<Product> lp = new();
        lp.AddColumn("ProductId", nameof(Product.ProductId));
        lp.AddColumn("Name", nameof(Product.Name));
        lp.AddColumn("Stock", nameof(Product.Stock));
        lp.AddColumn("BuyInPrice", nameof(Product.BuyInPrice));
        lp.AddColumn("Price", nameof(Product.Price));       
        lp.AddColumn("ProfitMargin", nameof(Product.ShowProfitMargin));
       
        
        

        lp.Add(Database.Instance.GetProduct());
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();
    }
}