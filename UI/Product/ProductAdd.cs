using TECHCOOL.UI;
public class ProductAdd : Screen
{
    public override string Title {get; set;} = "Add Product";
    protected override void Draw()
    {
        Product product = new Product();
        
        Form<Product> editor = new();
        editor.TextBox("Title", nameof(Product.Name));        
        editor.TextBox("Description", nameof(Product.Description));
        editor.AddField("Price", new DecimalBox(){ Title = "Price", Property = nameof(Product.Price) });        
        editor.AddField("BuyInPrice", new DecimalBox(){ Title = "BuyInPrice", Property = nameof(Product.BuyInPrice) });        
        editor.AddField("Quantity", new DecimalBox(){ Title = "Quantity", Property = nameof(Product.Quantity) });
        editor.TextBox("Location", nameof(Product.Location));        
        editor.TextBox("Unit", nameof(Product.Unit));
        
        
        if (editor.Edit(product))
        {
            Database.Instance.AddProduct(product);
            Quit();
        }
    }

}