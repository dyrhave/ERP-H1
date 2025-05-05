using TECHCOOL.UI;

public class ProductEdit : Screen
{
    public override string Title {get; set;} = "Edit Products";
    void Back(Product _)
    {        
        Quit();       
    }

    protected override void Draw()
    {
        Product product = new Product();
        Form<Product> editor = new();
        editor.IntBox("ProductId", nameof(Product.ProductId));
        editor.TextBox("Name", nameof(Product.Name));
        editor.TextBox("Description", nameof(Product.Description));
        editor.IntBox("Price", nameof(Product.Price));
        editor.IntBox("BuyInPrice", nameof(Product.BuyInPrice));
        editor.TextBox("Location", nameof(Product.Location));
        editor.IntBox("Stock", nameof(Product.Quantity));
        editor.TextBox("Unit", nameof(Product.Unit));
        editor.Edit(product);

        
        Quit();

        // Screen.AddKey(ConsoleKey.Escape, Back);
    }
}