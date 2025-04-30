using TECHCOOL.UI;

public class ProductEdit : Screen
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
        lp.AddColumn("Description", nameof(Product.Description));
        lp.AddColumn("Price", nameof(Product.Price));
        lp.AddColumn("BuyInPrice", nameof(Product.BuyInPrice));
        lp.AddColumn("Location", nameof(Product.Location));
        lp.AddColumn("Stock", nameof(Product.Stock));
        lp.AddColumn("Unit", nameof(Product.Unit));     
       
        
        

        lp.Add(Database.Instance.GetProduct());
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.Select();
    }
}