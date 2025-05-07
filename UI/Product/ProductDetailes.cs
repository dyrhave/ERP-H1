using TECHCOOL.UI;

public class ProductDetailes : Screen
{
    public override string Title {get; set;} = "Products";
    void Delete(Product cmp)
    {
        Database.Instance?.DeleteProduct(cmp.ProductId);
        Console.Clear();      
    }
    void ShowEdit(Product _)
    {
        Screen.Display(new ProductEdit());
    }
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
        lp.AddColumn("In Stock", nameof(Product.Quantity));
        lp.AddColumn("Location", nameof(Product.Location));
        lp.AddColumn("Stock", nameof(Product.Quantity));
        lp.AddColumn("Unit", nameof(Product.Unit));
        lp.AddColumn("ProfitMargin", nameof(Product.ShowProfitMargin));
        lp.AddColumn("Profit", nameof(Product.ShowProfit));
       
        
        

        lp.Add(Database.Instance?.GetProduct());
        lp.AddKey(ConsoleKey.Escape, Back);
        lp.AddKey(ConsoleKey.F5, Delete);
        lp.AddKey(ConsoleKey.F2, ShowEdit);   
        lp.Select();
    }
}